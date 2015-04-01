using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tdt4240.Menu
{
    class PlayerSelectStatus
    {
        private readonly int _index;

        private readonly string[] _statusTexts = new string[] { "Press A to join", "Select color", "Ready" };

        public string StatusText
        {
            get
            {
                if (Player == null)
                    return _statusTexts[0];

                return _statusTexts[2];
            }
        }

        public Player Player { get; set; }

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value;  }
        }

        private Vector2 _position;

        public PlayerSelectStatus(int index)
        {
            _index = index;
            _position = new Vector2();
        }

        public void UpdatePosition(int width, int heigthModifier, SpriteFont font)
        {
            _position.X = width - font.MeasureString(StatusText).X - 30;
            _position.Y = heigthModifier * _index + 30;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font, GameTime gameTime)
        {
            if (Player == null)
            {
                PulsatingText.Draw(spriteBatch, gameTime, font, StatusText, _position, Color.Yellow);
            }
            else
            {
                spriteBatch.DrawString(font, StatusText, _position, Player.Color);
            }

        }

    }
}
