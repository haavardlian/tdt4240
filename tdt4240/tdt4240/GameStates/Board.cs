using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tdt4240
{
    class Board : GameScreen
    {
        Texture2D backgroundTexture;
        ContentManager content;
        SpriteFont font;
        List<Texture2D> playerPieces = new List<Texture2D>();
        Vector2[] offsets = new Vector2[] { new Vector2(-25, -25), new Vector2(25, -25), new Vector2(-25, 25), new Vector2(25, 25) };

        public void MiniGameDone(int winnerIndex)
        {
            //TODO
            //Aword winner
            //Remove minigame
        }

        public override void Activate(bool instancePreserved)
        {
            if (!instancePreserved)
            {
                if (content == null)
                    content = new ContentManager(ScreenManager.Game.Services, "Content");

                font = content.Load<SpriteFont>("menufont");
                LoadContent();
            }
        }

        public void LoadContent()
        {
            System.Diagnostics.Debug.WriteLine(ScreenManager.GetScalingFactor());
            backgroundTexture = content.Load<Texture2D>("board");
            for(int i = 0; i < PlayerManager.Instance.NumberOfPlayers; i++)
            {
                playerPieces.Add(content.Load<Texture2D>("piece"));
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            spriteBatch.Draw(backgroundTexture, Vector2.Zero, null, new Color(TransitionAlpha, TransitionAlpha, TransitionAlpha), 0f, Vector2.Zero, ScreenManager.GetScalingFactor(), SpriteEffects.None, 0f);

            int i = 0;
            

            foreach (Texture2D piece in playerPieces)
            {
                Vector2 pos = new Vector2(78, 1004);
                pos += offsets[i];
                pos *= ScreenManager.GetScalingFactor();

                spriteBatch.Draw(piece, pos, null, PlayerManager.Instance.Players[i].color, 0f, new Vector2(20, 10), ScreenManager.GetScalingFactor(), SpriteEffects.None, 0f);
                i++;
            }
            
            
            spriteBatch.End();
        }
    }
}
