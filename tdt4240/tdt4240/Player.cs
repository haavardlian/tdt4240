using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tdt4240
{



    class Player
    {
        public PlayerIndex playerIndex;
        public int controllerIndex;
        public Color color;
        public InputDevice Input;
        public string TestString = "This is player ";


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
