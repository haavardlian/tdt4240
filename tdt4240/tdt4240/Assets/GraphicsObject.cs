using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tdt4240.Assets
{
    class GraphicsObject
    {

        private Vector2 _position;

        /// <summary>
        /// Sets the position of the object, and makes sure its within the screen
        /// </summary>
        public Vector2 Position
        {
            get { return _position; }
            set
            {
                if (value.X + Center.X > 0 && value.X + Center.X < ScreenManager.MaxWidth)
                    _position.X = value.X;
                if (value.Y + Center.Y > 0 && value.Y + Center.Y < ScreenManager.MaxHeight)
                    _position.Y = value.Y;
            }
        }

        public Vector2 Center { get; set; }
        public Texture2D Texture { get; set; }

        protected readonly Random Random;


        public GraphicsObject(Texture2D texture )
        {
            Texture = texture;
            Center = new Vector2(texture.Width / 2, texture.Height / 2);
            Random = new Random();

        }

        public bool Contains(Vector2 point)
        {
            return ((point.X > Position.X && point.X < Position.X + Texture.Width) &
                (point.Y > Position.Y && point.Y < Position.Y + Texture.Height));
        }

    }


}
