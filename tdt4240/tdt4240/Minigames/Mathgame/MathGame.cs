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

        public static SupportedPlayers SupportedPlayers = SupportedPlayers.Four;

        private SpriteFont _font;
        private readonly Vector2[] _textPosition = new Vector2[4];

        private readonly int _numberOfPlayers;
        private Texture2D _blankTexture;
        private readonly int _scoreToWin = 3;
        private DateTime _nextEquationTime;
        private string _currentNumber;
        private string _currentEquation;
        private int _equationNumber = 0;
        private readonly List<Mathplayer> _mathplayers;
        private Player _lastResponder;
        private enum GameState { InGame, Correct, Wrong, Winner }
        private GameState _gameState;

        private const int ScreenPadding = 10;

        private static readonly Random rnd = new Random();
        private static readonly TimeSpan MaxTimePerEquation = TimeSpan.FromSeconds(3);

        Problem problem = new Problem();

        public MathGame(Board board) : base(board)
        {
            Title = "Math game";
            _gameState = GameState.InGame;
            _numberOfPlayers = PlayerManager.Instance.NumberOfPlayers;

            _mathplayers = new List<Mathplayer>();

            _nextEquationTime = DateTime.Now;
            ShowNewProblem();

        }

        private void ShowNewProblem()
        {
            if (_gameState == GameState.Winner)
            {
                NotifyDone(_lastResponder.PlayerIndex);
            }
            _gameState = GameState.InGame;
            problem = new Problem();
            _currentNumber = problem.answer;
            _equationNumber = 0;
            ShowNewEquation();
        }

        private void ShowNewEquation()
        {
            if (_gameState == GameState.Winner)
            {
                NotifyDone(_lastResponder.PlayerIndex);
            }
            _gameState = GameState.InGame;
            _currentEquation = problem.equationTable[_equationNumber].equation;
            Console.WriteLine("Equation: " + problem.equationTable[_equationNumber].CorrectAnswer);
            _nextEquationTime = DateTime.Now + MaxTimePerEquation;
            _equationNumber++;

        }

        private double GetRelativeTimeLeft()
        {
            var diff = _nextEquationTime - DateTime.Now;
            return diff.TotalSeconds / MaxTimePerEquation.TotalSeconds;
        }

        public override void Activate(bool instancePreserved)
        {

            base.Activate(instancePreserved);

            if (!instancePreserved)
            {
                _font = content.Load<SpriteFont>("fonts/dice");
                _blankTexture = content.Load<Texture2D>("blank");
                Background = new Background("background");
                ScreenManager.AddScreen(Background, null);

                for (var i = 0; i < _numberOfPlayers; i++)
                {
                    _mathplayers.Add(new Mathplayer(PlayerManager.Instance.Players[i]));
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
            if (!_gameState.Equals(GameState.InGame)) return;
            foreach (var mathplayer in _mathplayers)
            {
                if (mathplayer.Player.Input.IsButtonPressed(GameButtons.X))
                {
                    _lastResponder = mathplayer.Player;
                    if (problem.equationTable[_equationNumber-1].CorrectAnswer)
                    {
                        mathplayer.score += 1;
                        _gameState = GameState.Correct;
                        if (mathplayer.score == _scoreToWin)
                        {
                            _gameState = GameState.Winner;
                        }
                    }
                    else
                    {
                        mathplayer.score -= 1;
                        _gameState = GameState.Wrong;
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

            // Draw number
            var stringSizeNumber = _font.MeasureString(_currentNumber);
            var desiredHeightNumber = 0.2 * ScreenManager.GetHeight();
            var stringScaleNumber = (float)desiredHeightNumber / stringSizeNumber.Y;
            spriteBatch.DrawString(
                _font,
                _currentNumber,
                new Vector2(
                    (int)(0.5 * ScreenManager.GetWidth() - 0.5 * stringSizeNumber.X * stringScaleNumber),
                    (int)(0.1 * ScreenManager.GetHeight())
                ),
                Color.Black,
                0,
                new Vector2(0, 0),
                stringScaleNumber,
                SpriteEffects.None,
                0
            );

            // Draw equation
            var stringSizeEquation = _font.MeasureString(_currentEquation);
            var desiredHeightEquation = 0.2 * ScreenManager.GetHeight();
            var stringScaleEquation = (float)desiredHeightEquation / stringSizeEquation.Y;
            spriteBatch.DrawString(
                _font,
                _currentEquation,
                new Vector2(
                    (int)(0.5 * ScreenManager.GetWidth() - 0.5 * stringSizeEquation.X * stringScaleEquation),
                    (int)(0.30 * ScreenManager.GetHeight())
                ),
                Color.Black,
                0,
                new Vector2(0, 0),
                stringScaleEquation,
                SpriteEffects.None,
                0
            );

            // Draw rectangle that indicates time left
            var relativeTimeLeft = GetRelativeTimeLeft();
            var timeRectangle = new Rectangle(
                (int)(0.5 * ScreenManager.GetWidth() - relativeTimeLeft * 0.25 * ScreenManager.GetWidth()),
                (int)(0.55 * ScreenManager.GetHeight()),
                (int)(relativeTimeLeft * 0.5 * ScreenManager.GetWidth()),
                (int)(0.05 * ScreenManager.GetHeight())
            );
            spriteBatch.Draw(_blankTexture, timeRectangle, Color.White);

            // Draw a rectangle with score for each player
            for (var i = 0; i < PlayerManager.Instance.Players.Count; i++)
            {
                Player player = PlayerManager.Instance.Players[i];
                var destinationRect = new Rectangle(
                    (int)(i * 0.25 * ScreenManager.GetWidth()),
                    (int)(0.75 * ScreenManager.GetHeight()),
                    (int)(0.25 * ScreenManager.GetWidth()),
                    (int)(0.25 * ScreenManager.GetHeight())
                );
                var colorMultiplier = 1.0f;
                if (_gameState != GameState.InGame && player == _lastResponder)
                {
                    colorMultiplier = (float)(1 + Math.Sin(GetRelativeTimeLeft() * 15));
                }
                spriteBatch.Draw(_blankTexture, destinationRect, Color.Multiply(player.Color, colorMultiplier));
 
                // Draw score string
                var score =  _mathplayers[i].score.ToString();
                if (_gameState.Equals(GameState.Winner) && _mathplayers[i].score.ToString().Equals(_scoreToWin.ToString()))
                {
                    score = "Winner!";
                }
                var scoreStringSize = _font.MeasureString(score);
                var desiredScoreHeight = 0.125 * ScreenManager.GetHeight();
                var scoreStringScale = (float)desiredScoreHeight / scoreStringSize.Y;
                spriteBatch.DrawString(
                    _font,
                    score,
                    new Vector2(
                        (int)((i + 0.5) * 0.25 * ScreenManager.GetWidth() - 0.5 * scoreStringSize.X * scoreStringScale),
                        (int)(0.825 * ScreenManager.GetHeight())
                    ),
                    Color.White,
                    0,
                    new Vector2(0, 0),
                    scoreStringScale,
                    SpriteEffects.None,
                    0
                );
            }

            spriteBatch.End();
        }

        public override void NotifyDone(PlayerIndex winnerIndex)
        {
            base.NotifyDone(winnerIndex);
        }

    }
}
