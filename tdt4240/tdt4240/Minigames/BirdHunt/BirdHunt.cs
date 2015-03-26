using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tdt4240.Minigames.BirdHunt
{
    class BirdHunt : MiniGame
    {
        private int numberOfPlayers;
        private List<CrossHair> crossHairs;

        public BirdHunt(Board board) : base(board)
        {
            this.supportedPlayers = SupportedPlayers.All;
            this.numberOfPlayers = PlayerManager.Instance.NumberOfPlayers;
            
            crossHairs = new List<CrossHair>();
        }

        public override void Activate(bool instancePreserved)
        {
            base.Activate(instancePreserved);



            if (!instancePreserved)
            {
                for (int i = 0; i < numberOfPlayers; i++)
                {
                    crossHairs.Add(new CrossHair(content.Load<Texture2D>("minigames/BirdHunt/CrossHair"),PlayerManager.Instance.Players[i]));
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
            foreach (CrossHair crossHair in crossHairs)
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

            foreach (CrossHair crossHair in crossHairs)
            {
                spriteBatch.Draw(crossHair, crossHair.Position, crossHair.Color);
            }

            spriteBatch.End();
        }
        public override void NotifyDone(PlayerIndex winnerIndex)
        {
            base.NotifyDone(winnerIndex);
        }
    }
}
