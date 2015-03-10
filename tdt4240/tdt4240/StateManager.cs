using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using tdt4240.GameStates;

namespace tdt4240
{
    class StateManager
    {
        private static StateManager _instance = null;

        private GameState _currentState = null;
        private GameState _mainMenu;
        private GameState _board;
        private List<MiniGame> _miniGames = new List<MiniGame>();
        private int _currentMiniGameIndex = -1;

        private StateManager()
        {
            
        }

        public void Initialize()
        {
            _currentState = _mainMenu;
        }

        public static StateManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new StateManager();
                }
                return _instance;
            }
        }

        public GameState CurrentState
        {
            get
            {
                return _currentState;
            }
        }

        public List<MiniGame> MiniGames
        {
            get
            {
                return _miniGames;
            }
        }

        public GameState MainMenu
        {
            get
            {
                return _mainMenu;
            }
            set
            {
                _mainMenu = value;
            }
        }

        public GameState Board
        {
            get
            {
                return _board;
            }
            set
            {
                _board = value;
            }
        }

        public MiniGame NextMiniGame()
        {
            if (_currentMiniGameIndex == -1 || (_currentMiniGameIndex + 1) >= _miniGames.Count)
            {
                return null;
            }
            else
            {
                return _miniGames[_currentMiniGameIndex];
            }
        }

        public void ChangeState(GameState state)
        {
            //TODO: Check to only allow valid state transitions?
            _currentState = state;
        }

        public void MiniGameComplete()
        {
            _currentState = _board;
        }
    }
}
