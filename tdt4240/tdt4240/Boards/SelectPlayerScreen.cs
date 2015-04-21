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
        private Player _player;
        /// <summary>
        /// Constructor.
        /// </summary>
        public SelectPlayerScreen(PowerUp powerUp, Player player)
            : base("Select player")
        {
            IsPopup = true;
            _powerUp = powerUp;
            _player = player;
        }

        #endregion

        #region Handle Input

        public override void Activate(bool instancePreserved)
        {
            if (ControllingPlayer == null)
                throw new Exception("ControllingPlayer cannot be null in PowerUpSelectScreen");

            
        }

        public override void Added()
        {
            if (_powerUp.Target == Target.Self)
            {
                _player.RemovePowerUp(_powerUp);
                _powerUp.OnApply(this, new PowerUpEvent(_player));
                ExitScreen();
                ScreenManager.AddScreen(new DiceRoll(ScreenManager.Board.HandleDiceRollResult), _player.PlayerIndex);
                return;
            }

            foreach (var player in PlayerManager.Instance.Players)
            {
                if (_powerUp.Target == Target.Enemy && player == _player)
                    continue;
                var item = new MenuItem(player.ToString());
                item.Selected += PlayerSelected;
                MenuEntries.Add(item);
            }
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            var inputState = PlayerManager.Instance.GetPlayer(ControllingPlayer).Input;

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
                PlayerSelected(selectedEntry, new PlayerEvent(PlayerManager.Instance.GetPlayer(ControllingPlayer).PlayerIndex));
            }
        }

        void PlayerSelected(object sender, PlayerEvent e)
        {
            var index = (int) sender;
            if (_powerUp.Target == Target.Enemy && index > (int) e.PlayerIndex)
                index++;

            PlayerManager.Instance.GetPlayer(e.PlayerIndex).RemovePowerUp(_powerUp);
            var pue = new PowerUpEvent(PlayerManager.Instance.Players[index]);
            _powerUp.OnApply(this, pue);
            ExitScreen();
            ScreenManager.AddScreen(new DiceRoll(ScreenManager.Board.HandleDiceRollResult), ScreenManager.Board.CurrentPlayer.PlayerIndex);

        }
        #endregion
    }
}
