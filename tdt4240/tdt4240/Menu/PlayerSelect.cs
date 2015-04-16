using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using tdt4240.Boards;
using tdt4240.Minigames;

namespace tdt4240.Menu
{
    class PlayerSelect : GameScreen
    {
        private const int MinimumAllowedPlayers = 1;

        private ContentManager _content;
        private SpriteFont _font;

        readonly List<PlayerSelectStatus> _playerSelectStatuses = new List<PlayerSelectStatus>();

        public PlayerSelect()
        {
            for (int i = 0; i < PlayerManager.MaxPlayers; i++)
            {
                _playerSelectStatuses.Add(new PlayerSelectStatus(i));
            }
        }

        public override void Activate(bool instancePreserved)
        {
            if (!instancePreserved)
            {
                if (_content == null)
                    _content = new ContentManager(ScreenManager.Game.Services, "Content");

                _font = ScreenManager.Font;
            }
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            for (int i = 0; i < 4; i++)
            {
                GamePadState gamePadState = input.CurrentGamePadStates[i];

                if (gamePadState.Buttons.A == ButtonState.Pressed)
                {
                    AddPlayer(i, InputType.Controller);
                    _playerSelectStatuses[PlayerManager.Instance.NumberOfPlayers - 1].Player = PlayerManager.Instance.LatestPlayer();
                }
                if (gamePadState.Buttons.B == ButtonState.Pressed)
                {
                    RemovePlayer((PlayerIndex)i);
                }

                if (gamePadState.Buttons.Start == ButtonState.Pressed)
                {
                    StartGame();
                }
            }

            KeyboardState keyboardState = input.CurrentKeyboardStates[0];

            if (keyboardState.IsKeyDown(Keys.A))
            {
                AddPlayer(-1, InputType.Keyboard);
                _playerSelectStatuses[PlayerManager.Instance.NumberOfPlayers-1].Player = PlayerManager.Instance.LatestPlayer();
            }
            if (keyboardState.IsKeyDown(Keys.Back))
            {
                RemovePlayer((PlayerIndex)0);
            }
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                StartGame();
            }

        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            int screenHeigthModifier = ScreenManager.GraphicsDevice.Viewport.Height / 4;

            foreach (PlayerSelectStatus playerSelectStatus in _playerSelectStatuses)
            {
                playerSelectStatus.updatePosition(ScreenManager.GraphicsDevice.Viewport.Width, screenHeigthModifier, _font);
            }

        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            foreach (PlayerSelectStatus playerSelectStatus in _playerSelectStatuses)
            {
                playerSelectStatus.draw(spriteBatch, _font, gameTime);
            }

            if (PlayerManager.Instance.NumberOfPlayers >= MinimumAllowedPlayers)
            {
                float x = ScreenManager.MaxWidth*ScreenManager.GetScalingFactor()/2
                          - (_font.MeasureString("Press Start to play").X/2);

                float y = ScreenManager.MaxHeight*ScreenManager.GetScalingFactor()*90/100;

                Vector2 position = new Vector2(x, y);

                PulsatingText.Draw(spriteBatch, gameTime, _font, "Press Start to play", position, Color.Yellow);
            }

            spriteBatch.End();
        }

        private void AddPlayer(int controllerIndex, InputType type)
        {
            PlayerManager.Instance.AddPlayer(controllerIndex, type);
        }

        private void RemovePlayer(PlayerIndex playerIndex)
        {
            if (PlayerManager.Instance.PlayerExists(playerIndex))
            {
                PlayerManager.Instance.RemovePlayer(playerIndex);
            }
            else
            {
                //If the player that entered the player select screen backs the game will return to the main menu
                if (playerIndex == ControllingPlayer)
                {
                    ScreenManager.RemoveScreen(this);
                }
            }
        }

        private void StartGame()
        {
            int players = PlayerManager.Instance.NumberOfPlayers;
            if (players >= MinimumAllowedPlayers)
            {
                var board = new Board();

                ScreenManager.ExitAllScreens();
                ScreenManager.AddScreen(board , null);
            }
        }
    }
}
