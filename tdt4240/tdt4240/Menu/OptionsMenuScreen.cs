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
using Microsoft.Xna.Framework.Graphics;
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

        readonly MenuItem soundMenuEntry;
        readonly MenuItem fullscreenMenuEntry;

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

            var back = new MenuItem("Back");

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
            soundMenuEntry.Text = "Sound: " + (Settings.Instance.Sound ? " on" : "off");
            fullscreenMenuEntry.Text = "Fullscreen: " + (Settings.Instance.Fullscreen ? " on" : "off");
        }


        #endregion

        #region Handle Input

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

        #endregion
    }
}
