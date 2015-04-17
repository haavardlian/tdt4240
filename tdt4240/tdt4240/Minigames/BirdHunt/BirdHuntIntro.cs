using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using tdt4240.Boards;

namespace tdt4240.Minigames.BirdHunt
{
    class BirdHuntIntro : MinigameIntro
    {
        public new static SupportedPlayers SupportedPlayers = SupportedPlayers.All;

        private ContentManager _content;


        public BirdHuntIntro(Board board) : base(board)
        {
            MiniGame = new BirdHunt(board);

            ThumbstickDescription = "Move the crosshair around";
            GameDescription = "aim with the crosshair and shoot the birds";
            Goal = "TBA";

            ControllerButtons.Add(Buttons.A, "Shoot");
            KeyboardButtons.Add(Keys.D1, "Shoot");


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
