using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace tdt4240.Boards
{
    class DiceRoll : GameScreen
    {
        private SpriteFont _font;
        private readonly Vector2 _position = new Vector2(350, 120);
        private ContentManager _content;
        private readonly Action<int> _callback; 
        private string _text = "";
        private const int Time = 400;
        private int _elapsed;
        private int _step = 100;
        private int _wait;
        int _n;

        private bool _pressed;

        public DiceRoll(Action<int> callback)
        {
            _callback = callback;
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            var player = PlayerManager.Instance.GetPlayer(ControllingPlayer);
            if (player.Input.IsButtonPressed(GameButtons.A))
            {
                _pressed = true;
            }
        }

        public override void Activate(bool instancePreserved)
        {
            base.Activate(instancePreserved);

            if(ControllingPlayer == null) throw new Exception("DiceRoll must have a controlling player");

            if (!instancePreserved)
            {
                if (_content == null)
                    _content = new ContentManager(ScreenManager.Game.Services, "Content");
                _font = _content.Load<SpriteFont>("fonts/dice");
            }
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            _elapsed += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if(_pressed)
            {
                _step += 2;
            }

            if(_step >= Time)
            {
                _text = "" + _n;
                _wait += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

                if(_wait > 1000)
                {
                    _callback(_n);
                    ScreenManager.RemoveScreen(this);
                }

            }
            else if (_elapsed > _step)
            {
                _n++;
                if (_n >= 7) _n = 1;
                _text = "" + _n;
                _elapsed = 0;
            }
                
            

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            var spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();
            spriteBatch.DrawString(_font, _text, _position, Color.HotPink);
            spriteBatch.End();
        }
    }
}
