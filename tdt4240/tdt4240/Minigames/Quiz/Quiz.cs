using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using tdt4240.Boards;
using DataTypes;
using System.Collections.Generic;
using System.Text;
using System;

namespace tdt4240.Minigames.Quiz
{
    class Quiz : MiniGame
    {

        public static SupportedPlayers SupportedPlayers = SupportedPlayers.All;
        private SpriteFont font;
        private QuestionRepository _questionRepository;
        private Dictionary<PlayerIndex, int> _points = new Dictionary<PlayerIndex, int>();
        private static readonly int WIN_TRESHOLD = 300;

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
                MusicPlayer.GetInstance().StartLoopingSong("2");

                font = ScreenManager.Font;
                Background = new Background("background");
                ScreenManager.AddScreen(Background, null);
                _questionRepository = new QuestionRepository(font, content);
                _currentQuestion = _questionRepository.getQuestion();

                foreach (Player player in PlayerManager.Instance.Players)
                {
                    _points.Add(player.PlayerIndex, 0);
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
                        AnswerQuestion(player.PlayerIndex, _currentQuestion._alternatives[2]._text);
                   if (player.Input.IsButtonPressed(GameButtons.Y))
                       AnswerQuestion(player.PlayerIndex, _currentQuestion._alternatives[3]._text);
                   if (player.Input.IsButtonPressed(GameButtons.A))
                       AnswerQuestion(player.PlayerIndex, _currentQuestion._alternatives[0]._text);
                   if (player.Input.IsButtonPressed(GameButtons.B))
                       AnswerQuestion(player.PlayerIndex, _currentQuestion._alternatives[1]._text);
                }

                if (player.Input.IsButtonPressed(GameButtons.Down) && _scoreScreen)
                {
                    _scoreScreen = false;
                    _readyScreen = true;
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
                spriteBatch.DrawString(font, _currentQuestion._question, _currentQuestion._position*ScreenManager.GetScalingFactor(), Color.Black, 
                    0.0f, new Vector2(0, 0), ScreenManager.GetScalingFactor(), SpriteEffects.None, 0.0f);
                foreach (Alternative alternative in _currentQuestion._alternatives)
                {
                    spriteBatch.Draw(alternative.Texture, alternative.Position*ScreenManager.GetScalingFactor(), null, Color.White, 0.0f,
                        new Vector2(120, 20), ScreenManager.GetScalingFactor(),SpriteEffects.None, 0.0f);
                    spriteBatch.DrawString(font, alternative._text, (alternative.Position + new Vector2(120, 20)) * ScreenManager.GetScalingFactor(),
                        Color.Black, 0.0f, new Vector2(0, 0), ScreenManager.GetScalingFactor(), SpriteEffects.None, 0.0f);
                }

                spriteBatch.DrawString(font, "Timer: " + _questionTimer.ToString("0.00"), new Vector2(ScreenManager.Instance.GetWidth() / 6, ScreenManager.Instance.GetHeight() / 6), Color.Black, 0.0f, new Vector2(0, 0), ScreenManager.GetScalingFactor(), SpriteEffects.None, 0.0f);
            }

            if (_scoreScreen)
            { 
                foreach (Player player in PlayerManager.Instance.Players)
                {
                    string output = "Player " + player.PlayerIndex + ": " + _points[player.PlayerIndex];
                    Vector2 fontSize = font.MeasureString(output);
                    fontSize.X = 0;
                    fontSize.Y += (30*(int)player.PlayerIndex) * ScreenManager.GetScalingFactor();
                    spriteBatch.DrawString(font, output, new Vector2(ScreenManager.Instance.GetWidth() / 6, ScreenManager.Instance.GetHeight() / 6) + fontSize, Color.Black, 0.0f, new Vector2(0, 0), ScreenManager.GetScalingFactor(), SpriteEffects.None, 0.0f);
                }
            }

            if (_readyScreen)
            {
                foreach (Player player in PlayerManager.Instance.Players)
                {
                    spriteBatch.DrawString(font, "Get ready! " + _readyTimer.ToString("0.00"), new Vector2(ScreenManager.Instance.GetWidth() / 6, ScreenManager.Instance.GetHeight() / 6), Color.Black, 0.0f, new Vector2(0, 0), ScreenManager.GetScalingFactor(), SpriteEffects.None, 0.0f);
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
            if(!_currentQuestion.HasAnswered(player))
                _currentAnswers++;
            if (answer.Equals(_currentQuestion._correctAlternative))
            {
                _points[player] += (int)System.Math.Ceiling(_questionTimer*10);
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
                if (entry.Value >= WIN_TRESHOLD)
                {
                    PlayerIndex winner = entry.Key;
                    foreach (KeyValuePair<PlayerIndex, int> player in _points)
                    {
                        if (player.Value > _points[winner])
                        {
                            winner = player.Key;
                        }
                    }
                    NotifyDone(winner);
                }
            }
        }

        private void StartRound()
        {
            _scoreScreen = false;
            _readyScreen = false;
            _readyTimer = 2;
            _currentAnswers = 0;
            _questionTimer = 10;
            _currentQuestion = _questionRepository.getQuestion();
            _activeQuestion = true;
        }
   

    }

    
}
