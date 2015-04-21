#region File Description
//-----------------------------------------------------------------------------
// PauseMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements



#endregion

namespace tdt4240.Menu
{
    /// <summary>
    /// The pause menu comes up over the top of the game,
    /// giving the player options to resume or quit.
    /// </summary>
    class PauseMenuScreen : MenuScreen
    {
        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public PauseMenuScreen(string title = "Paused")
            : base(title)
        {
            // Create our menu entries.
            MenuItem resumeGameMenuEntry = new MenuItem("Resume Game");
            MenuItem settingsGameMenuEntry = new MenuItem("Settings");
            MenuItem quitGameMenuEntry = new MenuItem("Quit Game");

            // Hook up menu event handlers.
            resumeGameMenuEntry.Selected += OnCancel;
            quitGameMenuEntry.Selected += QuitGameMenuEntrySelected;
            settingsGameMenuEntry.Selected += SettingsGameMenuEntrySelected;

            // Add entries to the menu.
            MenuEntries.Add(resumeGameMenuEntry);
            MenuEntries.Add(settingsGameMenuEntry);
            MenuEntries.Add(quitGameMenuEntry);
            
        }

        #endregion

        #region Handle Input

        private void SettingsGameMenuEntrySelected(object sender, PlayerEvent playerEvent)
        {
            ScreenManager.AddScreen(new OptionsMenuScreen(), null);
        }

        /// <summary>
        /// Event handler for when the Quit Game menu entry is selected.
        /// </summary>
        void QuitGameMenuEntrySelected(object sender, PlayerEvent e)
        {
            PlayerManager.Instance.Players.Clear();
            InputDevice.Clear();
            ScreenManager.AddScreen(new Background("background"), null);
            ScreenManager.AddScreen(new MainMenu(), null);
        }
        #endregion
    }
}
