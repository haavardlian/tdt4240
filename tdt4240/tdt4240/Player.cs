using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tdt4240
{

    public enum PlayerStatus
    {
        nan = 0,
        //Joined = 1,
        Ready = 2,
    }

    class Player
    {
        public PlayerStatus status = PlayerStatus.nan;
        public int id;
        public int controllerIndex;
        public Color color;


        public Player(int id)
        {
            this.id = id;
            this.color = PlayerManager.colors[id];
        }

        public void join(int controllerIndex)
        {
            this.controllerIndex = controllerIndex;
            this.status = PlayerStatus.Ready;
        }

        public void leave()
        {
            this.status = PlayerStatus.nan;
        }
    }
}
