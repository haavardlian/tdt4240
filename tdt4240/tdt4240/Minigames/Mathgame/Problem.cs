using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tdt4240.Assets;
using NCalc;
using tdt4240.Minigames.Mathgame;

namespace tdt4240.Minigames.MathGame
{
    class Problem
    {
        private string _answer = "0";
        private static int _numberOfEquations = 10;
        private static Equation[] _equationTable = new Equation[_numberOfEquations];
        Random rnd = new Random();
        

        //Constructor
        public Problem()
        {

            for (int i = 0; i < _numberOfEquations; i++)
            {
                {
                    _equationTable[i] = GenerateEquation();
                }
            }
            _answer = new Expression(_equationTable[9]._equation).Evaluate().ToString();
            RandomizeCorrectAnswerPosition();
            Console.WriteLine("Answer: " + _answer);
        }

        public string answer
        {
            get { return _answer; }
            set { _answer = value; }
        }

        public int numberOfEquations
        {
            get { return _numberOfEquations; }
            set { _numberOfEquations = value; }
        }

        public Equation[] equationTable
        {
            get { return _equationTable; }
            set { _equationTable = value; }
        }

        public void RandomizeCorrectAnswerPosition()
        {
            int randomize = rnd.Next(11);

            Equation temp = _equationTable[9];
            _equationTable[9] = _equationTable[randomize];
            _equationTable[randomize] = temp;
            Console.WriteLine("Randomize" + randomize);
        }

        public Equation GenerateEquation()
        {
            Random rnd = new Random();
            StringBuilder builder = new StringBuilder();
            int numberOfOperands = rnd.Next(3,6);
            int randomNumber;

            for (int i = 0; i <numberOfOperands ; i++)
            {
                randomNumber = rnd.Next(11);
                builder.Append(randomNumber);

                int randomOperand = rnd.Next(1,4);
                string operand = null;

                switch (randomOperand)
                {
                    case 1:
                        operand = " + ";
                        break;
                    case 2:
                        operand = " - ";
                        break;
                    case 3:
                        operand = " * ";
                        break;
                    case 4:
                        operand = " / ";
                        break;
                }
                
                builder.Append(operand);
            }
            randomNumber = rnd.Next(11);
            builder.Append(randomNumber);

            Expression e = new Expression(builder.ToString());
            
            Console.WriteLine(builder.ToString() + " = " + e.Evaluate());
            
            Boolean correct = false;
            if (e.Evaluate() == _answer.ToString())
            {
                correct = true;
            }

            return new Equation(builder.ToString(), correct);
        }

    }
}
