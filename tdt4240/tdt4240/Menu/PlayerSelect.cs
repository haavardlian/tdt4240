using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace tdt4240
{
    class PlayerSelect : GameScreen
    {
        ContentManager content;
        SpriteFont font;
        int screenHeigthModifier;
        float fade;

        List<PlayerSelectStatus> playerSelectStatuses = new List<PlayerSelectStatus>();

        public PlayerSelect()
        {      
            foreach (Player player in PlayerManager.Instance.Players)
            {
                playerSelectStatuses.Add(new PlayerSelectStatus(player));
            }
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

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            // Look up inputs for the active player profile.

            for (int i = 0; i < 4; i++)
            {
                KeyboardState keyboardState = input.CurrentKeyboardStates[i];
                GamePadState gamePadState = input.CurrentGamePadStates[i];

                PlayerManager playerManager = PlayerManager.Instance;

                if (gamePadState.Buttons.A == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.A))
                {
                    if (!playerManager.playerJoined(i))
                    {
                        playerManager.joinPlayer(i);
                    }
                }
                if (gamePadState.Buttons.B == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Back))
                {
                    if (playerManager.playerJoined(i))
                    {
                        playerManager.removePlayer(i);
                    }
                }

                if (gamePadState.Buttons.Start == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Space))
                {
                    if (playerManager.NumberOfPlayers >= 2)
                    {
                        //TODO Start the game
                    }
                }
            }

        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            foreach (PlayerSelectStatus playerSelectStatus in playerSelectStatuses)
            {
                playerSelectStatus.updatePosition(ScreenManager.GraphicsDevice.Viewport.Width, screenHeigthModifier, font);
            }

        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            foreach (PlayerSelectStatus playerSelectStatus in playerSelectStatuses)
            {
                playerSelectStatus.draw(spriteBatch, font, gameTime);
            }

            if (PlayerManager.Instance.NumberOfPlayers >= 2) 
            {

                float fadeSpeed = (float)gameTime.ElapsedGameTime.TotalSeconds * 4;

                fade = Math.Min(fade + fadeSpeed, 1);

                // Pulsate the size of the selected menu entry.
                double time = gameTime.TotalGameTime.TotalSeconds;

                float pulsate = (float)Math.Sin(time * 6) + 1;

                float scale = 1 + pulsate * 0.05f * fade;

                Vector2 origin = new Vector2(0, font.LineSpacing / 2);

                Vector2 position = new Vector2(200, 300);

                spriteBatch.DrawString(font, "Press start to play", position, Color.Yellow, 0,
                origin, scale, SpriteEffects.None, 0);
            }

            spriteBatch.End();
        }

    }
}
