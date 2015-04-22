using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tdt4240.Minigames.MusicMania
{
    class MusicManiaPlayer
    {
        public int Score { get; set; }
        private readonly List<Arrow> _arrows;
        private int _nextArrowIndex;
        private readonly Player _player;
        private readonly MusicMania _game;
        private ArrowScore _lastScore;
        private DateTime _lastScoreTime;
        private float _lastScoreOpacity;

        public MusicManiaPlayer(MusicMania game, Player player, List<Arrow> arrows)
        {
            _game = game;
            _player = player;
            Score = 0;
            _arrows = arrows;
            _nextArrowIndex = 0;
        }

        public void Update()
        {
            foreach (var arrow in _arrows)
            {
                arrow.Update();
            }

            var arrowOfInterest = _arrows[_nextArrowIndex];
            if (arrowOfInterest.GetTimeDiff().TotalSeconds > Arrow.MissedThreshold && arrowOfInterest.Score == ArrowScore.NotSet)
            {
                arrowOfInterest.SetScore(ArrowScore.Missed);
                AddScore(ArrowScore.Missed);
                IncrementCurrentArrowIndex();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var arrow in _arrows)
            {
                arrow.Draw(spriteBatch);
            }

            // Draw total score string
            var scoreStringSize = _game.Font.MeasureString(Score.ToString());
            var desiredScoreHeight = 0.125 * ScreenManager.Instance.GetHeight();
            var scoreStringScale = (float)desiredScoreHeight / scoreStringSize.Y;
            spriteBatch.DrawString(
                _game.Font,
                Score.ToString(),
                new Vector2(
                    (int)(((int)_player.PlayerIndex + 0.5) * 0.25 * ScreenManager.Instance.GetWidth() - 0.5 * scoreStringSize.X * scoreStringScale),
                    (int)(0.875 * ScreenManager.Instance.GetHeight())
                ),
                Color.White,
                0,
                new Vector2(0, 0),
                scoreStringScale,
                SpriteEffects.None,
                0
            );

            var timeSinceLastScore = DateTime.Now - _lastScoreTime;
            if (timeSinceLastScore.TotalSeconds < 1)
            {
                if (_lastScore == ArrowScore.Marvellous)
                {
                    _lastScoreOpacity = (float)(0.75 + 0.25 * Math.Cos(timeSinceLastScore.TotalSeconds * 60));
                }
                else
                {
                    _lastScoreOpacity = (float)Math.Max(1 - timeSinceLastScore.TotalSeconds, 0);
                }

                // Draw last score string
                var lastScoreStringSize = _game.Font.MeasureString(_lastScore.ToString());
                var desiredLastScoreHeight = 0.125 * ScreenManager.Instance.GetHeight();
                var lastScoreStringScale = (float)desiredLastScoreHeight / lastScoreStringSize.Y;
                spriteBatch.DrawString(
                    _game.Font,
                    _lastScore.ToString(),
                    new Vector2(
                        (int)(((int)_player.PlayerIndex + 0.5) * 0.25 * ScreenManager.Instance.GetWidth() - 0.5 * lastScoreStringSize.X * lastScoreStringScale),
                        (int)(0.425 * ScreenManager.Instance.GetHeight())
                    ),
                    Color.White * _lastScoreOpacity,
                    0,
                    new Vector2(0, 0),
                    lastScoreStringScale,
                    SpriteEffects.None,
                    0
                );
            }
        }

        public void HandleInput(InputState input)
        {
            var pressedDirection = GetPressedDirection();
            if (pressedDirection == null) return;
            Console.WriteLine(pressedDirection);
            var targetedArrow = GetTargetedArrow();
            if (targetedArrow.IsTooFarAway()) return;
            var score = targetedArrow.DetermineAndSetScore(pressedDirection);
            AddScore(score);
            IncrementCurrentArrowIndex();
        }

        private void IncrementCurrentArrowIndex()
        {
            if (_nextArrowIndex < _arrows.Count - 1)
            {
                _nextArrowIndex++;
            }
        }

        private String GetPressedDirection()
        {
            if (_player.Input.IsButtonPressed(GameButtons.Up))
            {
                return "U";
            }
            if (_player.Input.IsButtonPressed(GameButtons.Down))
            {
                return "D";
            }
            if (_player.Input.IsButtonPressed(GameButtons.Left))
            {
                return "L";
            }
            if (_player.Input.IsButtonPressed(GameButtons.Right))
            {
                return "R";
            }
            return null;
        }

        private Arrow GetTargetedArrow()
        {
            return _arrows[_nextArrowIndex];
        }

        public void AddScore(ArrowScore score)
        {
            Score += (int)score;
            _lastScore = score;
            _lastScoreTime = DateTime.Now;
        }
    }
}
