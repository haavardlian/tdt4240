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

        protected Vector2 _position;

        /// <summary>
        /// Sets the position of the object, and makes sure its within the screen
        /// </summary>
        public virtual Vector2 Position
        {
            get { return _position; }
            set
            {
                if (value.X + Center.X > 0 && value.X + Center.X < ScreenManager.MaxWidth)
                    _position.X = value.X;
                if (value.Y + Center.Y > 0 && value.Y + Center.Y < ScreenManager.MaxHeight)
                    _position.Y = value.Y;

                Bounds = new Rectangle((int)_position.X, (int)_position.Y, Texture.Width, Texture.Height);
            }
        }
        public Rectangle Bounds { get; set; }

        public Vector2 Center { get; set; }
        public Texture2D Texture { get; set; }

        protected readonly Random Random;


        public GraphicsObject(Texture2D texture, Vector2 position )
        {
            Texture = texture;
            Center = new Vector2(texture.Width / 2, texture.Height / 2);
            Random = new Random();
            _position = position;
            Bounds = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);

        }

        public bool Contains(Vector2 point)
        {
            return ((point.X > Position.X && point.X < Position.X + Texture.Width) &
                (point.Y > Position.Y && point.Y < Position.Y + Texture.Height));
        }
    }


}
