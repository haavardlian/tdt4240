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

        public string StatusText
        {
            get { return statusText; }
            set { statusText = value; }
        }

        string statusText = "Press A to join";

        //bool done = false;

        public Vector2 Position
        {
            get { return position; }
            set { position = value;  }
        }

        Vector2 position;

        int playerId;

        public PlayerSelectStatus(int id)
        {
            playerId = id;
            position = new Vector2();
        }

        public void updatePosition(int width, int heigthModifier, SpriteFont font)
        {
            position.X = width - font.MeasureString(statusText).X - 30;
            position.Y = heigthModifier * playerId + 30;
        }

        public void draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.DrawString(font, statusText, position, Color.White);
        }

    }
}
