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
    class PowerUpSelectScreen : MenuScreen
    {
        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public PowerUpSelectScreen()
            : base("Select PowerUp")
        {
            IsPopup = true;
                   
        }

        #endregion

        #region Handle Input

        public override void Activate(bool instancePreserved)
        {
            if (ControllingPlayer == null)
                throw new Exception("ControllingPlayer cannot be null in PowerUpSelectScreen");

            foreach (var powerUp in PlayerManager.Instance.GetPlayer(ControllingPlayer).PowerUps)
            {
                var item = new MenuItem(powerUp.ToString());
                item.Selected += PowerUpSelected;
                MenuEntries.Add(item);

            }
        }

        void PowerUpSelected(object sender, PlayerEvent e)
        {
            ExitScreen();
            ScreenManager.AddScreen(new DiceRoll(ScreenManager.Board.HandleDiceRollResult), e.PlayerIndex);
            Console.WriteLine("Chose " + PlayerManager.Instance.GetPlayer(e.PlayerIndex).PowerUps[MenuEntries.IndexOf((MenuItem)sender)]);
        }
        #endregion
    }
}
