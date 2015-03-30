using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace tdt4240
{
    class MinigameIntro : GameScreen
    {
        private SpriteFont _font;
        private ContentManager _content;
        private Background _background;

        protected Dictionary<Buttons, string> ControllerButtons = new Dictionary<Buttons, string>();
        protected Dictionary<Keys, string> KeyboardButtons = new Dictionary<Keys, string>();

        protected String ThumbstickDescription = null;
        protected String GameDescription;

        //TODO immage


        public override void Activate(bool instancePreserved)
        {
            base.Activate(instancePreserved);

            if (!instancePreserved)
            {
                if (_content == null)
                    _content = new ContentManager(ScreenManager.Game.Services, "Content");

                _font = _content.Load<SpriteFont>("fonts/menufont");
                _background = new Background("background2");
                ScreenManager.AddScreen(_background, null);

            }
        }


        public override void HandleInput(GameTime gameTime, InputState input)
        {
            foreach (Player player in PlayerManager.Instance.Players)
            {
                if (player.Input.IsButtonPressed(GameButtons.Start))
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

            spriteBatch.DrawString(_font, "Controller", new Vector2(100, 80), Color.Yellow);

            Vector2 pos = new Vector2(100, 100);

            foreach (KeyValuePair<Buttons, string> entry in ControllerButtons)
            {
                spriteBatch.DrawString(_font, entry.Key + " - " + entry.Value, pos, Color.LightYellow);
                pos.Y += 20;
            }

            spriteBatch.End();
        }


    }
}
