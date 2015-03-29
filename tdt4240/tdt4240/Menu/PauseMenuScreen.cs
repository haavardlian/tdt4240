#region File Description
//-----------------------------------------------------------------------------
// PauseMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements

using System;
using Microsoft.Xna.Framework;
using tdt4240;
#endregion

namespace menu.tdt4240
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
        public PauseMenuScreen()
            : base("Paused")
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

            ScreenManager.AddScreen(new Background("background"), null);
            ScreenManager.AddScreen(new MainMenu(), null);

            /*
            const string message = "Are you sure you want to quit this game?";

            MessageBoxScreen confirmQuitMessageBox = new MessageBoxScreen(message);

            confirmQuitMessageBox.Accepted += ConfirmQuitMessageBoxAccepted;

            ScreenManager.AddScreen(confirmQuitMessageBox, ControllingPlayer);
             */
        }


        /// <summary>
        /// Event handler for when the user selects ok on the "are you sure
        /// you want to quit" message box. This uses the loading screen to
        /// transition from the game back to the main menu screen.
        /// </summary>
        void ConfirmQuitMessageBoxAccepted(object sender, PlayerEvent e)
        {
            //LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(), new MainMenuScreen());
        }


        #endregion
    }
}
