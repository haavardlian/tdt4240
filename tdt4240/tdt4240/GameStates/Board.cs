using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tdt4240.GameStates
{
    class Board : GameState
    {
        public Board(Game game)
            : base(game)
        {

        }

        public override void LoadContent()
        {
            _background = Game.Content.Load<Texture2D>("board");
        }
    }
}
