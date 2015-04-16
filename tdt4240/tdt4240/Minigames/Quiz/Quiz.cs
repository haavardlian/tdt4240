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
        private Question _currentQuestion;
        private float _questionTimer = 10;
        private bool _activeQuestion = false;
        private Dictionary<Player, int> _points = new Dictionary<Player, int>();

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
                    _activeQuestion = true;
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
                    spriteBatch.DrawString(font, "TEST", alternative.Position + new Vector2(120, 20), Color.Black);
                }

                spriteBatch.DrawString(font, "Timer: " + _questionTimer.ToString("0.00"), new Vector2(ScreenManager.MaxWidth / 6, ScreenManager.MaxHeight / 6), Color.Black);
            }
            

            spriteBatch.End();
        }

        public override void NotifyDone(PlayerIndex winningPlayerIndex)
        {
            base.NotifyDone(winningPlayerIndex);
        }

        private void AnswerQuestion(PlayerIndex player, string answer)
        {

        }

        private void EndRound()
        {
            _activeQuestion = false;
            _questionTimer = 10;
            _currentQuestion = _questionRepository.getQuestion();
        }

        private void StartRound()
        {

        }
        
       
    }
}
