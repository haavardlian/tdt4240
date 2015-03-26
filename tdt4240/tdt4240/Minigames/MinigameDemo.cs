using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tdt4240.Minigames
{
    class MinigameDemo : MiniGame
    {

        private int numberOfPlayers;
        
        
        private SpriteFont font;
        private Vector2[] textPosition = new Vector2[4];

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
            foreach(Player player in PlayerManager.Instance.Players)
            {
                textPosition[(int)player.playerIndex] += player.Input.GetThumbstickVector();

                if (player.Input.IsButtonPressed(GameButtons.X))
                {
                    ScreenManager.AddScreen(new DiceRoll(7, this, player), player.playerIndex);
                }

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

            
            foreach (Player player in PlayerManager.Instance.Players)
            {
                spriteBatch.DrawString(font, player.TestString, textPosition[(int)player.playerIndex], player.color);
            }
            spriteBatch.End();
        }

        public override void ResultHandler(Player player, int result)
        {
            System.Diagnostics.Debug.WriteLine(player.playerIndex + " rolled a " + result);
        }

    }
}
