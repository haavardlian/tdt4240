#region File Description
//-----------------------------------------------------------------------------
// OptionsMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
using tdt4240;
#endregion

namespace menu.tdt4240
{
    /// <summary>
    /// The options screen is brought up over the top of the main menu
    /// screen, and gives the user a chance to configure the game
    /// in various hopefully useful ways.
    /// </summary>
    class OptionsMenuScreen : MenuScreen
    {
        #region Fields

        MenuItem soundMenuEntry;
        MenuItem fullscreenMenuEntry;

        //TODO move to global
        bool sound = true;
        bool fullscreen = true;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public OptionsMenuScreen()
            : base("Options")
        {
            // Create our menu entries.
            soundMenuEntry = new MenuItem(string.Empty);
            fullscreenMenuEntry = new MenuItem(string.Empty);

            SetMenuEntryText();

            MenuItem back = new MenuItem("Back");

            // Hook up menu event handlers.
            soundMenuEntry.Selected += SoundMenuEntrySelected;
            fullscreenMenuEntry.Selected += FullscreenMenuEntrySelected;
            back.Selected += OnCancel;

            // Add entries to the menu.
            MenuEntries.Add(soundMenuEntry);
            MenuEntries.Add(fullscreenMenuEntry);
            MenuEntries.Add(back);
        }


        /// <summary>
        /// Fills in the latest values for the options screen menu text.
        /// </summary>
        void SetMenuEntryText()
        {
            soundMenuEntry.Text = "Sound: " + (sound ? " on" : "off");
            fullscreenMenuEntry.Text = "Fullscreen: " + (fullscreen ? " on" : "off");
        }


        #endregion

        #region Handle Input

        void SoundMenuEntrySelected(object sender, PlayerEvent e)
        {
            sound = !sound;
            SetMenuEntryText();
        }


        void FullscreenMenuEntrySelected(object sender, PlayerEvent e)
        {
            fullscreen = !fullscreen;
            SetMenuEntryText();
        }

        #endregion
    }
}
