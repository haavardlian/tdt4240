using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tdt4240
{
    interface IDiceRoller
    {
        void DiceResultHandler(Player player, int result);
    }
}
