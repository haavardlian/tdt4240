using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tdt4240.Menu
{
    static class PulsatingText
    {

        private static float _fade;

        public static void Draw(SpriteBatch spriteBatch, GameTime gameTime, SpriteFont font, String text, Vector2 position, Color color)
        {
            float fadeSpeed = (float)gameTime.ElapsedGameTime.TotalSeconds * 4;

            _fade = Math.Min(_fade + fadeSpeed, 1);

            // Pulsate the size of the selected menu entry.
            double time = gameTime.TotalGameTime.TotalSeconds;

            float pulsate = (float)Math.Sin(time * 6) + 1;

            float scale = 1 + pulsate * 0.05f * _fade;

            Vector2 origin = new Vector2(0, font.LineSpacing / 2);

            spriteBatch.DrawString(font, text, position, color, 0,
            origin, scale, SpriteEffects.None, 0);
        }

    }
}
