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
        private int _knockBack;

        public PlayerObject(Player player, Texture2D playerTexture, Vector2 position) : base(playerTexture,position)
        {
            Player = player;
            Color = player.Color;
            Score = 0;
            _knockBack = 0;
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
            set { _knockBack = value; }
        }
    }
}
