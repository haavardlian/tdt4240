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
        private SpriteFont equation;
        private Vector2 fontPosNumber;
        private Vector2 fontPosEquation;
        private String fontOutputNumber;
        private String fontOutputEquation;
        private DateTime _nextEquationTime;
        private string _currentNumber;
        private string _currentEquation;
        private int equationNumber = 0;

        private static readonly Random rnd = new Random();
        private static readonly TimeSpan MaxTimePerEquation = TimeSpan.FromSeconds(5);

        Problem problem = new Problem();

        public MathGame(Board board) : base(board)
        {
            _numberOfPlayers = PlayerManager.Instance.NumberOfPlayers;

            _corners = new[]{new Vector2(ScreenPadding, ScreenPadding),
            new Vector2(ScreenManager.MaxWidth - ScreenPadding, ScreenPadding),
            new Vector2(ScreenPadding, ScreenManager.MaxHeight - ScreenPadding),
            new Vector2(ScreenManager.MaxWidth - ScreenPadding, ScreenManager.MaxHeight - ScreenPadding)};

            _nextEquationTime = DateTime.Now;
            ShowNewProblem();

        }

        private void ShowNewProblem()
        {
            problem = new Problem();
            _currentNumber = problem.answer;
            equationNumber = 0;
            ShowNewEquation();
        }

        private void ShowNewEquation()
        {
            if (equationNumber >= problem.numberOfEquations)
            {
                ShowNewProblem();
            }
            _currentEquation = problem.equationTable[equationNumber]._equation;
            _nextEquationTime = DateTime.Now + MaxTimePerEquation;
            equationNumber++;
            
        }

        public override void Activate(bool instancePreserved)
        {
            base.Activate(instancePreserved);

            if (!instancePreserved)
            {
                fontPosNumber = new Vector2(ScreenManager.GraphicsDevice.Viewport.Width / 2,
                    ScreenManager.GraphicsDevice.Viewport.Height / 5);
                fontPosEquation = new Vector2(ScreenManager.GraphicsDevice.Viewport.Width / 2,
                    ScreenManager.GraphicsDevice.Viewport.Height / 3);
                number = ScreenManager.Font;
                equation = ScreenManager.Font;
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
            if (_nextEquationTime <= DateTime.Now)
            {
                ShowNewEquation();
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

            fontOutputNumber = _currentNumber;
            fontOutputEquation = _currentEquation;

            Vector2 FontOriginNumber = number.MeasureString(fontOutputNumber) / 2;
            Vector2 FontOriginEquation = equation.MeasureString(fontOutputEquation) / 2;

            spriteBatch.DrawString(number, fontOutputNumber, fontPosNumber, Color.Black,
                0, FontOriginNumber, 1.0f, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(equation, fontOutputEquation, fontPosEquation, Color.Black,
                0, FontOriginEquation, 1.0f, SpriteEffects.None, 0.5f);

            spriteBatch.End();
        }

        public override void NotifyDone(PlayerIndex winnerIndex)
        {
            base.NotifyDone(winnerIndex);
        }

    }
}
