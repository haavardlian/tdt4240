using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tdt4240.PowerUps
{
    class FreezePowerUp : PowerUp
    {

        public FreezePowerUp()
        {
            this.target = Target.Enemy;
            this.effect += FreezeEffect;
        }

        void FreezeEffect(object sender, PowerUpEvent powerUpEvent)
        {
            powerUpEvent.Player.Effect = Effect.Freeze;
        }
    }
}
