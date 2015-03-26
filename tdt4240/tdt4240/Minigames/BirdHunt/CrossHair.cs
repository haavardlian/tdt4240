using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tdt4240.Minigames.BirdHunt
{
    class CrossHair : Texture2D
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vector2 Position { get; set; }

        public Player Owner { get; set; }

        public Color Color { get; set; }

        private CrossHair(GraphicsDevice graphicsDevice, int width, int height) : base(graphicsDevice, width, height)
        {
        }

        public CrossHair(Texture2D sprite, Player player) : this(sprite.GraphicsDevice,sprite.Width,sprite.Height)
        {
            X = 50;
            Y = 50;
            Position = new Vector2(50,50);
            Color = player.color;
        }

        public Vector2 GetPosition()
        {
            return new Vector2(X,Y);
        }
    }
}
