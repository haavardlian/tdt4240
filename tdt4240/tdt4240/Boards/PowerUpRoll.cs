using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace tdt4240.Boards
{
    class PowerUpRoll : GameScreen
    {
        private List<PowerUp> _powerUps = Board.PowerUps();
        private readonly Action<PowerUp> _callback;
        private bool _pressed;
        private ContentManager _content;
        private PowerUp _currentPowerUp;
        private int _elapsed;
        private int _step = 100;
        private int _n;
        private int time = 400;
        private int _wait;

        public PowerUpRoll(Action<PowerUp> callback)
        {
            _callback = callback;
        }

        public override void Activate(bool instancePreserved)
        {
            base.Activate(instancePreserved);
            if (ControllingPlayer == null) throw new Exception("PowerUpRoll must have a controlling player");
            IsPopup = true;
            if (!instancePreserved)
            {
                if (_content == null)
                    _content = new ContentManager(ScreenManager.Game.Services, "Content");

                _currentPowerUp = _powerUps.FirstOrDefault();
            }
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            _elapsed += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (_pressed)
            {
                _step += 2;
            }

            if (_step >= time)
            {
                
                _wait += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (_wait > 1000)
                {
                    _callback(_currentPowerUp);
                    ScreenManager.RemoveScreen(this);
                }

            }
            else if (_elapsed > _step)
            {
                _n++;
                if (_n >= _powerUps.Count) _n = 0;
                _elapsed = 0;
            }

            _currentPowerUp = _powerUps[_n];

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            var player = PlayerManager.Instance.GetPlayer(ControllingPlayer);
            if (player.Input.IsButtonPressed(GameButtons.A))
            {
                _pressed = true;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            var spriteBatch = ScreenManager.SpriteBatch;

            Texture2D icon = AssetManager.Instance.GetAsset<Texture2D>(_currentPowerUp.IconPath);

            spriteBatch.Begin();
            spriteBatch.Draw(icon, new Vector2(ScreenManager.MaxWidth / 2, ScreenManager.MaxHeight / 2) * ScreenManager.GetScalingFactor(), null, Color.White, 0f,
                new Vector2(icon.Width / 2, icon.Height / 2) * ScreenManager.GetScalingFactor(), ScreenManager.GetScalingFactor() * 0.5f, SpriteEffects.None, 0f);
            spriteBatch.End();
        }
    }
}
