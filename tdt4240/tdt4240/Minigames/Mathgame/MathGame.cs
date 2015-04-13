using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using tdt4240.Boards;

namespace tdt4240.Minigames.MathGame
{
    class MathGame : MiniGame
    {
        private const int ScreenPadding = 10;
        private Vector2[] _corners;
        private readonly int _numberOfPlayers;


        public MathGame(Board board) : base(board)
        {
            _numberOfPlayers = PlayerManager.Instance.NumberOfPlayers;

            _corners = new[]{new Vector2(ScreenPadding, ScreenPadding),
            new Vector2(ScreenManager.MaxWidth - ScreenPadding, ScreenPadding),
            new Vector2(ScreenPadding, ScreenManager.MaxHeight - ScreenPadding),
            new Vector2(ScreenManager.MaxWidth - ScreenPadding, ScreenManager.MaxHeight - ScreenPadding)};
        }

        public override void Activate(bool instancePreserved)
        {

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

        }


        /// <summary>
        /// This is called when the screen should draw itself.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();
            //TODO
            spriteBatch.End();
        }
        public override void NotifyDone(PlayerIndex winnerIndex)
        {
            base.NotifyDone(winnerIndex);
        }
    }
}
