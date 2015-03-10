using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tdt4240
{
    class PlayerSelect : GameScreen
    {
        ContentManager content;
        SpriteFont font;
        int screenHeigthModifier;

        Vector2 playerOneStatusPosition;

        List<PlayerSelectStatus> players = new List<PlayerSelectStatus>();

        public PlayerSelect()
        {
            for (int i = 0; i < 4; i++ )
                players.Add(new PlayerSelectStatus(i));
  
        }

        public override void Activate(bool instancePreserved)
        {
            if (!instancePreserved)
            {
                if (content == null)
                    content = new ContentManager(ScreenManager.Game.Services, "Content");

                font = content.Load<SpriteFont>("menufont");

                screenHeigthModifier = ScreenManager.GraphicsDevice.Viewport.Height / 4;

            }
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            foreach(PlayerSelectStatus player in players)
            {
                player.updatePosition(ScreenManager.GraphicsDevice.Viewport.Width, screenHeigthModifier, font);
            }

        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            foreach (PlayerSelectStatus player in players)
            {
                player.updatePosition(ScreenManager.GraphicsDevice.Viewport.Width, screenHeigthModifier, font);
            }

            foreach (PlayerSelectStatus player in players)
            {
                player.draw(spriteBatch, font);
            }

            spriteBatch.End();
        }

    }
}
