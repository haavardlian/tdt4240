using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tdt4240
{
    class MiniGame : GameState
    {
        public MiniGame(Game game) : base(game)
        {

        }

        public void NotifyDone()
        {
            //TODO: StateManager should have a method to take score,
            //      and the switch back to board?
            StateManager.Instance.MiniGameComplete();
        }

        public void Start()
        {
            throw new NotImplementedException();
        }
    }
}
