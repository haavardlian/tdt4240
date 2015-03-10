using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];
            GamePadState gamePadState = input.CurrentGamePadStates[playerIndex];

            // The game pauses either if the user presses the pause button, or if
            // they unplug the active gamepad. This requires us to keep track of
            // whether a gamepad was ever plugged in, because we don't want to pause
            // on PC if they are playing with a keyboard and have no gamepad at all!
            bool gamePadDisconnected = !gamePadState.IsConnected &&
                                       input.GamePadWasConnected[playerIndex];

            if (gamePadState.Buttons.A == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.A))
            {
                players[playerIndex].Status = PlayerStatus.Joined;
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
                player.draw(spriteBatch, font, gameTime);
            }

            spriteBatch.End();
        }

    }
}
