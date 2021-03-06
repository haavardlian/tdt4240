﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tdt4240.Boards
{
    class Countdown : GameScreen
    {
        private SpriteFont _font;
        private Vector2 _position;
        private ContentManager _content;
        private int _waitAmount;
        private int _elapsed;
        private int _step = 1000;
        private GameScreen _next;
        private string _text = "Minigame starting";

        private bool _pressed;

        public Countdown(int seconds, GameScreen next)
        {
            _waitAmount = seconds;
            _next = next;
 
        }

        public override void Activate(bool instancePreserved)
        {
            base.Activate(instancePreserved);

            if (!instancePreserved)
            {
                if (_content == null)
                    _content = new ContentManager(ScreenManager.Game.Services, "Content");
                _font = _content.Load<SpriteFont>("fonts/dice");
            }

            _position = new Vector2((ScreenManager.MaxWidth - _font.MeasureString("Minigame starting...").X)/2, 400);
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            _elapsed += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if(_elapsed >= _step)
            {
                _elapsed = 0;
                _waitAmount--;
                _text += '.';
            }

            if (_waitAmount < 1)
            {
                ScreenManager.RemoveScreen(this);
                ScreenManager.AddScreen(_next, null);
            }
                
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            var spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();
            spriteBatch.DrawString(_font, _text, _position*ScreenManager.GetScalingFactor(), Color.Green, 0.0f,
                new Vector2(0, 0), ScreenManager.GetScalingFactor(), SpriteEffects.None, 0.0f);
            spriteBatch.End();
        }
    }
}
