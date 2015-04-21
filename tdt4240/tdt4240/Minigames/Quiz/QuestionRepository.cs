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
        private List<Question> _usedQuestions = new List<Question>();
        private SpriteFont _font;
        private Vector2 _questionPosition;
        private Vector2[] _alternativePositions = new Vector2[4];
        private Texture2D[] _alternativeTextures = new Texture2D[4];
        private ContentManager _content;
        private static Random rnd = new Random();
        
        public QuestionRepository(SpriteFont font, ContentManager content)
        {
            _content = content;
            _font = font;
            _seralizedQuestions = _content.Load<SerializableQuestion[]>("minigames/quiz/questions");
            _questionPosition = new Vector2(ScreenManager.MaxWidth / 6, ScreenManager.MaxHeight / 8);
            _alternativePositions[0] = new Vector2(ScreenManager.MaxWidth / 10, ScreenManager.MaxHeight / 3);
            _alternativePositions[1] = new Vector2(ScreenManager.MaxWidth / 2, ScreenManager.MaxHeight / 3);
            _alternativePositions[2] = new Vector2(ScreenManager.MaxWidth / 10, ScreenManager.MaxHeight / 2);
            _alternativePositions[3] = new Vector2(ScreenManager.MaxWidth / 2, ScreenManager.MaxHeight / 2);
            _alternativeTextures[0] = _content.Load<Texture2D>("minigames/quiz/button_blue_x");
            _alternativeTextures[1] = _content.Load<Texture2D>("minigames/quiz/button_yellow_y");
            _alternativeTextures[2] = _content.Load<Texture2D>("minigames/quiz/button_green_a");
            _alternativeTextures[3] = _content.Load<Texture2D>("minigames/quiz/button_red_b");
            BuildQuestions();
        }

        private void BuildQuestions()
        {
            foreach (SerializableQuestion xmlQuestion in _seralizedQuestions)
            {
                Question question = new Question();
                question._font = _font;
                question._position = _questionPosition;

                question._question = xmlQuestion.Question;
                question.AddAlternative(new Alternative(_alternativeTextures[0], _alternativePositions[0], xmlQuestion.Alternative_1));
                question.AddAlternative(new Alternative(_alternativeTextures[1], _alternativePositions[1], xmlQuestion.Alternative_2));
                question.AddAlternative(new Alternative(_alternativeTextures[2], _alternativePositions[2], xmlQuestion.Alternative_3));
                question.AddAlternative(new Alternative(_alternativeTextures[3], _alternativePositions[3], xmlQuestion.Alternative_4));
                question._correctAlternative = xmlQuestion.CorrectAlternative;

                _questions.Add(question);
            }

        }


        public Question getQuestion()
        {
            Question candidate = _questions[rnd.Next(_questions.Count)];
            while (_usedQuestions.Contains(candidate))
            {
                if (_usedQuestions.Count == _questions.Count)
                {
                    _usedQuestions.Clear();
                    //return null;
                }
                candidate = _questions[rnd.Next(_questions.Count)];
            }
            _usedQuestions.Add(candidate);
            return candidate;
        }

    }
}
