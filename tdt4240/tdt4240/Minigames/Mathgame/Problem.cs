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
        private static int _numberOfEquations = 5;
        private const int _highestNumberInEquation = 10;
        private const int _minimumOperands = 1;
        private const int _maximumOperands = 4;
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
            Equation correctEq = _equationTable[_numberOfEquations-1];
            _answer = new Expression(correctEq.equation).Evaluate().ToString();
            RandomizeCorrectAnswerPosition();
            checkEquationsForCorrectAnswer();
                
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

            int randomize = rnd.Next(_numberOfEquations);

            Equation temp = _equationTable[_numberOfEquations-1];
            _equationTable[_numberOfEquations-1] = _equationTable[randomize];
            _equationTable[randomize] = temp;
            Console.WriteLine("Randomize" + randomize);
        }

        public Equation GenerateEquation()
        {
            StringBuilder builder = new StringBuilder();
            int numberOfOperands = rnd.Next(_minimumOperands,_maximumOperands);
            int randomNumber;

            for (int i = 0; i <numberOfOperands ; i++)
            {
                randomNumber = rnd.Next(_highestNumberInEquation+1);
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
                        operand = "*";
                        break;
                    case 4:
                        operand = "/";
                        break;
                }
                
                builder.Append(operand);
            }
            builder.Append(rnd.Next(_highestNumberInEquation+1));

            Console.WriteLine("equation " + builder.ToString());
            
            return new Equation(builder.ToString(), false);
        }

        public void checkEquationsForCorrectAnswer()
        {
            for (int i = 0; i < _equationTable.Length; i++)
            {
                Expression e = new Expression(_equationTable[i].equation);
                if (e.Evaluate().ToString().Equals(_answer.ToString()))
                {
                    _equationTable[i].CorrectAnswer = true;
                }
                Console.WriteLine(_equationTable[i].equation + " = " + e.Evaluate());
            }
        }

    }
}
