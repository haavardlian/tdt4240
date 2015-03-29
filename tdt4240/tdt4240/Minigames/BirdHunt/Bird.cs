using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tdt4240.Assets;

namespace tdt4240.Minigames.BirdHunt
{
    class Bird : GraphicsObject

    {
        public const int SpeedLimit = 4;


        public Vector2 Speed { get; private set; }

        public Bird(Texture2D texture, Vector2 position) : base(texture)
        {
            Position = position;
        }

        public float getDistanceFactor()
        {
            var v = Position.Y/ScreenManager.MaxHeight;
            return (v/2) + 0.5f;
        }

        public void UpdateSpeed()
        {
            var current = Speed;
            current.X += Random.Next(-2, 3);
            if (current.X > SpeedLimit)
                current.X = SpeedLimit;
            else if (current.X < SpeedLimit * -1)
                current.X = SpeedLimit * -1;

            current.Y += Random.Next(-3, 2);
            if (current.Y > SpeedLimit)
                current.Y = SpeedLimit;
            else if (current.Y < SpeedLimit * -1)
                current.Y = SpeedLimit * -1;

            Speed = current;
        }
    }
}
