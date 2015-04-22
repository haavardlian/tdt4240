using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using tdt4240.Boards;

namespace tdt4240.Minigames.MusicMania
{
    class MusicManiaIntro : MinigameIntro
    {
        public new static SupportedPlayers SupportedPlayers = MusicMania.SupportedPlayers;

        private ContentManager _content;


        public MusicManiaIntro(Board board)
            : base(board)
        {
            MiniGame = new MusicMania(board);

            GameDescription = "Press the correct button when an arrow is between the white lines. Good timing will get you a good score.";
            Goal = "Get the highest score";
        }

        public override void Activate(bool instancePreserved)
        {
            base.Activate(instancePreserved);

            if (!instancePreserved)
            {
                if (_content == null)
                    _content = new ContentManager(ScreenManager.Game.Services, "Content");

                Cover = _content.Load<Texture2D>("minigameCovers/MusicMania");
            }
        }
    }
}
