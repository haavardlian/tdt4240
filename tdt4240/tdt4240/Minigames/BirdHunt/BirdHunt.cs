using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tdt4240.Boards;

namespace tdt4240.Minigames.BirdHunt
{
    class BirdHunt : MiniGame
    {
        private readonly int _numberOfPlayers;
        private readonly List<CrossHair> _crossHairs;
        public new static SupportedPlayers SupportedPlayers = SupportedPlayers.All;


        public BirdHunt(Board board) : base(board)
        {
            _numberOfPlayers = PlayerManager.Instance.NumberOfPlayers;
            
            _crossHairs = new List<CrossHair>();
        }

        public override void Activate(bool instancePreserved)
        {
            base.Activate(instancePreserved);



            if (!instancePreserved)
            {
                var sprite = content.Load<Texture2D>("minigames/BirdHunt/CrossHair");

                for (int i = 0; i < _numberOfPlayers; i++)
                {
                    _crossHairs.Add(new CrossHair(PlayerManager.Instance.Players[i],sprite));
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
        }

        /// <summary>
        /// Allows the screen to handle user input. Unlike Update, this method
        /// is only called when the screen is active, and not when some other
        /// screen has taken the focus.
        /// </summary>
        public override void HandleInput(GameTime gameTime, InputState input)
        {
            foreach (CrossHair crossHair in _crossHairs)
            {
                crossHair.Position += crossHair.Owner.Input.GetThumbstickVector();

                if (crossHair.Owner.Input.IsButtonPressed(GameButtons.X))
                {
                    //SHOOOOT
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

            foreach (CrossHair crossHair in _crossHairs)
            {
                spriteBatch.Draw(crossHair.Sprite, crossHair.Position, Color.White);
            }

            spriteBatch.End();
        }
        public override void NotifyDone(PlayerIndex winnerIndex)
        {
            base.NotifyDone(winnerIndex);
        }
    }
}
