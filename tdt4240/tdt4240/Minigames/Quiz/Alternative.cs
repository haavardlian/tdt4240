using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tdt4240.Assets;


namespace tdt4240.Minigames.Quiz
{
    class Alternative : GraphicsObject
    {
        public String _text { get; set; }
        public Alternative(Texture2D texture, Vector2 position, String text)
            : base(texture, position)
        {
            _text = text;
        }


    }
}
