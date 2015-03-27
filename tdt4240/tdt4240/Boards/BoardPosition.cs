using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tdt4240.Boards
{
    public enum PositionType
    {
        Default = 0,
        Start,
        PowerUp
    }

    class BoardPosition
    {
        public Vector2 Position { get; set; }
        public Action<Player> NavigateTo { get; set; }
        public Texture2D Background { get; set; }

        public BoardPosition(Vector2 position)
        {
            Position = position;
            NavigateTo = OnNavigateToDefault;
        }

        public BoardPosition(Vector2 position, PositionType type)
        {
            Position = position;
            switch (type)
            {
                case PositionType.Start:
                    NavigateTo = OnNavigateToStart;
                    break;
                default:
                    NavigateTo = OnNavigateToDefault;
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Background == null) return;
            spriteBatch.Draw(Background, Vector2.Zero, Color.White);
        }

        public static void OnNavigateToDefault(Player player)
        {
            Console.WriteLine(player.playerIndex + " navigated to a new position");
        }

        public static void OnNavigateToStart(Player player)
        {
            Console.WriteLine(player.playerIndex + " navigated to start");
        }
    }
}
