using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tdt4240.Minigames.BirdHunt
{
    class CrossHair
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vector2 Position { get; set; }

        public Player Owner { get; set; }

        public Color Color { get; set; }

        public Texture2D Sprite { get; set; }

        public CrossHair(Player player, Texture2D sprite)
        {
            X = 200;
            Y = 200;
            Position = new Vector2(200,200);
            Owner = player;
            Color = player.color;

            DrawColorAndSetSprite(sprite);

        }

        private void DrawColorAndSetSprite(Texture2D sprite)
        {
            var data = new Color[sprite.Height * sprite.Width];
            sprite.GetData(data);

            for (int i = 0; i < data.Length; i++)
            {
                if (data[i].Equals(Color.Black))
                    data[i] = Color;
            }
            sprite.SetData(data);
            Sprite = sprite;
        }

        public Vector2 GetPosition()
        {
            return new Vector2(X,Y);
        }
    }
}
