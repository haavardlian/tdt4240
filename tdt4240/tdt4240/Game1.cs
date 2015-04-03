using System;
using Microsoft.Xna.Framework;
using tdt4240.Menu;

namespace tdt4240
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        readonly GraphicsDeviceManager _graphics;

        private const float Ratio2 = 9f/16f;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            ScreenManager.CreateInstance(this, _graphics);
            var screenManager = ScreenManager.Instance;
            Components.Add(screenManager);

            if (Settings.Instance.Fullscreen)
                screenManager.SetFullScreen();
            else
            {
                _graphics.PreferredBackBufferHeight = 432;
                _graphics.PreferredBackBufferWidth = 768;
            }

            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += Window_ClientSizeChanged;

            Content.RootDirectory = "Content";

            screenManager.AddScreen(new Background("background"), null);
            screenManager.AddScreen(new MainMenu(), null);

        }

        void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            _graphics.PreferredBackBufferWidth = GraphicsDevice.Viewport.Bounds.Width;
            _graphics.PreferredBackBufferHeight = (int)(GraphicsDevice.Viewport.Bounds.Width * Ratio2);
            _graphics.ApplyChanges();
        }

    }
}
