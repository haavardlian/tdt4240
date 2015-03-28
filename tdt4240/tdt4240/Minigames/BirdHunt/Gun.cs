using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tdt4240.Minigames.BirdHunt
{
    class Gun
    {
        public const int AccuracyLimit = 2;
        private Vector2 _position;

        public Vector2 Position
        {
            get { return _position; }
            set
            {
                if (value.X + (CrossHair.Width/2) > 0 && value.X + (CrossHair.Width/2) < ScreenManager.MaxWidt)
                    _position.X = value.X;
                if (value.Y + (CrossHair.Height / 2) > 0 && value.Y + (CrossHair.Height / 2) < ScreenManager.MaxHeight)
                    _position.Y = value.Y;
            }
        }

        public Player Player { get; set; }

        public Color Color { get; set; }

        public Texture2D CrossHair { get; set; }
        public Texture2D Shot { get; set; }

        public Vector2 Accuracy { get; private set; }

        private readonly Random _random;

        private int _shotFramCount;

        public Gun(Player player, Texture2D crossHair, Texture2D shot)
        {
            
            Player = player;
            _random = new Random();
            Color = player.color;

            DrawColorAndSetSprite(crossHair);
            Shot = shot;
            Position = new Vector2(200, 200);

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
            CrossHair = sprite;
        }

        public void UpdateAccuracy()
        {
            var current = Accuracy;
            current.X += _random.Next(-1, 2);
            if (current.X > AccuracyLimit)
                current.X = AccuracyLimit;
            else if (current.X < AccuracyLimit*-1)
                current.X = AccuracyLimit*-1;

            current.Y += _random.Next(-1, 2);
            if (current.Y > AccuracyLimit)
                current.Y = AccuracyLimit;
            else if (current.Y < AccuracyLimit * -1)
                current.Y = AccuracyLimit * -1;

            Accuracy = current;
        }

        public void Fire()
        {
            _shotFramCount = 60;
        }
    }
}
