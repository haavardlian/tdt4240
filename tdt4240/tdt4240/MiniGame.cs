using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using tdt4240.Boards;

namespace tdt4240
{
    [Flags]
    public enum SupportedPlayers
    {
        None = 0,
        Two = 1,
        Three = 2,
        Four = 4,
        All = 7
    }

    internal abstract class MiniGame : GameScreen
    {
        public static SupportedPlayers SupportedPlayers = SupportedPlayers.All;

        protected Board Board;
        protected ContentManager content;
        protected String Title = "Title not implemented";

        protected Background Background;

        protected MiniGame(Board board)
        {
            this.Board = board;
        }

        public virtual void NotifyDone(PlayerIndex winningPlayerIndex)
        {
            Background.ExitScreen();
            this.ExitScreen();
            Board.MiniGameDone(winningPlayerIndex);
        }

        //Gamescreen overiding
        public override void Activate(bool instancePreserved)
        {
            if (!instancePreserved)
            {
                if (content == null)
                    content = new ContentManager(ScreenManager.Game.Services, "Content");
            }
        }
    }
}
