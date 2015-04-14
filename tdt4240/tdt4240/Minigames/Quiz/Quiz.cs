using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using tdt4240.Boards;
using DataTypes;

namespace tdt4240.Minigames.Quiz
{
    class Quiz : MiniGame
    {

        public static SupportedPlayers SupportedPlayers = SupportedPlayers.Three;
        private SpriteFont font;
        private QuestionRepository _questionRepository;
        private Vector2[] textPosition = new Vector2[4];


        public Quiz(Board board) : base(board)
        {
            this.Title = "Quiz";
            font = ScreenManager.Font;
            Vector2 _questionVector = new Vector2(ScreenManager.MaxWidth / 2);
            Vector2[] _alternativeVectors = {
                                            new Vector2(ScreenManager.MaxWidth / 2),
                                            new Vector2(ScreenManager.MaxWidth / 2),
                                            new Vector2(ScreenManager.MaxWidth / 2),
                                            new Vector2(ScreenManager.MaxWidth / 2)
                                            };
            _questionRepository = new QuestionRepository(font, content.Load<SerializableQuestion[]>("minigames/quiz/questions.xml"), _questionVector, _alternativeVectors);
        }

        public override void Activate(bool instancePreserved)
        {
            base.Activate(instancePreserved);

            if (!instancePreserved)
            {
                font = ScreenManager.Font;
                Background = new Background("background");
                ScreenManager.AddScreen(Background, null);

            }
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            foreach (Player player in PlayerManager.Instance.Players)
            {
                textPosition[(int)player.playerIndex] += player.Input.GetThumbstickVector();

                if (player.Input.IsButtonPressed(GameButtons.Y))
                {
                    NotifyDone(PlayerIndex.One);
                }

            }
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            foreach (Player player in PlayerManager.Instance.Players)
            {
                spriteBatch.DrawString(font, player.TestString, textPosition[(int)player.playerIndex], player.color);
            }


            spriteBatch.End();
        }

        public override void NotifyDone(PlayerIndex winningPlayerIndex)
        {
            base.NotifyDone(winningPlayerIndex);
        }
        
       
    }
}
