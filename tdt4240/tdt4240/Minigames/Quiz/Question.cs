using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tdt4240.Assets;


namespace tdt4240.Minigames.Quiz
{
    class Question
    {
        public Vector2 _position { get; set; }
        public String _question{ get; set; }
        public SpriteFont _font { get; set; }
        public List<Alternative> _alternatives { get; set; }
        public String _correctAlternative { get; set; }

        public Question(Vector2 position, SpriteFont font, String question)
        {
            _position = position;
            _font = font;
            _question = question;
            _alternatives = new List<Alternative>();
        }

        public Question()
        {
        }

        public void AddAlternative(Alternative alternative)
        {
            _alternatives.Add(alternative);
        }

    }
}
