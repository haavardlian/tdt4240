using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace tdt4240.Minigames.BirdHunt
{
    class BirdFactory
    {
        private Texture2D _birdTexture;

        public BirdFactory(Texture2D birdTexture)
        {
            _birdTexture = birdTexture;
        }

        public Bird GenerateBird()
        {
            return new Bird(_birdTexture);
        }
    }
}
