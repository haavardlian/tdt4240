using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using tdt4240.Boards;

namespace tdt4240
{

    public enum Effect
    {
        Freeze,
        DoubleRoll,
        HalfRollRange
    }


    class Player : EventArgs
    {
        private const int MaxPowerUps = 2;


        private readonly PlayerIndex _playerIndex;
        private readonly int _controllerIndex;
        private readonly Color _color;
        public InputDevice Input;
        public string TestString = "This is player ";
        
        private BoardPosition _boardPosition;
        private readonly List<PowerUp> _powerUps = new List<PowerUp>();

        public PlayerIndex PlayerIndex
        {
            get { return _playerIndex; }
        }

        public int ControllerIndex
        {
            get { return _controllerIndex; }
        }

        public Color Color
        {
            get { return _color; }
        }

        public BoardPosition BoardPosition
        {
            get
            {
                return _boardPosition;
            }
            set
            {
                _boardPosition = value;
                _boardPosition.OnNavigateTo(this, this);
            }
        }

        public Effect Effect { get; set; }

        public List<PowerUp> PowerUps  
        {
            get { return _powerUps; }
        }


        public Player(PlayerIndex playerIndex, int controllerIndex, InputType type)
        {
            _playerIndex = playerIndex;
            _color = PlayerManager.Colors[(int)playerIndex];
            Input = InputDevice.CreateInputDevice(type, playerIndex);
            _controllerIndex = controllerIndex;
            TestString += playerIndex + " on controller " + controllerIndex;
        }

        public void AddPowerUp(PowerUp powerUp)
        {
            if (_powerUps.Count >= MaxPowerUps)
            {
                _powerUps.RemoveAt(0);
            }
            _powerUps.Add(powerUp);
        }

        public void RemovePowerUp(PowerUp powerUp)
        {
            _powerUps.Remove(powerUp);
        }
    }
}
