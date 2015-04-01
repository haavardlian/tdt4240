using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using tdt4240.Boards;

namespace tdt4240.Minigames.MinigameDemo
{
    class MinigameDemoIntro : MinigameIntro
    {
        public new static SupportedPlayers SupportedPlayers = SupportedPlayers.Two | SupportedPlayers.Four;

        private ContentManager _content;


        public MinigameDemoIntro(Board board) : base(board)
        {
            MiniGame = new MinigameDemo(board);

            ThumbstickDescription = "Move the string around";
            GameDescription = "DEMO minigame to display how the minigames should be implemented";
            Goal = "Get 3 thingies";

            ControllerButtons.Add(Buttons.A, "Exit game");
            ControllerButtons.Add(Buttons.B, "Test 1");
            ControllerButtons.Add(Buttons.Y, "Test 2");

            KeyboardButtons.Add(Keys.D1, "Exit game");
            KeyboardButtons.Add(Keys.Back, "Test 1");
            KeyboardButtons.Add(Keys.Y, "Test 2");
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
