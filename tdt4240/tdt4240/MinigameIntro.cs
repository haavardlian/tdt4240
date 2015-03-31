using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using tdt4240.Boards;
using tdt4240.Menu;

namespace tdt4240
{
    class MinigameIntro : GameScreen
    {
        public static SupportedPlayers SupportedPlayers = SupportedPlayers.All;

        private SpriteFont _font;
        private SpriteFont _titleFont;
        private SpriteFont _smallFont;
        private ContentManager _content;
        private Background _background;

        private Vector2 _controllerPosition = new Vector2();
        private Vector2 _keyboardPosition = new Vector2();
        private Vector2 _titlePosition = new Vector2();
        private Rectangle _coverPosition = new Rectangle();
        private Vector2 _descriptionPosition = new Vector2();
        private Vector2 _goalPosition = new Vector2();
        private Vector2 _startPosition = new Vector2();

        protected Dictionary<Buttons, string> ControllerButtons = new Dictionary<Buttons, string>();
        protected Dictionary<Keys, string> KeyboardButtons = new Dictionary<Keys, string>();

        protected Board Board;
        protected String ThumbstickDescription = null;
        protected String GameDescription;
        protected String Goal;
        protected MiniGame MiniGame;
        protected Texture2D Cover;

        //TODO immage

        public MinigameIntro(Board board)
        {
            Board = board;
        }


        public override void Activate(bool instancePreserved)
        {
            base.Activate(instancePreserved);

            if (!instancePreserved)
            {
                if (_content == null)
                    _content = new ContentManager(ScreenManager.Game.Services, "Content");

                _font = ScreenManager.Font;
                _titleFont = ScreenManager.TitleFont;
                _smallFont = _content.Load<SpriteFont>("fonts/smallfont");
                _background = new Background("background3");
                ScreenManager.AddScreen(_background, null);
            }
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            float center = (ScreenManager.MaxWidth*50/100)*ScreenManager.GetScalingFactor();

            _titlePosition.X = center - (_font.MeasureString(MiniGame.ToString()).X/2);
            _titlePosition.Y = 0;

            _coverPosition.X = 20;
            _coverPosition.Y = 70;
            _coverPosition.Width = (int)((ScreenManager.MaxWidth*55/100)*ScreenManager.GetScalingFactor());
            _coverPosition.Height = (int)((ScreenManager.MaxHeight * 55 / 100) * ScreenManager.GetScalingFactor());

            _descriptionPosition.X = 20;
            _descriptionPosition.Y = (ScreenManager.MaxHeight * 70 / 100) * ScreenManager.GetScalingFactor();

            _goalPosition.X = 20;
            _goalPosition.Y = (ScreenManager.MaxHeight*80/100)*ScreenManager.GetScalingFactor();

            _startPosition.X = center - _font.MeasureString("Press Start to play").X/2;
            _startPosition.Y = (ScreenManager.MaxHeight * 90 / 100) * ScreenManager.GetScalingFactor();

            float x = (ScreenManager.MaxWidth * 60 / 100) * ScreenManager.GetScalingFactor();

            _controllerPosition.X = x;
            _controllerPosition.Y = 60;

            _keyboardPosition.X = x;
            _keyboardPosition.Y = (ScreenManager.MaxHeight / 2) * ScreenManager.GetScalingFactor();

        }


        public override void HandleInput(GameTime gameTime, InputState input)
        {
            foreach (Player player in PlayerManager.Instance.Players)
            {
                if (player.Input.IsButtonPressed(GameButtons.Start))
                {
                    _background.ExitScreen();
                    this.ExitScreen();
                    ScreenManager.AddScreen(MiniGame, null);
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            spriteBatch.DrawString(_titleFont, MiniGame.ToString(), _titlePosition, Color.Red);

            spriteBatch.Draw(Cover,_coverPosition, Color.White);

            StringBuilder description = WrapWord(GameDescription, _smallFont,
                (int) ((ScreenManager.MaxWidth*55/100)*ScreenManager.GetScalingFactor()));

            spriteBatch.DrawString(_smallFont, description, _descriptionPosition, Color.White);
            spriteBatch.DrawString(_font, "Goal: " + Goal, _goalPosition, Color.Yellow);

            spriteBatch.DrawString(_font, "Controller", _controllerPosition, Color.Yellow);
            spriteBatch.DrawString(_font, "Keyboard", _keyboardPosition, Color.Yellow);
            PulsatingText.Draw(spriteBatch, gameTime, _font, "Press Start to play", _startPosition, Color.Yellow);

            Vector2 controllerTempPosition = new Vector2(_controllerPosition.X, _controllerPosition.Y + 40);

            if (ThumbstickDescription != null)
            {
                spriteBatch.DrawString(_smallFont, "Thumbstick" + " - " + ThumbstickDescription, controllerTempPosition, Color.LightYellow);
                controllerTempPosition.Y += 25;
            }

            foreach (KeyValuePair<Buttons, string> entry in ControllerButtons)
            {
                spriteBatch.DrawString(_smallFont, entry.Key + " - " + entry.Value, controllerTempPosition, Color.LightYellow);
                controllerTempPosition.Y += 25;
            }

            Vector2 keyboardTempPosition = new Vector2(_keyboardPosition.X, _keyboardPosition.Y + 40);

            if (ThumbstickDescription != null)
            {
                spriteBatch.DrawString(_smallFont, "WASD" + " - " + ThumbstickDescription, keyboardTempPosition, Color.LightYellow);
                keyboardTempPosition.Y += 25;
            }

            foreach (KeyValuePair<Keys, string> entry in KeyboardButtons)
            {
                spriteBatch.DrawString(_smallFont, entry.Key + " - " + entry.Value, keyboardTempPosition, Color.LightYellow);
                keyboardTempPosition.Y += 25;
            }

            spriteBatch.End();
        }


        private StringBuilder WrapWord(String original, SpriteFont font, int width)
        {
            StringBuilder target = new StringBuilder();
            char[] newLine = {'\r', '\n'};
            int lastWhiteSpace = 0;
            Vector2 currentTargetSize;
            for (int i = 0; i < original.Length; i++)
            {
                char character = original[i];
                if (char.IsWhiteSpace(character))
                {
                    lastWhiteSpace = target.Length;
                }
                target.Append(character);
                currentTargetSize = font.MeasureString(target);
                if (currentTargetSize.X > width)
                {
                    target.Insert(lastWhiteSpace, newLine);
                    target.Remove(lastWhiteSpace + newLine.Length, 1);
                }
            }
            return target;
        }
    }
}
