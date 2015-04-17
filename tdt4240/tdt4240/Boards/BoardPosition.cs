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

    public class BoardPosition
    {
        public Vector2 Position { get; set; }
        public event EventHandler NavigateTo;
        public Texture2D Background { get; set; }
        public Texture2D Icon { get; set; }

        public BoardPosition(Vector2 position)
        {
            Position = position;
            NavigateTo += NavigateToDefault;
        }

        public BoardPosition(Vector2 position, PositionType type, Texture2D background)
        {
            Position = position;
            Background = background;
            switch (type)
            {
                case PositionType.Start:
                    NavigateTo += NavigateToStart;
                    break;
                default:
                    NavigateTo += NavigateToDefault;
                    break;
            }
        }

        public void OnNavigateTo(object sender, EventArgs args)
        {
            if (NavigateTo == null) return;
            NavigateTo(sender, args);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Background == null) return;
            spriteBatch.Draw(Background, Position * ScreenManager.Instance.GetScalingFactor(), null, Color.Gray, 0f, (new Vector2(48, 48)) * ScreenManager.Instance.GetScalingFactor(), ScreenManager.Instance.GetScalingFactor(), SpriteEffects.None, 0f);

            if (Icon != null)
            {
                spriteBatch.Draw(Icon, Position * ScreenManager.Instance.GetScalingFactor(), null, Color.White, 0f, (new Vector2(32, 32)) * ScreenManager.Instance.GetScalingFactor(), ScreenManager.Instance.GetScalingFactor(), SpriteEffects.None, 0f);
            }
        }

        private void NavigateToDefault(object sender, EventArgs args)
        {
            var player = args as Player;
            Console.WriteLine(player.PlayerIndex + " navigated to a new position");
        }

        private void NavigateToStart(object sender, EventArgs args)
        {
            var player = args as Player;
            Console.WriteLine(player.PlayerIndex + " navigated to start");
        }

        private void NavigateToPowerUp(object sender, EventArgs args)
        {
            var player = args as Player;
        }
    }
}
