using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tdt4240
{
    class PlayerSelectStatus
    {
        float fade;
        public Player Player = null;
        int index = 0;

        string[] statusTexts = new string[] { "Press A to join", "Select color", "Ready" };

        public string StatusText
        {
            get 
            {
                if (Player == null) return statusTexts[0];
                else return statusTexts[2];
            }
          
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value;  }
        }

        Vector2 position;

        public PlayerSelectStatus(int index)
        {
            this.index = index;
            position = new Vector2();
        }

        public void updatePosition(int width, int heigthModifier, SpriteFont font)
        {
            position.X = width - font.MeasureString(StatusText).X - 30;
            position.Y = heigthModifier * index + 30;
        }

        public void draw(SpriteBatch spriteBatch, SpriteFont font, GameTime gameTime)
        {
            if (Player == null)
            {
                float fadeSpeed = (float)gameTime.ElapsedGameTime.TotalSeconds * 4;

                fade = Math.Min(fade + fadeSpeed, 1);

                // Pulsate the size of the selected menu entry.
                double time = gameTime.TotalGameTime.TotalSeconds;

                float pulsate = (float)Math.Sin(time * 6) + 1;

                float scale = 1 + pulsate * 0.05f * fade;

                Vector2 origin = new Vector2(0, font.LineSpacing / 2);

                spriteBatch.DrawString(font, StatusText, position, Color.Yellow, 0,
                       origin, scale, SpriteEffects.None, 0);
            }
            else
            {
                spriteBatch.DrawString(font, StatusText, position, Player.color);

             

            }

        }

    }
}
