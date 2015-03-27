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

    abstract class MiniGame : GameScreen
    {
        public static SupportedPlayers SupportedPlayers = SupportedPlayers.All;

        protected Board board;
        protected ContentManager content;
        protected String title = "Title not implemented";

/*        public SupportedPlayers SupportedPlayers
        {
            get { return supportedPlayers; }
        }*/

        protected MiniGame(Board board)
        {
            this.board = board;
        }

        public virtual void NotifyDone(PlayerIndex winningPlayerIndex)
        {
            //Send the winner id/player object
            board.MiniGameDone(winningPlayerIndex);
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
