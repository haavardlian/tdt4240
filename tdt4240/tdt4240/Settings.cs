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
                    ScreenManager.SetFullScreen();
                else
                    ScreenManager.SetResolution(1280, 720);

                Stored.Default.Fullscreen = _fullscreen;
                Stored.Default.Save();
            }
        }

        public bool Sound
        {
            get { return _sound; }
            set
            {
                _sound = value;
                Stored.Default.Sound = _sound;
                Stored.Default.Save();
            }
        }

        private bool _sound;

        public ScreenManager ScreenManager { get; set; }
        private static Settings _instance;

        private Settings()
        {
            _fullscreen = Stored.Default.Fullscreen;
            Sound = Stored.Default.Sound;
        }

        public static Settings Instance
        {
            get { return _instance ?? (_instance = new Settings()); }
        }
    }
}
