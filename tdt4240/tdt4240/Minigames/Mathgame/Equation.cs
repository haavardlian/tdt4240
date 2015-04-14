using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tdt4240.Assets;

namespace tdt4240.Minigames.Mathgame
{
    class Equation
    {
        public String _equation;
        private Boolean _correctAnswer;

        //Constructor
        public Equation (String equation, Boolean correctAnswer)
        {
            _equation = equation;
            _correctAnswer = correctAnswer;
        }

        public String equation
        {
            get { return _equation; }
            set { _equation = value; }
        }

        public Boolean CorrectAnswer
        {
            get { return _correctAnswer; }
            set { _correctAnswer = value; }
        }
    }
}
