using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tdt4240
{
    class PlayerManager
    {
        private static PlayerManager _instance = null;
        static List<Player> players = new List<Player>();

        public static PlayerManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PlayerManager();
                }
                return _instance;
            }
        }

        public Boolean playerJoined(int controllerIndex)
        {
            foreach (Player player in players)
            {
                if (player.controllerIndex == controllerIndex)
                {
                    return true;
                }
            }
            return false;
        }

        public int numberOfPlayersJoined()
        {
            return players.Count;
        }

        public void addPlayer(int controller)
        {
            players.Add(new Player(numberOfPlayersJoined(), controller));
        }
    }
}
