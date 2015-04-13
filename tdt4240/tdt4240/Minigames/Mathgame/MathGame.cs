using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using tdt4240.Boards;
using tdt4240.Minigames.Mathgame;

namespace tdt4240.Minigames.MathGame
{
    class MathGame : MiniGame
    {
        private const int ScreenPadding = 10;
        private Vector2[] _corners;
        private readonly int _numberOfPlayers;
        private SpriteFont number;
        private Vector2 fontPos;
        private String fontOutput;
        private DateTime _nextNumberTime;
        private string _currentNumber;
        private string _currentExpression;

        private static readonly Random rnd = new Random();
        private static readonly TimeSpan MaxTimePerNumber = TimeSpan.FromSeconds(10);
        private static readonly TimeSpan MaxTimePerExpression = TimeSpan.FromSeconds(2);

        public MathGame(Board board) : base(board)
        {
            _numberOfPlayers = PlayerManager.Instance.NumberOfPlayers;

            _corners = new[]{new Vector2(ScreenPadding, ScreenPadding),
            new Vector2(ScreenManager.MaxWidth - ScreenPadding, ScreenPadding),
            new Vector2(ScreenPadding, ScreenManager.MaxHeight - ScreenPadding),
            new Vector2(ScreenManager.MaxWidth - ScreenPadding, ScreenManager.MaxHeight - ScreenPadding)};

            _nextNumberTime = DateTime.Now;
            ShowNewCombo();

        }

        private string GetRandomNumber()
        {
            return rnd.Next(101).ToString();
        }

        private void ShowNewCombo()
        {
            _currentNumber = GetRandomNumber();
            //_currentExpression = GetRandomExpression();
            _nextNumberTime = DateTime.Now + MaxTimePerNumber;
        }

        public override void Activate(bool instancePreserved)
        {
            base.Activate(instancePreserved);

            if (!instancePreserved)
            {
                fontPos = new Vector2(ScreenManager.GraphicsDevice.Viewport.Width / 2,
                    ScreenManager.GraphicsDevice.Viewport.Height / 5);
                number = ScreenManager.Font;
                Background = new Background("background");
                ScreenManager.AddScreen(Background, null);
            }

        }

        /// <summary>
        /// Allows the screen to run logic, such as updating the transition position.
        /// Unlike HandleInput, this method is called regardless of whether the screen
        /// is active, hidden, or in the middle of a transition.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            if (_nextNumberTime <= DateTime.Now)
            {
                ShowNewCombo();
                Console.WriteLine(ScreenManager.GetScalingFactor());
            }
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
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

            fontOutput = _currentNumber;

            Vector2 FontOrigin = number.MeasureString(fontOutput) / 2;
            spriteBatch.DrawString(number, fontOutput, fontPos, Color.Black,
                0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);

            spriteBatch.End();
        }

        public override void NotifyDone(PlayerIndex winnerIndex)
        {
            base.NotifyDone(winnerIndex);
        }

    }
}
