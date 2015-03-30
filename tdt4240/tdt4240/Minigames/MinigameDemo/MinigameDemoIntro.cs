using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using tdt4240.Boards;

namespace tdt4240.Minigames.MinigameDemo
{
    class MinigameDemoIntro : MinigameIntro
    {
        public MinigameDemoIntro()
        {
            ControllerButtons.Add(Buttons.A, "Exit game");
            ControllerButtons.Add(Buttons.B, "Test 1");
            ControllerButtons.Add(Buttons.RightTrigger, "Test 2");
        }
    }
}
