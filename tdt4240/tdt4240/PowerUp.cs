using System;

namespace tdt4240
{

    public enum Target
    {
        None,
        Self,
        Enemy,
        All
    }

    public abstract class PowerUp
    {
        public Target Target;

        protected event EventHandler<PowerUpEvent> effect;
        protected String Title;
        protected String Description;
        public String IconPath = "powerups/empty";

        public override String ToString()
        {
            return Title;
        }

        public void OnApply(object sender, PowerUpEvent e)
        {
            if (effect == null) return;
            effect(sender, e);
        }

        public PowerUp Clone()
        {
            return (PowerUp)MemberwiseClone();
        }
    }
}
