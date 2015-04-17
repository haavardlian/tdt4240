using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using tdt4240.Boards;

namespace tdt4240.Minigames.Quiz
{
    class QuizIntro : MinigameIntro
    {
        public static SupportedPlayers SupportedPlayers = SupportedPlayers.All;

        private ContentManager _content;


        public QuizIntro(Board board) : base(board)
        {
            MiniGame = new Quiz(board);

            //ThumbstickDescription = "Move the string around";
            GameDescription = "Quiz game";
            Goal = "Answer correct and quickly. First player to 300 points wins!";

            ControllerButtons.Add(Buttons.X, "Alternative 1");
            ControllerButtons.Add(Buttons.A, "Alternative 2");
            ControllerButtons.Add(Buttons.Y, "Alternative 3");
            ControllerButtons.Add(Buttons.B, "Alternative 4");
            ControllerButtons.Add(Buttons.DPadDown, "Next question");
            
            KeyboardButtons.Add(Keys.D1, "Alternative 1");
            KeyboardButtons.Add(Keys.D2, "Alternative 2");
            KeyboardButtons.Add(Keys.D3, "Alternative 3");
            KeyboardButtons.Add(Keys.D4, "Alternative 4");
        }

        public override void Activate(bool instancePreserved)
        {
            base.Activate(instancePreserved);

            if (!instancePreserved)
            {
                if (_content == null)
                    _content = new ContentManager(ScreenManager.Game.Services, "Content");

                Cover = _content.Load<Texture2D>("minigameCovers/quiz");
            }
        }


    }
}
