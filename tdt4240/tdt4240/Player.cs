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
        public PlayerIndex playerIndex;
        public int controllerIndex;
        public Color color;
        public InputDevice Input;


        public Player(PlayerIndex playerIndex)
        {
            this.playerIndex = playerIndex;
            this.color = PlayerManager.colors[(int)playerIndex];
        }

        public void join(int controllerIndex)
        {
            if (controllerIndex < 0)
            {
                this.Input = InputDevice.CreateInputDevice(InputType.Keyboard, playerIndex);
            }
            else
            {
                this.Input = InputDevice.CreateInputDevice(InputType.Controller, playerIndex);
            }


            this.controllerIndex = controllerIndex;
            this.status = PlayerStatus.Ready;
        }

        public void leave()
        {
            this.status = PlayerStatus.nan;
        }
    }
}
