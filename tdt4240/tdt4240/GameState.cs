using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using tdt4240.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tdt4240
{
    public abstract class GameState : IGameObject
    {
        protected Texture2D _background;

        public GameState(Texture2D Background)
        {
            _background = Background;
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(_background, Vector2.Zero, Color.White);
        }

        public Texture2D Background
        {
            get
            {
                return _background;
            }
        }
    }
}
