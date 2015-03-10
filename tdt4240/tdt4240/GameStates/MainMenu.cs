using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tdt4240.GameStates
{
    class MainMenu : GameState
    {
        double elapsed = 0;
        public MainMenu(Game game) : base(game)
        {

        }

        public override void Update(GameTime gameTime)
        {
            elapsed += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsed > 10000)
            {
                StateManager.Instance.ChangeState(StateManager.Instance.Board);
            }

            base.Update(gameTime);
        }

        public override void LoadContent()
        {
            _background = Game.Content.Load<Texture2D>("main_menu");
        }
    }
}
