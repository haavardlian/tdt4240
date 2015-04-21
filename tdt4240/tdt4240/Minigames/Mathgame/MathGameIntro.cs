using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using tdt4240.Boards;

namespace tdt4240.Minigames.MathGame
{
    class MathGameIntro : MinigameIntro
    {
        public new static SupportedPlayers SupportedPlayers = MathGame.SupportedPlayers;

        private ContentManager _content;


        public MathGameIntro(Board board)
            : base(board)
        {
            MiniGame = new MathGame(board);

            GameDescription = "Press X when the equation matches the number";
            Goal = "Get 3 points to win";
        }

        public override void Activate(bool instancePreserved)
        {
            base.Activate(instancePreserved);

            if (!instancePreserved)
            {
                if (_content == null)
                    _content = new ContentManager(ScreenManager.Game.Services, "Content");

                Cover = _content.Load<Texture2D>("minigameCovers/MathGame");
            }
        }
    }
}