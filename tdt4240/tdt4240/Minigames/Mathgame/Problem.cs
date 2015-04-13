using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tdt4240.Assets;
using NCalc;

namespace tdt4240.Minigames.MathGame
{
    class Problem
    {
        

        //Constructor
        public Problem()
        {

        }

        public String GenerateEquation()
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
            randomNumber = rnd.Next(20);
            builder.Append(randomNumber);

            Expression e = new Expression(builder.ToString());
            
            Console.WriteLine(builder.ToString() + " = " + e.Evaluate());

            return builder.ToString();
        }

    }
}
