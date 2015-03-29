using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using tdt4240.Boards;

namespace tdt4240.Minigames.BirdHunt
{
    class BirdHunt : MiniGame
    {
        private const double AimingDifficulty = 0.2;
        private const double BirdSpawnRate = 0.05;

        private const int ScreenPadding = 10;
        private Vector2[] _corners;

        private readonly int _numberOfPlayers;
        private readonly List<Gun> _guns;
        private readonly List<Bird> _birds;
        private readonly BirdFactory _birdFactory;
        public new static SupportedPlayers SupportedPlayers = SupportedPlayers.All;
        private readonly Random _random;


        public BirdHunt(Board board) : base(board)
        {
            _birdFactory = new BirdFactory();
            _numberOfPlayers = PlayerManager.Instance.NumberOfPlayers;
            _random = new Random();
            _guns = new List<Gun>();
            _birds = new List<Bird>();

            _corners = new[]{new Vector2(ScreenPadding, ScreenPadding),
            new Vector2(ScreenManager.MaxWidt - ScreenPadding, ScreenPadding),
            new Vector2(ScreenPadding, ScreenManager.MaxHeight - ScreenPadding),
            new Vector2(ScreenManager.MaxWidt - ScreenPadding, ScreenManager.MaxHeight - ScreenPadding)};
        }

        public override void Activate(bool instancePreserved)
        {
            base.Activate(instancePreserved);



            if (!instancePreserved)
            {
                _birdFactory.SetTexture(content.Load<Texture2D>("minigames/BirdHunt/Bird"));
                var crossHair = content.Load<Texture2D>("minigames/BirdHunt/CrossHair");
                var shot = content.Load<Texture2D>("minigames/BirdHunt/Shot");

                for (var i = 0; i < _numberOfPlayers; i++)
                {
                    _guns.Add(new Gun(PlayerManager.Instance.Players[i],crossHair,shot));
                    _guns[i].Corner = _corners[i];
                }
            }

        }

        /// <summary>
        /// Allows the screen to run logic, such as updating the transition position.
        /// Unlike HandleInput, this method is called regardless of whether the screen
        /// is active, hidden, or in the middle of a transition.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            foreach (var gun in _guns)
            {
                if(_random.NextDouble()<AimingDifficulty)
                    gun.UpdateAccuracy();
                    gun.Position += gun.Accuracy;
            }
            foreach (var bird in _birds)
            {
                bird.UpdateSpeed();
                bird.Position += bird.Speed;
            }
            if (_random.NextDouble() < BirdSpawnRate)
            {
                _birds.Add(_birdFactory.GenerateBird());
            }
            //if the birds reaches the top of the screen it will be removed
            _birds.RemoveAll(bird => bird.Position.Y < bird.Center.Y);
        }

        /// <summary>
        /// Allows the screen to handle user input. Unlike Update, this method
        /// is only called when the screen is active, and not when some other
        /// screen has taken the focus.
        /// </summary>
        public override void HandleInput(GameTime gameTime, InputState input)
        {
            //PlayerIndex player;
            //input.IsButtonPressed(Buttons.Start, null,out player);

            /*KeyboardState keyboardState = input.CurrentKeyboardStates[0];

            if (keyboardState.IsKeyDown(Keys.Space))
            {
                if (_guns[0].Fire())
                {
                    var deadBirds = CheckForHit(_guns[0]);
                    _guns[0].Score += deadBirds.Count();

                    deadBirds.ForEach(bird => _birds.Remove(bird));
                }
            }*/

            foreach (var gun in _guns)
            {
                gun.Position += gun.Player.Input.GetThumbstickVector()*3;
                //GameButtons.X == Keyboard.3
               if (gun.Player.Input.IsButtonPressed(GameButtons.X))
                {
                    if (gun.Fire())
                    {
                        var deadBirds = CheckForHit(gun);
                        gun.Score += deadBirds.Count();

                        deadBirds.ForEach(bird => _birds.Remove(bird));
                    }
                }

            }
        }

        private List<Bird> CheckForHit(Gun gun)
        {
            return _birds.Where(bird => bird.Contains(gun.Position + gun.Center)).ToList();
        }


        /// <summary>
        /// This is called when the screen should draw itself.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            foreach (var bird in _birds)
            {
                spriteBatch.Draw(bird.Texture, bird.Position * ScreenManager.GetScalingFactor(), null, Color.White, 0f, new Vector2(0, 0), ScreenManager.GetScalingFactor() * bird.getDistanceFactor(), SpriteEffects.None, 0f);
            }

            foreach (var gun in _guns)
            {
                spriteBatch.DrawString(ScreenManager.Font, gun.Score.ToString(),gun.Corner*ScreenManager.GetScalingFactor(), gun.Color);

                spriteBatch.Draw(gun.Texture, gun.Position*ScreenManager.GetScalingFactor(), null, Color.White, 0f, new Vector2(0,0),ScreenManager.GetScalingFactor(), SpriteEffects.None, 0f );
                if(gun.Fired)
                    spriteBatch.Draw(gun.Shot, (gun.Position + gun.Center) * ScreenManager.GetScalingFactor(), null, Color.White, 0f, new Vector2(0, 0), ScreenManager.GetScalingFactor(), SpriteEffects.None, 0f);
            }



            spriteBatch.End();
        }
        public override void NotifyDone(PlayerIndex winnerIndex)
        {
            base.NotifyDone(winnerIndex);
        }
    }
}
