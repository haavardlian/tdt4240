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
    class SelectPlayerScreen : MenuScreen
    {
        #region Initialization

        private PowerUp _powerUp;
        /// <summary>
        /// Constructor.
        /// </summary>
        public SelectPlayerScreen(PowerUp powerUp)
            : base("Select player")
        {
            IsPopup = true;
            _powerUp = powerUp;
        }

        #endregion

        #region Handle Input

        public override void Activate(bool instancePreserved)
        {
            if (ControllingPlayer == null)
                throw new Exception("ControllingPlayer cannot be null in PowerUpSelectScreen");

            foreach (var player in PlayerManager.Instance.Players)
            {
                var item = new MenuItem(player.ToString());
                item.Selected += PlayerSelected;
                MenuEntries.Add(item);

            }
        }

        void PlayerSelected(object sender, PlayerEvent e)
        {
            PlayerManager.Instance.GetPlayer(e.PlayerIndex).RemovePowerUp(_powerUp);
            var pue = new PowerUpEvent(PlayerManager.Instance.Players[MenuEntries.IndexOf((MenuItem)sender)]);
            _powerUp.OnApply(this, pue);
            ExitScreen();
            ScreenManager.AddScreen(new DiceRoll(ScreenManager.Board.HandleDiceRollResult), e.PlayerIndex);
        }
        #endregion
    }
}
