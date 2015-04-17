using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tdt4240.Minigames.MusicMania
{
    class Arrow
    {
        private Vector2 _position;
        private readonly Texture2D _sprite;
        private const int SpriteWidth = 160;
        private const int SpriteHeight = 160;
        private const float ScreenHeightsPerSecond = 0.9f;
        private readonly String _direction;
        private float _rotation;
        private readonly Rectangle _sourceRectangle;
        private readonly Vector2 _origin;
        private readonly TimeSpan _time;
        private readonly MusicMania _game;
        private readonly Player _player;
        public static float SizeFactor = 0.15f;
        public ArrowScore Score { get; set; }
        private TimeSpan _timeDiff;
        public const float MissedThreshold = 0.25f;
        private float _opacity;
        private DateTime _timeResponded;

        public Arrow(MusicMania game, Player player, Texture2D sprite, String direction, TimeSpan time)
        {
            _game = game;
            _player = player;
            _sprite = sprite;
            _direction = direction;
            _time = time;
            _sourceRectangle = new Rectangle(0, 0, SpriteWidth, SpriteHeight);
            _origin = new Vector2((float)0.5 * SpriteWidth, (float)0.5 * SpriteHeight);
            InitRotation();
            InitPosition();
            Score = ArrowScore.NotSet;
            _opacity = 1;
        }

        private void InitRotation()
        {
            switch (_direction)
            {
                case "R":
                    _rotation = 0;
                    break;
                case "U":
                    _rotation = (float)(3 * Math.PI / 2);
                    break;
                case "L":
                    _rotation = (float)Math.PI;
                    break;
                case "D":
                    _rotation = (float)(Math.PI / 2);
                    break;
                default:
                    _rotation = 0;
                    break;
            }
        }

        private void InitPosition()
        {
            _position = new Vector2(
                (float)((int)_player.PlayerIndex * 0.25 * ScreenManager.MaxWidth),
                -1000
                );
            switch (_direction)
            {
                case "R":
                    _position.X += (float)(0.06125 * ScreenManager.MaxWidth);
                    break;
                case "L":
                    _position.X -= (float)(0.06125 * ScreenManager.MaxWidth);
                    break;
            }
        }

        public void Update()
        {
            if (Score == ArrowScore.NotSet || Score == ArrowScore.Missed)
            {
                UpdateVerticalPosition();
            }
            else
            {
                var timeSinceResponse = DateTime.Now - _timeResponded;
                _opacity = (float)Math.Max(1 - 2 * timeSinceResponse.TotalSeconds, 0);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                _sprite,
                new Rectangle(
                    (int)((_position.X + 0.125 * ScreenManager.MaxWidth) * ScreenManager.Instance.GetScalingFactor()),
                    (int)(_position.Y + 0.5 * SizeFactor * ScreenManager.Instance.GetHeight()),
                    (int)(SizeFactor * ScreenManager.Instance.GetHeight()),
                    (int)(SizeFactor * ScreenManager.Instance.GetHeight())
                    ),
                _sourceRectangle,
                Color.White * _opacity,
                _rotation,
                _origin,
                SpriteEffects.None,
                0
            );
        }

        private void UpdateVerticalPosition()
        {
            _position.Y = (float)(
                ScreenManager.Instance.GetHeight() * MusicMania.RelativeTargetOffset
                + GetTimeDiff().TotalSeconds * ScreenManager.Instance.GetHeight() * ScreenHeightsPerSecond
                );
        }

        public TimeSpan GetTimeDiff()
        {
            return _game.TimeSinceMusicStarted() - _time;
        }

        public TimeSpan GetAbsTimeDiff()
        {
            return GetTimeDiff().Duration();
        }

        public ArrowScore DetermineAndSetScore(String direction)
        {
            if (!direction.Equals(_direction))
            {
                return SetScore(ArrowScore.Missed);
            }
            _timeResponded = DateTime.Now;
            var timeDiffSeconds = GetAbsTimeDiff().TotalSeconds;
            UpdateVerticalPosition();
            if (timeDiffSeconds < 0.04)
            {
                return SetScore(ArrowScore.Marvellous);
            }
            if (timeDiffSeconds < 0.1)
            {
                return SetScore(ArrowScore.Great);
            }
            if (timeDiffSeconds < 0.15)
            {
                return SetScore(ArrowScore.Good);
            }
            if (timeDiffSeconds < 0.2)
            {
                return SetScore(ArrowScore.Ok);
            }
            if (timeDiffSeconds < MissedThreshold)
            {
                return SetScore(ArrowScore.Bad);
            }
            return SetScore(ArrowScore.Missed);
        }

        public ArrowScore SetScore(ArrowScore score)
        {
            if (score == ArrowScore.Missed)
            {
                _opacity = 0.5f;
            }
            Score = score;
            return score;
        }

        public Boolean IsTooFarAway()
        {
            return GetAbsTimeDiff().TotalSeconds > MissedThreshold;
        }
    }
}
