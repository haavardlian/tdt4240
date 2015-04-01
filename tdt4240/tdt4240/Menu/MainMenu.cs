#region File Description
//-----------------------------------------------------------------------------
// MainMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements

using Microsoft.Xna.Framework;

#endregion

namespace tdt4240.Menu
{
    /// <summary>
    /// The main menu screen is the first thing displayed when the game starts up.
    /// </summary>
    class MainMenu : MenuScreen
    {
        #region Initialization


        /// <summary>
        /// Constructor fills in the menu contents.
        /// </summary>
        public MainMenu()
            : base("Main Menu")
        {
            // Create our menu entries.
            MenuItem playGameMenuEntry = new MenuItem("Start Game");
            MenuItem optionsMenuEntry = new MenuItem("Options");
            MenuItem aboutMenuEntry = new MenuItem("About");
            MenuItem exitMenuEntry = new MenuItem("Exit");

            // Hook up menu event handlers.
            playGameMenuEntry.Selected += PlayGameMenuEntrySelected;
            optionsMenuEntry.Selected += OptionsMenuEntrySelected;
            aboutMenuEntry.Selected += AboutMenuEntrySelected;
            exitMenuEntry.Selected += OnCancel;

            // Add entries to the menu.
            MenuEntries.Add(playGameMenuEntry);
            MenuEntries.Add(optionsMenuEntry);
            MenuEntries.Add(exitMenuEntry);
        }


        #endregion

        #region Handle Input


        /// <summary>
        /// Event handler for when the Play Game menu entry is selected.
        /// </summary>
        void PlayGameMenuEntrySelected(object sender, PlayerEvent e)
        {
            //TODO send user to select player menu

            ScreenManager.AddScreen(new PlayerSelect(), e.PlayerIndex);

            //LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
            //                   new GameplayScreen());
        }


        /// <summary>
        /// Event handler for when the Options menu entry is selected.
        /// </summary>
        void OptionsMenuEntrySelected(object sender, PlayerEvent e)
        {
            //TODO create option menu
            ScreenManager.AddScreen(new OptionsMenuScreen(), e.PlayerIndex);
        }

        void AboutMenuEntrySelected(object sender, PlayerEvent e)
        {
            //TODO create about page
        }

        protected override void OnCancel(PlayerIndex playerIndex)
        {
            ScreenManager.Game.Exit();
        }

        #endregion
    }
}
