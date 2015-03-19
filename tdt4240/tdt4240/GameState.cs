using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tdt4240
{
    public abstract class GameState : GameComponent
    {
        protected Texture2D _background;

        public GameState(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            LoadContent();
            base.Initialize();
        }

        public virtual void LoadContent()
        {
            throw new NotImplementedException();
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

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
