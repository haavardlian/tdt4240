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
                if (value.X + Center.X > 0 && value.X + Center.X < ScreenManager.MaxWidt)
                    _position.X = value.X;
                if (value.Y + Center.Y > 0 && value.Y + Center.Y < ScreenManager.MaxHeight)
                    _position.Y = value.Y;
            }
        }

        public Vector2 Center { get; set; }
        public Texture2D Texture { get; set; }


        public GraphicsObject(Vector2 center)
        {
            Center = center;
        }

    }


}
