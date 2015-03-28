using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        protected Target target;
        protected event EventHandler<PowerUpEvent> effect;
    }
}
