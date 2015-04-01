using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tdt4240.Assets;

namespace tdt4240.Minigames.BirdHunt
{
    class Gun :GraphicsObject
    {
        public const int AccuracyLimit = 2;

        public Player Player { get; set; }

        public Color Color { get; set; }

        public Texture2D Shot { get; set; }

        public Vector2 Accuracy { get; private set; }

        public int Score { get; set; }
        /// <summary>
        /// The corner which this player belongs to, this is where the score will be displayed
        /// </summary>
        public Vector2 Corner { get; set; }



        private int _shotFramCount;

        public Gun(Player player, Texture2D crossHair, Texture2D shot) : base(crossHair)
        {
            
            Player = player;
            Color = player.Color;

            DrawColorAndSetSprite(crossHair);
            Shot = shot;
            Position = new Vector2(200, 200);
            Score = 0;

        }

        public bool Fired
        {
            get
            {
                if(_shotFramCount>=0)
                    _shotFramCount--;
                return _shotFramCount >= 0;
            }
        }

        private void DrawColorAndSetSprite(Texture2D sprite)
        {
            var data = new Color[sprite.Height * sprite.Width];
            sprite.GetData(data);

            for (var i = 0; i < data.Length; i++)
            {
                if (data[i].Equals(Color.Black))
                    data[i] = Color;
            }
            sprite.SetData(data);
            Texture = sprite;
        }

        public void UpdateAccuracy()
        {
            var current = Accuracy;
            current.X += Random.Next(-1, 2);
            if (current.X > AccuracyLimit)
                current.X = AccuracyLimit;
            else if (current.X < AccuracyLimit*-1)
                current.X = AccuracyLimit*-1;

            current.Y += Random.Next(-1, 2);
            if (current.Y > AccuracyLimit)
                current.Y = AccuracyLimit;
            else if (current.Y < AccuracyLimit * -1)
                current.Y = AccuracyLimit * -1;

            Accuracy = current;
        }

        public bool Fire()
        {
            if (Fired)
                return false;
            _shotFramCount = 10;
            return true;
        }
    }
}
