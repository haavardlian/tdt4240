using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using tdt4240.Boards;

namespace tdt4240.Menu
{
    class MinigameWinnerScreen : GameScreen
    {

        private SpriteFont _font;
        private SpriteFont _titleFont;
        private Background _background;

        private Vector2 _titlePosition;
        private Vector2 _playerPosition;
        private Vector2 _powerUpPosition;

        private readonly Player _player;
        private PowerUp _powerUp;
         
        public MinigameWinnerScreen(Player player)
        {
            _player = player;
        }

        void HandlePowerUpResult(PowerUp powerUp)
        {
            _player.AddPowerUp(powerUp);
            _powerUp = powerUp;
        }

        public override void Activate(bool instancePreserved)
        {
            base.Activate(instancePreserved);

            if (!instancePreserved)
            {

                _font = ScreenManager.Font;
                _titleFont = ScreenManager.TitleFont;
                _background = new Background("background3");
                ScreenManager.AddScreen(_background, null);

               
            }
        }

        public override void Added()
        {
            PowerUpRoll roll = new PowerUpRoll(HandlePowerUpResult);
            ScreenManager.AddScreen(roll, _player.PlayerIndex);
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            foreach(Player player in PlayerManager.Instance.Players)
            {
                if (player.Input.IsButtonPressed(GameButtons.A) || player.Input.IsButtonPressed(GameButtons.Start))
                {
                    _powerUp = null;
                    _background.ExitScreen();
                    ExitScreen();
                    if(PlayerManager.Instance.GetPlayer(PlayerIndex.One).PowerUps.Count > 0)
                        ScreenManager.AddScreen(new ItemSelectScreen(), PlayerIndex.One);
                    else
                    {
                        ScreenManager.AddScreen(new DiceRoll(ScreenManager.Board.HandleDiceRollResult), PlayerIndex.One);                      
                    }
                }

            }
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            float center = (ScreenManager.MaxWidth*ScreenManager.GetScalingFactor())/2;

            _titlePosition.X = center - _titleFont.MeasureString("Winner").X/2;
            _titlePosition.Y = 20;

            _playerPosition.X = center - _font.MeasureString("Player " + _player.PlayerIndex).X/2;
            _playerPosition.Y = 100;

            _powerUpPosition.X = center - _font.MeasureString("Reward: " + _powerUp).X / 2;
            _powerUpPosition.Y = 150;
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            spriteBatch.DrawString(_titleFont, "Winner", _titlePosition, new Color(192, 192, 192) * TransitionAlpha);

            spriteBatch.DrawString(_font, "Player " + _player.PlayerIndex, _playerPosition, _player.Color);
            spriteBatch.DrawString(_font, "Reward: " + _powerUp, _powerUpPosition, Color.Yellow);

            spriteBatch.End();
        }
    }
}
