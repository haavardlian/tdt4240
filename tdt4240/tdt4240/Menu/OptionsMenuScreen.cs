namespace tdt4240.Menu
{
    class OptionsMenuScreen : MenuScreen
    {
        readonly MenuItem _soundMenuEntry;
        readonly MenuItem _fullscreenMenuEntry;

        public OptionsMenuScreen()
            : base("Options")
        {
            // Create our menu entries.
            _soundMenuEntry = new MenuItem(string.Empty);
            _fullscreenMenuEntry = new MenuItem(string.Empty);

            SetMenuEntryText();

            var back = new MenuItem("Back");

            // Hook up menu event handlers.
            _soundMenuEntry.Selected += SoundMenuEntrySelected;
            _fullscreenMenuEntry.Selected += FullscreenMenuEntrySelected;
            back.Selected += OnCancel;

            // Add entries to the menu.
            MenuEntries.Add(_soundMenuEntry);
            MenuEntries.Add(_fullscreenMenuEntry);
            MenuEntries.Add(back);
        }

        void SetMenuEntryText()
        {
            _soundMenuEntry.Text = "Sound: " + (Settings.Instance.Sound ? " on" : "off");
            _fullscreenMenuEntry.Text = "Fullscreen: " + (Settings.Instance.Fullscreen ? " on" : "off");
        }

        void SoundMenuEntrySelected(object sender, PlayerEvent e)
        {
            Settings.Instance.Sound = !Settings.Instance.Sound;
            SetMenuEntryText();
        }


        void FullscreenMenuEntrySelected(object sender, PlayerEvent e)
        {
            Settings.Instance.Fullscreen = !Settings.Instance.Fullscreen;
            SetMenuEntryText();
        }
    }
}
