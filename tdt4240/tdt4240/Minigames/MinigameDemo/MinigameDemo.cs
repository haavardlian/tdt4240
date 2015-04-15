using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tdt4240.Boards;

namespace tdt4240.Minigames.MinigameDemo
{
    class MinigameDemo : MiniGame
    {

        public new static SupportedPlayers SupportedPlayers = SupportedPlayers.Three;
        
        private SpriteFont _font;
        private readonly Vector2[] _textPosition = new Vector2[4];

        public MinigameDemo(Board board) : base(board)
        {
            Title = "Demo";

            //DO stuff based on the amount of players playing
        }

        public override void Activate(bool instancePreserved)
        {
            base.Activate(instancePreserved);

            if (!instancePreserved)
            {
                _font = ScreenManager.Font;
                Background = new Background("background");
                ScreenManager.AddScreen(Background, null);
            }
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            foreach(Player player in PlayerManager.Instance.Players)
            {
                _textPosition[(int)player.PlayerIndex] += player.Input.GetThumbstickVector();

                if (player.Input.IsButtonPressed(GameButtons.Y))
                {
                    NotifyDone(player.PlayerIndex);
                }

            }
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            //Update sprite coordinates
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();
            
            foreach (Player player in PlayerManager.Instance.Players)
            {
                spriteBatch.DrawString(_font, player.TestString, _textPosition[(int)player.PlayerIndex], player.Color);
            }

            spriteBatch.End();
        }
    }
}
