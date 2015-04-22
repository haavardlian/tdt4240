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
            GameDescription = "Aim with the crosshair and shoot the birds";
            Goal = "Be the first to kill 10 birds";

            ControllerButtons.Add(Buttons.A, "Shoot");
            KeyboardButtons.Add(Keys.D1, "Shoot");
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
