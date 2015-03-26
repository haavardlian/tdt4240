using Microsoft.Xna.Framework;
using System;
using tdt4240.Boards;

namespace tdt4240
{



    class Player : EventArgs
    {
        public PlayerIndex playerIndex;
        public int controllerIndex;
        public Color color;
        public InputDevice Input;
        public string TestString = "This is player ";
        private BoardPosition _boardPosition;
        public BoardPosition BoardPosition
        {
            get
            {
                return _boardPosition;
            }
            set
            {
                _boardPosition = value;
                BoardPosition.NavigateTo(this);
            }
        }



        public Player(PlayerIndex playerIndex, int controllerIndex, InputType type)
        {
            this.playerIndex = playerIndex;
            this.color = PlayerManager.Colors[(int)playerIndex];
            this.Input = InputDevice.CreateInputDevice(type, playerIndex);
            this.controllerIndex = controllerIndex;
            this.TestString += playerIndex + " on controller " + controllerIndex;
        }
    }
}
