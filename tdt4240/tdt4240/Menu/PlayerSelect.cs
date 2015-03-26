using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using tdt4240.Minigames;

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
            for (int i = 0; i < PlayerManager.MaxPlayers; i++)
            {
                playerSelectStatuses.Add(new PlayerSelectStatus(i));
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

            for (int i = 0; i < 4; i++)
            {
                GamePadState gamePadState = input.CurrentGamePadStates[i];

                if (gamePadState.Buttons.A == ButtonState.Pressed)
                {
                    AddPlayer(i, InputType.Controller);
                    playerSelectStatuses[PlayerManager.Instance.NumberOfPlayers - 1].Player = PlayerManager.Instance.LatestPlayer();
                }
                if (gamePadState.Buttons.B == ButtonState.Pressed)
                {
                    RemovePlayer((PlayerIndex)i);
                }

                if (gamePadState.Buttons.Start == ButtonState.Pressed)
                {
                    startGame();
                }
            }

            KeyboardState keyboardState = input.CurrentKeyboardStates[0];

            if (keyboardState.IsKeyDown(Keys.A))
            {
                AddPlayer(-1, InputType.Keyboard);
                playerSelectStatuses[PlayerManager.Instance.NumberOfPlayers-1].Player = PlayerManager.Instance.LatestPlayer();
            }
            if (keyboardState.IsKeyDown(Keys.Back))
            {
                RemovePlayer((PlayerIndex)0);
            }
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                startGame();
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

        private void AddPlayer(int controllerIndex, InputType type)
        {
            PlayerManager.Instance.AddPlayer(controllerIndex, type);
        }

        private void RemovePlayer(PlayerIndex playerIndex)
        {
            if (PlayerManager.Instance.PlayerExists(playerIndex))
            {
                PlayerManager.Instance.RemovePlayer(playerIndex);
            }
            else
            {
                //If the player that entered the player select screen backs the game will return to the main menu
                if (playerIndex == ControllingPlayer.Value)
                {
                    ScreenManager.RemoveScreen(this);
                }
            }
        }

        private void startGame()
        {
            if (PlayerManager.Instance.NumberOfPlayers >= 1)
            {
                //TODO start the game
                ScreenManager.RemoveScreen(this);

                Board board = new Board();
                ScreenManager.AddScreen(board, null);
               // ScreenManager.AddScreen(new MinigameDemo(board), null);
                
                //Add game screen
            }
        }
    }
}
