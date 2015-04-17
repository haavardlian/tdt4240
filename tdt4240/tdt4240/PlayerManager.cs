using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace tdt4240
{
    class PlayerManager
    {
        public static int MaxPlayers = 4;
        private static PlayerManager _instance;

        private readonly List<Player> _players = new List<Player>();

        public List<Player> Players
        {
            get { return _players;}
        }

        public int NumberOfPlayers { get; private set; }

        public static Color[] Colors = { Color.Green, Color.Red, Color.Blue, Color.Yellow };

        public PlayerManager()
        {
            NumberOfPlayers = 0;
        }

        public static PlayerManager Instance
        {
            get { return _instance ?? (_instance = new PlayerManager()); }
        }

        public bool PlayerExists(int controllerIndex)
        {
            return _players.Any(player => player.ControllerIndex == controllerIndex);
        }

        public bool PlayerExists(PlayerIndex playerIndex)
        {
            return _players.Any(player => player.PlayerIndex == playerIndex);
        }

        public void AddPlayer(int controllerIndex, InputType type)
        {
            if (PlayerExists(controllerIndex))
                return;
            if(type == InputType.Keyboard)
            {
                if (_players.Any(player => player.Input.Type == InputType.Keyboard))
                {
                    return;
                }
            }

            _players.Add(new Player((PlayerIndex)NumberOfPlayers, controllerIndex, type));
            NumberOfPlayers++;
          
        }

        public void RemovePlayer(PlayerIndex playerIndex)
        {
            if (!PlayerExists(playerIndex))
                return;

            foreach (var player in _players.Where(player => player.PlayerIndex == playerIndex))
            {
                _players.Remove(player);
                NumberOfPlayers--;
                return;
            }
        }

        public Player LatestPlayer()
        {
            return _players.Last();
        }

        public Player GetPlayer(PlayerIndex? playerIndex)
        {
            return _players.FirstOrDefault(player => player.PlayerIndex == playerIndex);
        }

        public void Reset()
        {
            _players.Clear();
            NumberOfPlayers = 0;
        }
    }
}
