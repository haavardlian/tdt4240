using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tdt4240.Assets;

namespace tdt4240.Minigames.Mathgame
{
    class Mathplayer
    {
        public int Score;
        /// <summary>
        /// The corner which this player belongs to, this is where the score will be displayed
        /// </summary>
        public Vector2 Corner;
        public Player Player;
        public Color Color;

        //Constructor
        public Mathplayer(Player player)
        {
            Player = player;
            Color = player.Color;
            Score = 0;
        }

        public int score
        {
            get { return Score; }
            set { Score = value; }
        }

        public Vector2 corner
        {
            get { return Corner; }
            set { Corner = value; }
        }

        public Player player
        {
            get { return Player; }
            set { Player = value; }
        }

        public Color color
        {
            get { return Color; }
            set { Color = value; }
        }
    }
}
