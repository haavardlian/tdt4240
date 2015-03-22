using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tdt4240
{
    class Player
    {
        public int id;
        public int controllerIndex;
        public Color color;

        Color[] colors = { Color.Green, Color.Red, Color.Blue, Color.Yellow };


        public Player(int id, int controllerIndex)
        {
            this.id = id;
            this.controllerIndex = controllerIndex;
            this.color = colors[id];
        }
    }
}
