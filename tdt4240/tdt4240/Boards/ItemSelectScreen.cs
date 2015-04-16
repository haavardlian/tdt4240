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

using System;
using Microsoft.Xna.Framework;

namespace tdt4240.Boards
{
    /// <summary>
    /// The pause menu comes up over the top of the game,
    /// giving the player options to resume or quit.
    /// </summary>
    class ItemSelectScreen : MenuScreen
    {
        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public ItemSelectScreen()
            : base("Select PowerUp or roll")
        {
            IsPopup = true;
            // Create our menu entries.
            MenuItem useMenuItem = new MenuItem("Use PowerUp");
            MenuItem rollMenuItem = new MenuItem("Roll");

            // Hook up menu event handlers.
            rollMenuItem.Selected += UseRollMenuEntrySelected;
            useMenuItem.Selected += UseItemMenuEntrySelected;

            // Add entries to the menu.
            MenuEntries.Add(useMenuItem);
            MenuEntries.Add(rollMenuItem);
            
        }

        #endregion

        #region Handle Input

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            if (ControllingPlayer == null)
            {
                PlayerManager.Instance.Players.ForEach(x => UpdateInputForPlayer(x.PlayerIndex));
            }
            else
            {
                UpdateInputForPlayer(PlayerManager.Instance.GetPlayer(ControllingPlayer).PlayerIndex);
            }
            
        }

        void UpdateInputForPlayer(PlayerIndex playerIndex)
        {
            var inputState = PlayerManager.Instance.GetPlayer(playerIndex).Input;

            if (inputState.IsButtonPressed(GameButtons.Down))
            {
                selectedEntry++;

                if (selectedEntry >= menuEntries.Count)
                    selectedEntry = 0;
            }
            else if (inputState.IsButtonPressed(GameButtons.Up))
            {
                selectedEntry--;

                if (selectedEntry < 0)
                    selectedEntry = menuEntries.Count - 1;
            }
            else if (inputState.IsButtonPressed(GameButtons.A))
            {
                OnSelectEntry(selectedEntry, playerIndex);
            }
        }

        void UseRollMenuEntrySelected(object sender, PlayerEvent e)
        {
            ExitScreen();
            ScreenManager.AddScreen(new DiceRoll(ScreenManager.Board.HandleDiceRollResult), e.PlayerIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        void UseItemMenuEntrySelected(object sender, PlayerEvent e)
        {
            ScreenManager.RemoveScreen(this);
            ScreenManager.AddScreen(new PowerUpSelectScreen(), e.PlayerIndex);
        }
        #endregion
    }
}
