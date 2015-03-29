using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tdt4240.Minigames.BirdHunt
{
    class BirdFactory
    {
        private Texture2D _birdTexture;
        private readonly Random _random;

        public BirdFactory()
        {
            _random = new Random();
        }

        public Bird GenerateBird()
        {
            return new Bird(_birdTexture, new Vector2(_random.Next(0,ScreenManager.MaxWidth), ScreenManager.MaxHeight - _birdTexture.Height));
        }

        public void SetTexture(Texture2D birdTexture)
        {
            _birdTexture = birdTexture;
        }
    }
}
