using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tdt4240
{
    class PlayerManager
    {
        private static PlayerManager _instance = null;

        private List<Player> players = new List<Player>();

        public List<Player> Players
        {
            get { return players;}
        }

        private int numberOfPlayers = 0;

        public int NumberOfPlayers
        {
            get { return numberOfPlayers; }
        }

        public static Color[] colors = { Color.Green, Color.Red, Color.Blue, Color.Yellow };

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

        public PlayerManager()
        {
            for (int i = 0; i < 4; i++)
            {
                players.Add(new Player(i));
            }
        }

        public Boolean playerJoined(int controllerIndex)
        {
            foreach (Player player in players)
            {
                if (player.controllerIndex == controllerIndex && player.status == PlayerStatus.Ready)
                {
                    return true;
                }
            }
            return false;
        }

        public void joinPlayer(int controller)
        {
            for (int i = 0; i < players.Count; i++ )
            {
                if (players[i].status == PlayerStatus.nan)
                {
                    players[i].join(controller);
                    numberOfPlayers++;
                    break;
                }
            }
        }

        public void removePlayer(int controller)
        {

            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].controllerIndex == controller && players[i].status == PlayerStatus.Ready)
                {
                    players[i].leave();
                    numberOfPlayers--;
                }
            }
        }
    }
}
