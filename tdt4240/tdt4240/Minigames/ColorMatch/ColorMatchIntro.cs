using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using tdt4240.Boards;

namespace tdt4240.Minigames.ColorMatch
{
    class ColorMatchIntro : MinigameIntro
    {
        public new static SupportedPlayers SupportedPlayers = ColorMatch.SupportedPlayers;

        private ContentManager _content;


        public ColorMatchIntro(Board board)
            : base(board)
        {
            MiniGame = new ColorMatch(board);

            GameDescription = "Press X when the color matches the word";
            Goal = "Get 3 points to win";
        }

        public override void Activate(bool instancePreserved)
        {
            base.Activate(instancePreserved);

            if (!instancePreserved)
            {
                if (_content == null)
                    _content = new ContentManager(ScreenManager.Game.Services, "Content");

                Cover = _content.Load<Texture2D>("minigameCovers/color_match");
            }
        }
    }
}
