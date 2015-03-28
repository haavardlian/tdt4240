#region File Description
//-----------------------------------------------------------------------------
// PlayerIndexEventArgs.cs
//
// XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using Microsoft.Xna.Framework;
#endregion

namespace tdt4240
{
    class PowerUpEvent : EventArgs
    {
        public PowerUpEvent(Player player)
        {
            this.player = player;
        }

        public Player Player
        {
            get { return player; }
        }

        Player player;
    }
}
