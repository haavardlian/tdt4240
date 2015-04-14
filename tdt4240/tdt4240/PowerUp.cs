using System;

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
        protected String iconPath = "powerups/unknown";

        public String IconPath
        {
            get { return iconPath;}
        }


        public override String ToString()
        {
            return Title;
        }
    }
}
