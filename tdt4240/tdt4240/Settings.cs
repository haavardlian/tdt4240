using Microsoft.Xna.Framework.Graphics;

namespace tdt4240
{
    class Settings
    {
        private bool _fullscreen;

        public bool Fullscreen
        {
            //TODO: Go back to something other than HD ready. Have a list of resolutions?
            get { return _fullscreen; }
            set
            {
                _fullscreen = value;

                if (_fullscreen)
                {
                    ScreenManager.Graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                    ScreenManager.Graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                }
                else
                {
                    ScreenManager.Graphics.PreferredBackBufferHeight = 720;
                    ScreenManager.Graphics.PreferredBackBufferWidth = 1280;
                }
                
                ScreenManager.Graphics.IsFullScreen = _fullscreen;
                ScreenManager.Graphics.ApplyChanges();
            }
        }

        public bool Sound { get; set; }
        public ScreenManager ScreenManager { get; set; }
        private static Settings _instance;

        private Settings()
        {
            _fullscreen = false;
            Sound = true;
        }

        public static Settings Instance
        {
            get { return _instance ?? (_instance = new Settings()); }
        }
    }
}
