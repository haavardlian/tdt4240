using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using tdt4240.Boards;
using Microsoft.Xna.Framework.Content;

namespace tdt4240.Menu
{
    class MinigameWinnerScreen : GameScreen
    {

        ContentManager content;
        private SpriteFont _font;
        private Background _background;


        private String _winnerText;
        private String _powerUpText;
        private Vector2 _winnerPosition;
        private Vector2 _powerUpPosition;
        

        private Player _player;
         
        public MinigameWinnerScreen(Player player, PowerUp powerUp)
        {
            _player = player;

            _winnerText = "Winner is: Player " + player.playerIndex;
            _powerUpText = "Reward: " + powerUp.ToString();

            //TODO set proper position
            _winnerPosition = new Vector2(250, 200);
            _powerUpPosition = new Vector2(250, 300);
        }

        public override void Activate(bool instancePreserved)
        {
            base.Activate(instancePreserved);

            if (!instancePreserved)
            {
                if (content == null)
                    content = new ContentManager(ScreenManager.Game.Services, "Content");

                _font = content.Load<SpriteFont>("fonts/menufont");
                _background = new Background("background2");
                ScreenManager.AddScreen(_background, null);
            }
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            foreach(Player player in PlayerManager.Instance.Players)
            {
                if (player.Input.IsButtonPressed(GameButtons.A) || player.Input.IsButtonPressed(GameButtons.Start))
                {
                    _background.ExitScreen();
                    this.ExitScreen();
                }

            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();
            
            spriteBatch.DrawString(_font, _winnerText, _winnerPosition, _player.color);
            spriteBatch.DrawString(_font, _powerUpText, _powerUpPosition, Color.Yellow);

            spriteBatch.End();
        }
    }
}
