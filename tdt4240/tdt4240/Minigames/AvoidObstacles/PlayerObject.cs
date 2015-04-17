using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tdt4240.Assets;

namespace tdt4240.Minigames.AvoidObstacles
{
    class PlayerObject : GraphicsObject
    {
        public const int KnockbackValue = 5;
        public const int MaxSpeed = 5;
        private int _knockBack;
        private Vector2 _speed;

        public Vector2 Speed
        {
            get
            {
                return _speed;
            }
            set
            {
                _speed = value;
                if (Speed.X > MaxSpeed)
                    _speed.X = MaxSpeed;
                else if (Speed.X < -MaxSpeed)
                    _speed.X = -MaxSpeed;
                if (Speed.Y > MaxSpeed)
                    _speed.Y = MaxSpeed;
                else if (Speed.Y < -MaxSpeed)
                    _speed.Y = -MaxSpeed;
            }
        }

        public PlayerObject(Player player, Texture2D playerTexture, Vector2 position) : base(playerTexture,position)
        {
            Player = player;
            Color = player.Color;
            Score = 0;
            _knockBack = 0;
            Speed = new Vector2(0,0);
        }

        public Vector2 Corner { get; set; }
        public Player Player { get; set; }
        public int Score { get; set; }
        public Color Color { get; set; }

        public int KnockBack
        {
            get
            {
                if (_knockBack > 0)
                {
                    _knockBack--;
                }
                return _knockBack;
            }
            set
            {
                _knockBack = value;
                Speed = new Vector2(Speed.X,0);
            }
        }
    }
}
