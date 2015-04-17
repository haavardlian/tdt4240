using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using tdt4240.Boards;

namespace tdt4240.Minigames.AvoidObstacles
{
    class AvoidObstaclesIntro : MinigameIntro
    {
        public new static SupportedPlayers SupportedPlayers = SupportedPlayers.All;

        private ContentManager _content;


        public AvoidObstaclesIntro(Board board) : base(board)
        {
            MiniGame = new AvoidObstacles(board);

            ThumbstickDescription = "Use thumbstick do speed the helicopter in different directions";
            GameDescription = "Avoid birds and the other helicopters";
            Goal = "TBA";

            /*ControllerButtons.Add(Buttons.A, "Exit game");
            ControllerButtons.Add(Buttons.B, "Test 1");
            ControllerButtons.Add(Buttons.Y, "Test 2");

            KeyboardButtons.Add(Keys.D1, "Exit game");
            KeyboardButtons.Add(Keys.Back, "Test 1");
            KeyboardButtons.Add(Keys.Y, "Test 2");*/
        }

        public override void Activate(bool instancePreserved)
        {
            base.Activate(instancePreserved);

            if (!instancePreserved)
            {
                if (_content == null)
                    _content = new ContentManager(ScreenManager.Game.Services, "Content");

                Cover = _content.Load<Texture2D>("minigameCovers/demo");
            }
        }
    }
}
