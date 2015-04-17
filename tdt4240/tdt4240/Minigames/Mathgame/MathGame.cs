﻿using System;
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

        public static SupportedPlayers SupportedPlayers = SupportedPlayers.Four;

        private SpriteFont _font;
        private readonly Vector2[] _textPosition = new Vector2[4];

        private readonly int _numberOfPlayers;
        private readonly int _scoreToWin;
        private SpriteFont _numberFont;
        private SpriteFont _equationFont;
        private Vector2 _fontPosNumber;
        private Vector2 _fontPosEquation;
        private String _fontOutputNumber;
        private String _fontOutputEquation;
        private DateTime _nextEquationTime;
        private string _currentNumber;
        private string _currentEquation;
        private int _equationNumber = 0;
        private readonly List<Mathplayer> _mathplayers;

        private const int ScreenPadding = 10;
        private Vector2[] _corners;

        private static readonly Random rnd = new Random();
        private static readonly TimeSpan MaxTimePerEquation = TimeSpan.FromSeconds(3);

        Problem problem = new Problem();

        public MathGame(Board board) : base(board)
        {
            Title = "Math game";
            _numberOfPlayers = PlayerManager.Instance.NumberOfPlayers;

            _mathplayers = new List<Mathplayer>();

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
            _equationNumber = 0;
            ShowNewEquation();
        }

        private void ShowNewEquation()
        {
            _currentEquation = problem.equationTable[_equationNumber].equation;
            Console.WriteLine("Equation: " + problem.equationTable[_equationNumber].CorrectAnswer);
            _nextEquationTime = DateTime.Now + MaxTimePerEquation;
            _equationNumber++;
            
        }

        public override void Activate(bool instancePreserved)
        {

            base.Activate(instancePreserved);

            if (!instancePreserved)
            {
                _fontPosNumber = new Vector2(ScreenManager.GraphicsDevice.Viewport.Width / 2,
                    ScreenManager.GraphicsDevice.Viewport.Height / 5);
                _fontPosEquation = new Vector2(ScreenManager.GraphicsDevice.Viewport.Width / 2,
                    ScreenManager.GraphicsDevice.Viewport.Height / 3);
                _numberFont = ScreenManager.Font;
                _equationFont = ScreenManager.Font;
                _font = ScreenManager.Font;
                Background = new Background("background");
                ScreenManager.AddScreen(Background, null);

                for (var i = 0; i < _numberOfPlayers; i++)
                {
                    _mathplayers.Add(new Mathplayer(PlayerManager.Instance.Players[i]));
                    _mathplayers[i].Corner = _corners[i];
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
            if (_nextEquationTime <= DateTime.Now)
            {
                if (_equationNumber >= problem.numberOfEquations)
                {
                    ShowNewProblem();
                }
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
            foreach (var mathplayer in _mathplayers)
            {
                if (mathplayer.Player.Input.IsButtonPressed(GameButtons.X))
                {
                    if (problem.equationTable[_equationNumber-1].CorrectAnswer)
                    {
                        mathplayer.score += 1;
                        ShowNewProblem();
                        if (mathplayer.score == _scoreToWin)
                        {
                            NotifyDone(mathplayer.Player.PlayerIndex);
                        }
                    }
                    else
                    {
                        mathplayer.score -= 1;
                    }
                }
            }
            /*
            foreach (Player player in PlayerManager.Instance.Players)
            {
                _textPosition[(int)player.PlayerIndex] += player.Input.GetThumbstickVector();

                if (player.Input.IsButtonPressed(GameButtons.Y))
                {
                    NotifyDone(PlayerIndex.One);
                }
            }
             * */
        }


        /// <summary>
        /// This is called when the screen should draw itself.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            /*
            foreach (Player player in PlayerManager.Instance.Players)
            {
                spriteBatch.DrawString(_font, player.TestString, _textPosition[(int)player.PlayerIndex], player.Color);
            }
             * */
            
            _fontOutputNumber = _currentNumber;
            _fontOutputEquation = _currentEquation;

            Vector2 FontOriginNumber = _numberFont.MeasureString(_fontOutputNumber) / 2;
            Vector2 FontOriginEquation = _equationFont.MeasureString(_fontOutputEquation) / 2;

            spriteBatch.DrawString(_numberFont, _fontOutputNumber, _fontPosNumber, Color.Black,
                0, FontOriginNumber, 1.0f, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(_equationFont, _fontOutputEquation, _fontPosEquation, Color.Black,
                0, FontOriginEquation, 1.0f, SpriteEffects.None, 0.5f);

            foreach (var mathplayer in _mathplayers)
            {
                spriteBatch.DrawString(ScreenManager.Font, mathplayer.score.ToString(), mathplayer.Corner * ScreenManager.GetScalingFactor(), mathplayer.Color);
            }

            spriteBatch.End();
        }

        public override void NotifyDone(PlayerIndex winnerIndex)
        {
            base.NotifyDone(winnerIndex);
        }

    }
}
