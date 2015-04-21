using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using tdt4240.Boards;

namespace tdt4240.Minigames.ColorMatch
{
    class ColorMatch : MiniGame
    {
        public new static SupportedPlayers SupportedPlayers = SupportedPlayers.All;

        private readonly List<Color> _colors;
        private readonly List<String> _colorWords;
        private SpriteFont _font;
        private DateTime _nextWordTime;
        private String _currentWord;
        private Color _currentColor;
        private Texture2D _blankTexture;
        private Dictionary<Player, int> _playerScores;
        private enum GameState { InGame, Correct, Wrong, Winner }
        private GameState _gameState;
        private Player _lastResponder;
        private int _numConsecutiveMisMatches;

        private static readonly Random Rnd = new Random();
        private static readonly TimeSpan MaxTimePerWord = TimeSpan.FromSeconds(1.1);
        private static readonly int winnerThreshold = 3;

        public ColorMatch(Board board)
            : base(board)
        {
            Title = "Color match";
            _gameState = GameState.InGame;
            _numConsecutiveMisMatches = 0;

            _colors = new List<Color>
            {
                Color.HotPink,
                Color.CornflowerBlue,
                Color.Green,
                Color.Red,
                Color.Yellow
            };
            _colorWords = new List<String>
            {
                "Pink",
                "Blue",
                "Green",
                "Red",
                "Yellow"
            };
            _nextWordTime = DateTime.Now;
            ShowNewCombo();
            _playerScores = new Dictionary<Player, int>();
            foreach (Player player in PlayerManager.Instance.Players)
            {
                _playerScores.Add(player, 0);
            }
        }

        private Color GetRandomColor()
        {
            var i = Rnd.Next(_colors.Count);
            return _colors[i];
        }

        private String GetRandomWord()
        {
            var i = Rnd.Next(_colorWords.Count);
            return _colorWords[i];
        }

        private Boolean ColorMatchesWord()
        {
            return _colors.IndexOf(_currentColor) == _colorWords.IndexOf(_currentWord);
        }

        private void ShowNewCombo()
        {
            if (_gameState == GameState.Winner)
            {
                NotifyDone(_lastResponder.PlayerIndex);
            }
            _gameState = GameState.InGame;
            _nextWordTime = DateTime.Now + MaxTimePerWord;
            var previousWord = _currentWord;
            var previousColor = _currentColor;

            while (_currentWord == previousWord && _currentColor == previousColor)
            {
                _currentWord = GetRandomWord();
                _currentColor = GetRandomColor();
            }
            if (_numConsecutiveMisMatches >= 2 && !ColorMatchesWord())
            {
                // if at least two mismatches on a row, double the chance of a match, to keep things interesting
                while (_currentWord == previousWord && _currentColor == previousColor)
                {
                    _currentWord = GetRandomWord();
                    _currentColor = GetRandomColor();
                }
            }
            if (!ColorMatchesWord())
            {
                _numConsecutiveMisMatches++;
            }
        }

        private double GetRelativeTimeLeft()
        {
            var diff = _nextWordTime - DateTime.Now;
            return diff.TotalSeconds / MaxTimePerWord.TotalSeconds;
        }

        private void AddPoints(Player player, int value)
        {
            _playerScores[player] += value;
        }

        private bool IsWinner(Player player)
        {
            return _playerScores[player] >= winnerThreshold;
        }

        public override void Activate(bool instancePreserved)
        {
            base.Activate(instancePreserved);

            if (!instancePreserved)
            {
                MusicPlayer.GetInstance().StartLoopingSong("7");
                Background = new Background("minigames/ColorMatch/background");
                ScreenManager.AddScreen(Background, null);

                _font = content.Load<SpriteFont>("fonts/dice");
                _blankTexture = content.Load<Texture2D>("blank");
            }
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            if (!_gameState.Equals(GameState.InGame)) return;
            foreach (Player player in PlayerManager.Instance.Players.Where(player => player.Input.IsButtonPressed(GameButtons.A)))
            {
                _lastResponder = player;
                if (ColorMatchesWord())
                {
                    AddPoints(player, 1);
                    if (IsWinner(player))
                    {
                        _gameState = GameState.Winner;
                    }
                    else
                    {
                        _gameState = GameState.Correct;
                    }

                }
                else
                {
                    _gameState = GameState.Wrong;
                    AddPoints(player, -1);
                }
                _nextWordTime = DateTime.Now + MaxTimePerWord;
                _numConsecutiveMisMatches = 0;
                break;
            }
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            if (_nextWordTime <= DateTime.Now)
            {
                ShowNewCombo();
            }

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            var spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            // Draw colored word
            var stringSize = _font.MeasureString(_currentWord);
            var desiredHeight = 0.2 * ScreenManager.GetHeight();
            var stringScale = (float)desiredHeight / stringSize.Y;
            spriteBatch.DrawString(
                _font,
                _currentWord,
                new Vector2(
                    (int)(0.5 * ScreenManager.GetWidth() - 0.5 * stringSize.X * stringScale),
                    (int)(0.1 * ScreenManager.GetHeight())
                ),
                _currentColor,
                0,
                new Vector2(0, 0),
                stringScale,
                SpriteEffects.None,
                0
            );

            // Draw rectangle that indicates time left
            var relativeTimeLeft = GetRelativeTimeLeft();
            var timeRectangle = new Rectangle(
                (int)(0.5 * ScreenManager.GetWidth() - relativeTimeLeft * 0.25 * ScreenManager.GetWidth()),
                (int)(0.35 * ScreenManager.GetHeight()),
                (int)(relativeTimeLeft * 0.5 * ScreenManager.GetWidth()),
                (int)(0.05 * ScreenManager.GetHeight())
            );
            spriteBatch.Draw(_blankTexture, timeRectangle, Color.White);


            // Draw a rectangle with score for each player
            for (var i = 0; i < PlayerManager.Instance.Players.Count; i++)
            {
                var player = PlayerManager.Instance.Players[i];
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
                var score = _playerScores[player].ToString();
                if (_gameState.Equals(GameState.Winner) && IsWinner(player))
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

        public override void NotifyDone(PlayerIndex winningPlayerIndex)
        {
            base.NotifyDone(winningPlayerIndex);
        }

    }
}
