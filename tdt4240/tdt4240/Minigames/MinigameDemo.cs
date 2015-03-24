﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using tdt4240.GameStates;

namespace tdt4240.Minigames
{
    class MinigameDemo : MiniGame
    {

        private int numberOfPlayers;
        
        
        private SpriteFont font;
        private Vector2 textPosition = new Vector2(200, 200);

        public MinigameDemo(Board board) : base(board)
        {
            this.supportedPlayers = SupportedPlayers.All;
            this.numberOfPlayers = PlayerManager.Instance.NumberOfPlayers;

            //DO stuff based on the amount of players playing
        }

        public override void Activate(bool instancePreserved)
        {
            base.Activate(instancePreserved);

            if (!instancePreserved)
            {
                font = content.Load<SpriteFont>("menufont");
            }
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            KeyboardState keyboardState = input.CurrentKeyboardStates[0];

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                textPosition.Y--;
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                textPosition.Y++;
            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                textPosition.X--;
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                textPosition.X++;
            }

        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            spriteBatch.DrawString(font, "Test", textPosition, Color.HotPink);

            spriteBatch.End();
        }

    }
}
