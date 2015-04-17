using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tdt4240.Assets;

namespace tdt4240.Minigames.AvoidObstacles
{
    class Obstacle : GraphicsObject
    {

        public const int SpeedLimit = 4;

        /// <summary>
        /// Sets the position of the object, and makes sure its within the screen
        /// </summary>
        public override Vector2 Position
        {
            get { return _position; }
            set
            {

                _position.X = value.X;
                if (value.Y + Center.Y > 0 && value.Y + Center.Y < ScreenManager.MaxHeight)
                    _position.Y = value.Y;

                Bounds = new Rectangle((int)_position.X, (int)_position.Y, Texture.Width, Texture.Height);

            }
        }

        public Obstacle(Texture2D texture, Vector2 position) : base(texture,position)
        {
            CrashedPlayers = new List<PlayerObject>();
        }

        public Vector2 Speed { get; set; }

        public List<PlayerObject> CrashedPlayers { get; private set; }

        public void UpdateSpeed()
        {
            var current = Speed;
            current.X += Random.Next(-3, 2);
            if (current.X > SpeedLimit)
                current.X = SpeedLimit;
            else if (current.X < SpeedLimit * -1)
                current.X = SpeedLimit * -1;

            current.Y += Random.Next(-2, 3);
            if (current.Y > SpeedLimit)
                current.Y = SpeedLimit;
            else if (current.Y < SpeedLimit * -1)
                current.Y = SpeedLimit * -1;

            Speed = current;
        }
    }
}
