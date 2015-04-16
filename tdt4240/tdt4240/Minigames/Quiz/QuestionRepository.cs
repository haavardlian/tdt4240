using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using tdt4240.Boards;
using tdt4240.Assets;
using DataTypes;
using Microsoft.Xna.Framework.Content;

namespace tdt4240.Minigames.Quiz
{
    class QuestionRepository
    {
        private SerializableQuestion[] _seralizedQuestions;
        private List<Question> _questions = new List<Question>();
        private SpriteFont _font;
        private Vector2 _questionPosition;
        private Vector2[] _alternativePositions;
        private Texture2D[] _alternativeTextures;
        private ContentManager _content;
        
        public QuestionRepository(SpriteFont font, ContentManager content, Vector2 questionVector, Vector2[] alternativeVector)
        {
            _content = content;
            _font = font;
            _seralizedQuestions = _content.Load<SerializableQuestion[]>("minigames/quiz/questions.xml");
            _questionPosition = questionVector;
            _alternativePositions = alternativeVector;
            _alternativeTextures[0] = _content.Load<Texture2D>("minigames/quiz/blue");
            _alternativeTextures[1] = _content.Load<Texture2D>("minigames/quiz/yellow");
            _alternativeTextures[2] = _content.Load<Texture2D>("minigames/quiz/green");
            _alternativeTextures[3] = _content.Load<Texture2D>("minigames/quiz/red");
            
        }

        private void BuildQuestions()
        {
            foreach (SerializableQuestion xmlQuestion in _seralizedQuestions)
            {
                Question question = new Question();
                question._font = _font;
                question._position = _questionPosition;

                question._question = xmlQuestion.Question;
                question.AddAlternative(new Alternative(_alternativeTextures[0], _alternativePositions[0]));
                question.AddAlternative(new Alternative(_alternativeTextures[1], _alternativePositions[1]));
                question.AddAlternative(new Alternative(_alternativeTextures[2], _alternativePositions[2]));
                question.AddAlternative(new Alternative(_alternativeTextures[3], _alternativePositions[3]));
                question._correctAlternative = xmlQuestion.CorrectAlternative;

                _questions.Add(question);
            }

        }


        public Question getQuestion()
        {
            return _questions[0];
        }
    }
}
