using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tdt4240
{
    class PlayerManager
    {
        public static int MaxPlayers = 4;
        private static PlayerManager _instance = null;

        private List<Player> _players = new List<Player>();

        public List<Player> Players
        {
            get { return _players;}
        }

        private int _numberOfPlayers = 0;

        public int NumberOfPlayers
        {
            get { return _numberOfPlayers; }
        }

        public static Color[] Colors = { Color.Green, Color.Red, Color.Blue, Color.Yellow };

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
        }

        public Boolean PlayerExists(int controllerIndex)
        {
            foreach (Player player in _players)
            {
                if (player.controllerIndex == controllerIndex)
                    return true;
            }
            return false;
        }

        public Boolean PlayerExists(PlayerIndex playerIndex)
        {
            foreach (Player player in _players)
            {
                if (player.playerIndex == playerIndex)
                    return true;
            }
            return false;
        }

        public void AddPlayer(int controllerIndex, InputType type)
        {
            if (PlayerExists(controllerIndex))
                return;
            else if(type == InputType.Keyboard)
            {
                foreach (Player player in _players)
                {
                    if (player.Input.Type == InputType.Keyboard)
                        return;
                }
            }

            _players.Add(new Player((PlayerIndex)_numberOfPlayers, controllerIndex, type));
            _numberOfPlayers++;
          
        }

        public void RemovePlayer(PlayerIndex playerIndex)
        {
            if (!PlayerExists(playerIndex))
                return;

            foreach(Player player in _players)
            {
                if (player.playerIndex == playerIndex)
                {
                    _players.Remove(player);
                    _numberOfPlayers--;
                    return;
                }
            }
        }

        public Player LatestPlayer()
        {
            return _players.Last();
        }

    }
}
