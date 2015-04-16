using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using tdt4240.Boards;
using DataTypes;
using System.Collections.Generic;

namespace tdt4240.Minigames.Quiz
{
    class Quiz : MiniGame
    {

        public static SupportedPlayers SupportedPlayers = SupportedPlayers.All;
        private SpriteFont font;
        private QuestionRepository _questionRepository;
        private Dictionary<PlayerIndex, int> _points = new Dictionary<PlayerIndex, int>();

        private Question _currentQuestion;

        private float _questionTimer = 10;
        private bool _activeQuestion = false;
        private int _currentAnswers = 0;

        private bool _scoreScreen = false;
        private bool _readyScreen = true;
        private float _readyTimer = 2;
        

        public Quiz(Board board) : base(board)
        {
            this.Title = "Quiz";
        }

        public override void Activate(bool instancePreserved)
        {
            base.Activate(instancePreserved);

            if (!instancePreserved)
            {
                font = ScreenManager.Font;
                Background = new Background("background");
                ScreenManager.AddScreen(Background, null);
                _questionRepository = new QuestionRepository(font, content);
                _currentQuestion = _questionRepository.getQuestion();

                foreach (Player player in PlayerManager.Instance.Players)
                {
                    _points.Add(player.playerIndex, 0);
                }
            }
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            foreach (Player player in PlayerManager.Instance.Players)
            {
                if (_activeQuestion)
                {
                   if (player.Input.IsButtonPressed(GameButtons.X))
                        AnswerQuestion(player.playerIndex, _currentQuestion._alternatives[0]._text);
                   if (player.Input.IsButtonPressed(GameButtons.A))
                       AnswerQuestion(player.playerIndex, _currentQuestion._alternatives[1]._text);
                   if (player.Input.IsButtonPressed(GameButtons.Y))
                       AnswerQuestion(player.playerIndex, _currentQuestion._alternatives[2]._text);
                   if (player.Input.IsButtonPressed(GameButtons.B))
                       AnswerQuestion(player.playerIndex, _currentQuestion._alternatives[3]._text);
                }

                if (player.Input.IsButtonPressed(GameButtons.Down))
                {
                    //NotifyDone(PlayerIndex.One);
                    StartRound();
                }
                
            }
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
            if (_activeQuestion)
            {
                _questionTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (_questionTimer < 0)
            {
                EndRound();
            }

            if (_readyScreen)
            {
                _readyTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (_readyTimer < 0)
            {
                StartRound();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();
            if(_activeQuestion)
            {
                spriteBatch.DrawString(font, _currentQuestion._question, _currentQuestion._position, Color.Black);
                foreach (Alternative alternative in _currentQuestion._alternatives)
                {
                    spriteBatch.Draw(alternative.Texture, alternative.Position, Color.White);
                    spriteBatch.DrawString(font, alternative._text, alternative.Position + new Vector2(120, 20), Color.Black);
                }

                spriteBatch.DrawString(font, "Timer: " + _questionTimer.ToString("0.00"), new Vector2(ScreenManager.MaxWidth / 6, ScreenManager.MaxHeight / 6), Color.Black);
            }

            if (_scoreScreen)
            {
                foreach (Player player in PlayerManager.Instance.Players)
                {
                    spriteBatch.DrawString(font, "Player " + player.playerIndex + ": " + _points[player.playerIndex], new Vector2(ScreenManager.MaxWidth / 6, ScreenManager.MaxHeight / 6), Color.Black);
                }
            }

            if (_readyScreen)
            {
                foreach (Player player in PlayerManager.Instance.Players)
                {
                    spriteBatch.DrawString(font, "Get ready! " + _readyTimer.ToString("0.00"), new Vector2(ScreenManager.MaxWidth / 6, ScreenManager.MaxHeight / 6), Color.Black);
                }
            }
            

            spriteBatch.End();
        }

        public override void NotifyDone(PlayerIndex winningPlayerIndex)
        {
            base.NotifyDone(winningPlayerIndex);
        }

        private void AnswerQuestion(PlayerIndex player, string answer)
        {
            _currentAnswers++;
            if (answer.Equals(_currentQuestion._correctAlternative))
            {
                _points[player] += (int)System.Math.Ceiling(_questionTimer*10);
                //_points[player] ++;  
            }

            if (_currentAnswers == PlayerManager.Instance.NumberOfPlayers)
            {
                EndRound();
            }
        }

        private void EndRound()
        {
            _activeQuestion = false;
            _scoreScreen = true;

            foreach (KeyValuePair<PlayerIndex, int> entry in _points)
            {
                if(entry.Value >= 300) {
                    NotifyDone(entry.Key);
                }
            }
        }

        private void StartRound()
        {
            _readyScreen = false;
            _readyTimer = 2;
            _currentAnswers = 0;
            _questionTimer = 10;
            _scoreScreen = false;
            _currentQuestion = _questionRepository.getQuestion();
            _activeQuestion = true;
        }
        
       
    }
}
