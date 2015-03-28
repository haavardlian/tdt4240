using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tdt4240.Boards;

namespace tdt4240.Minigames.BirdHunt
{
    class BirdHunt : MiniGame
    {
        private const double AimingDifficulty = 0.2;
        private const double BirdSpawnRate = 0.2;

        private readonly int _numberOfPlayers;
        private readonly List<Gun> _guns;
        private readonly List<Bird> _birds;
        private readonly BirdFactory _birdFactory;
        public new static SupportedPlayers SupportedPlayers = SupportedPlayers.All;
        private readonly Random _random;


        public BirdHunt(Board board) : base(board)
        {
            _birdFactory = new BirdFactory(content.Load<Texture2D>("minigames/BirdHunt/Bird"));
            _numberOfPlayers = PlayerManager.Instance.NumberOfPlayers;
            _random = new Random();
            _guns = new List<Gun>();
            _birds = new List<Bird>();
        }

        public override void Activate(bool instancePreserved)
        {
            base.Activate(instancePreserved);



            if (!instancePreserved)
            {
                var crossHair = content.Load<Texture2D>("minigames/BirdHunt/CrossHair");
                //TODO: create sprite
                var shot = content.Load<Texture2D>("minigames/BirdHunt/Shot");

                for (var i = 0; i < _numberOfPlayers; i++)
                {
                    _guns.Add(new Gun(PlayerManager.Instance.Players[i],crossHair,shot));
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
            if (_random.NextDouble() < BirdSpawnRate)
            {
                //TODO: add bird in bottom of screen
                _birds.Add(_birdFactory.GenerateBird());
            }

            foreach (var bird in _birds)
            {
                //TODO: delete bird when it reaches top of screen
            }
        }

        /// <summary>
        /// Allows the screen to handle user input. Unlike Update, this method
        /// is only called when the screen is active, and not when some other
        /// screen has taken the focus.
        /// </summary>
        public override void HandleInput(GameTime gameTime, InputState input)
        {
            foreach (var gun in _guns)
            {
                gun.Position += gun.Player.Input.GetThumbstickVector()*3;

                if (gun.Player.Input.IsButtonPressed(GameButtons.X))
                {
                    gun.Fire();
                    Console.WriteLine("fired");
                }

            }
        }


        /// <summary>
        /// This is called when the screen should draw itself.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();
            foreach (var gun in _guns)
            {
                spriteBatch.Draw(gun.Texture, gun.Position*ScreenManager.GetScalingFactor(), null, Color.White, 0f, new Vector2(0,0),ScreenManager.GetScalingFactor(), SpriteEffects.None, 0f );
                if(gun.Fired)
                    spriteBatch.Draw(gun.Shot, gun.Position * ScreenManager.GetScalingFactor(), null, Color.White, 0f, new Vector2(0, 0), ScreenManager.GetScalingFactor(), SpriteEffects.None, 0f);
            }

            foreach (var bird in _birds)
            {
                //TODO: using scalingfactor to draw bird smaller when it goes higher to the screen
                spriteBatch.Draw(bird.Texture, bird.Position * ScreenManager.GetScalingFactor(), null, Color.White, 0f, new Vector2(0, 0), ScreenManager.GetScalingFactor(), SpriteEffects.None, 0f);
            }

            spriteBatch.End();
        }
        public override void NotifyDone(PlayerIndex winnerIndex)
        {
            base.NotifyDone(winnerIndex);
        }
    }
}
