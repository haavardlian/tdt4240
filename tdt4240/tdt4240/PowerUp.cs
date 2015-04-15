﻿using System;

namespace tdt4240
{

    enum Target
    {
        None,
        Self,
        Enemy,
        All
    }

    abstract class PowerUp
    {
        protected Target Target;
        protected event EventHandler<PowerUpEvent> effect;
        protected String Title;
        protected String Description;
        public String IconPath = "powerups/empty";


        public override String ToString()
        {
            return Title;
        }

        public PowerUp Clone()
        {
            return (PowerUp)MemberwiseClone();
        }
    }
}
