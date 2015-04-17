
#region Using Statements
using System;
#endregion

namespace tdt4240
{
    public class PowerUpEvent : EventArgs
    {
        public PowerUpEvent(Player player)
        {
            this.player = player;
        }

        public Player Player
        {
            get { return player; }
        }

        Player player;
    }
}
