using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using tdt4240.Menu;

namespace tdt4240.Boards
{
    public abstract class Board : GameScreen
    {
        protected Texture2D BackgroundTexture;
        protected Texture2D PieceTexture;
        protected Texture2D TileTexture;
        protected Texture2D PlayerBackground;
        protected Texture2D EmptyPowerUp;
        protected Texture2D DownTexture;
        protected Texture2D StarTexture;
        protected ContentManager Content;
        protected SpriteFont Font;
        protected readonly Vector2[] Offsets = { new Vector2(-20, -20), new Vector2(20, -20), new Vector2(-20, 20), new Vector2(20, 20) };
        protected readonly List<BoardPosition> Positions = new List<BoardPosition>();

        protected List<Vector2> PlayerBackgroundPositions = new List<Vector2>();
        protected List<Vector2> PlayerInfoPositions = new List<Vector2>();

        protected List<Type> MiniGames;

        private bool _miniGame = false;

        public Player CurrentPlayer { get; private set; }

        public void NextPlayer()
        {
            var nextIndex = ((int) CurrentPlayer.PlayerIndex) + 1;
            if (nextIndex >= PlayerManager.Instance.NumberOfPlayers)
                nextIndex = 0;
            CurrentPlayer = PlayerManager.Instance.Players[nextIndex];
        }

        public void MiniGameDone(PlayerIndex winningPlayerIndex)
        {
            var winner = PlayerManager.Instance.GetPlayer(winningPlayerIndex);

            ScreenManager.AddScreen(new MinigameWinnerScreen(winner), null);      
        }

        public override void Activate(bool instancePreserved)
        {
  
            if (instancePreserved) return;
            if (Content == null)
                Content = new ContentManager(ScreenManager.Game.Services, "Content");

            
            LoadContent();
            CurrentPlayer = PlayerManager.Instance.Players[0];

            Positions.Add(new BoardPosition(new Vector2(370, 1000), PositionType.Start, TileTexture));
            
            PlayerManager.Instance.Players.ForEach(player => player.BoardPosition = Positions[0]);

            for (int i = 0; i < 4; i++)
            {
                PlayerBackgroundPositions.Add(new Vector2());
                PlayerInfoPositions.Add(new Vector2());
            }

            AddPositions();

            Positions.Last().NavigateTo += OnFinish;
            Positions.Last().Icon = StarTexture;

            MiniGames = ViableMiniGames(PlayerManager.Instance.NumberOfPlayers);

            AssetManager.Instance.AddAsset<Texture2D>("powerups/empty");
            AssetManager.Instance.AddAsset<Texture2D>("powerups/freeze");
            AssetManager.Instance.AddAsset<Texture2D>("powerups/double_dice");
            AssetManager.Instance.AddAsset<Texture2D>("powerups/unknown");

            ScreenManager.Board = this;

            float x = ScreenManager.MaxWidth - Font.MeasureString("Player X").X - 5;
            float y = ScreenManager.MaxHeight - 180;

            PlayerBackgroundPositions[0] = new Vector2(5, 0);
            PlayerBackgroundPositions[1] = new Vector2(ScreenManager.MaxWidth, 0);
            PlayerBackgroundPositions[2] = new Vector2(5, ScreenManager.MaxHeight);
            PlayerBackgroundPositions[3] = new Vector2(ScreenManager.MaxWidth, ScreenManager.MaxHeight);

            PlayerInfoPositions[0] = new Vector2(0, 0);
            PlayerInfoPositions[1] = new Vector2(x, 0);
            PlayerInfoPositions[2] = new Vector2(0, y);
            PlayerInfoPositions[3] = new Vector2(x, y);

            if (PlayerManager.Instance.NumberOfPlayers >= 3)
            {
                PlayerManager.Instance.Players[2].Effect = Effect.DoubleRoll;
            }
        }

        public override void Added()
        {
            ScreenManager.AddScreen(new DiceRoll(HandleDiceRollResult), CurrentPlayer.PlayerIndex);
        }

        private void OnFinish(object sender, EventArgs eventArgs)
        {
            var player = eventArgs as Player;

            Console.WriteLine(player + " is the winner!!!");
        }

        protected void NavigateToArrow(object sender, EventArgs args)
        {
            var player = args as Player;

            if (player == null) return;

            var index = Positions.IndexOf(player.BoardPosition);
            player.BoardPosition = Positions[index + player.BoardPosition.MoveAmount];
        }

        protected virtual void AddPositions()
        {
           
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            if (PlayerManager.Instance.Players.Any(player => player.Input.IsButtonPressed(GameButtons.Back)))
            {
                ScreenManager.AddScreen(new PauseMenuScreen(), null);
            }
        }

        public void LoadContent()
        {
            //_backgroundTexture = _content.Load<Texture2D>("board/board");
            PieceTexture = AssetManager.Instance.AddAsset<Texture2D>("board/piece2");
            Font = AssetManager.Instance.AddAsset<SpriteFont>("fonts/smallfont");
            TileTexture = AssetManager.Instance.AddAsset<Texture2D>("board/tile");
            PlayerBackground = AssetManager.Instance.AddAsset<Texture2D>("black_circle");
            EmptyPowerUp = AssetManager.Instance.AddAsset<Texture2D>("powerups/empty");
            DownTexture = AssetManager.Instance.AddAsset<Texture2D>("powers/down");
            StarTexture = AssetManager.Instance.AddAsset<Texture2D>("powers/star");
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);

        }

        public override void Draw(GameTime gameTime)
        {
            var spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            //spriteBatch.Draw(_backgroundTexture, Vector2.Zero, null, new Color(TransitionAlpha, TransitionAlpha, TransitionAlpha), 0f, Vector2.Zero, ScreenManager.GetScalingFactor(), SpriteEffects.None, 0f);
            ScreenManager.GraphicsDevice.Clear(Color.DarkSeaGreen);
            Positions.ForEach(x => x.Draw(spriteBatch));


            BoardPosition lastPosition = null;
            var index = 0;
            foreach (var player in PlayerManager.Instance.Players)
            {
                if (lastPosition == player.BoardPosition)
                {
                    index++;
                }
                else
                {
                    index = 0;
                    lastPosition = player.BoardPosition;
                }

                if (player.BoardPosition == null) continue;
                var pos = player.BoardPosition.Position;

                var n = PlayerManager.Instance.Players.Count(x => x.BoardPosition == player.BoardPosition);
                if(n > 1)
                    pos += Offsets[index];
                pos *= ScreenManager.GetScalingFactor();

                DrawStatus(spriteBatch, player);
                spriteBatch.Draw(PieceTexture, pos, null, player.Color, 0f, new Vector2(32, 45) * ScreenManager.GetScalingFactor(), ScreenManager.GetScalingFactor(), SpriteEffects.None, 0f);
            }

            //Tempcode for testing positions for all 4 players
            /*
            for (int i = 0; i < 4; i++)
            {
                spriteBatch.DrawString(_font, "Player " + i, _playerInfoPositions[i], PlayerManager.Colors[i]);

                float diff = ScreenManager.GetScalingFactor()*50;
                float scale = ScreenManager.GetScalingFactor() * 0.1f;

                Vector2 powerUpPosition = new Vector2(_playerInfoPositions[i].X + 20, _playerInfoPositions[i].Y + diff);

                spriteBatch.Draw(_emptyPowerUp, powerUpPosition, null, Color.White, 0f,
                    new Vector2(0, 0), scale, SpriteEffects.None, 0f);

                powerUpPosition = new Vector2(powerUpPosition.X + diff * 1.5f, powerUpPosition.Y);

                spriteBatch.Draw(_emptyPowerUp, powerUpPosition, null, Color.White, 0f,
                    new Vector2(0, 0), scale, SpriteEffects.None, 0f);

                Vector2 statusPosition = new Vector2(_playerInfoPositions[i].X, _playerInfoPositions[i].Y + diff * 2);

                spriteBatch.DrawString(_font, "Frozen", statusPosition, Color.OrangeRed);
            }
            */

            spriteBatch.End();
        }

        private void DrawStatus(SpriteBatch spriteBatch, Player player)
        {
            var playerIndex = (int) player.PlayerIndex;
            var diff = new Vector2(85, 0);
            var scale = ScreenManager.GetScalingFactor() * 0.1f;
            var powerUpPosition = PlayerInfoPositions[playerIndex] + new Vector2(10, 60);
            var n = 0;

            spriteBatch.DrawString(Font, "Player " + (playerIndex + 1), PlayerInfoPositions[playerIndex] * ScreenManager.GetScalingFactor(), CurrentPlayer == player ? Color.HotPink : player.Color, 0.0f,
                new Vector2(0, 0), ScreenManager.GetScalingFactor(), SpriteEffects.None, 0.0f);

            foreach (var icon in player.PowerUps.Select(powerUp => AssetManager.Instance.GetAsset<Texture2D>(powerUp.IconPath)))
            {
                spriteBatch.Draw(icon, powerUpPosition * ScreenManager.GetScalingFactor(), null, Color.White, 0f,
                    new Vector2(0, 0), scale, SpriteEffects.None, 0f);
                n++;
                powerUpPosition += diff;
            }

            for (var i = n; i < Player.MaxPowerUps; i++)
            {

                spriteBatch.Draw(EmptyPowerUp, powerUpPosition * ScreenManager.GetScalingFactor(), null, Color.White, 0f,
                    new Vector2(0, 0), scale, SpriteEffects.None, 0f);
                powerUpPosition += diff;
            }

                

            if (player.Effect != Effect.None)
            {
                Vector2 statusPosition = new Vector2(PlayerInfoPositions[playerIndex].X, PlayerInfoPositions[playerIndex].Y + 120);
                spriteBatch.DrawString(Font, player.Effect.ToString(), statusPosition * ScreenManager.GetScalingFactor(), Color.OrangeRed, 0.0f,
                    new Vector2(0, 0), ScreenManager.GetScalingFactor(), SpriteEffects.None, 0.0f);
            }
        }

        public void GameWon(PlayerIndex winningPlayer)
        {
            ScreenManager.AddScreen(new PauseMenuScreen("Player " + winningPlayer + " won!!"), null);
        }

        public void HandleDiceRollResult(int result)
        {
            //Move player
            var index = Positions.IndexOf(CurrentPlayer.BoardPosition) + result;
            if (index >= Positions.Count)
            {
                CurrentPlayer.BoardPosition = Positions.Last();
                GameWon(CurrentPlayer.PlayerIndex);
                return;
            }
            CurrentPlayer.BoardPosition = Positions[index];

            UpdateEffects();

            if (_miniGame)
            {
                _miniGame = false;
                StartMinigame();
            }
            else
            {
                if(CurrentPlayer.PowerUps.Count > 0)
                    ScreenManager.AddScreen(new ItemSelectScreen(), CurrentPlayer.PlayerIndex);
                else
                    ScreenManager.AddScreen(new DiceRoll(HandleDiceRollResult), CurrentPlayer.PlayerIndex);
            }

        }

        private void StartMinigame()
        {
            var random = new Random();
            var gameIndex = random.Next(MiniGames.Count);


            var minigameIntro = (MinigameIntro)Activator.CreateInstance(MiniGames[gameIndex], this);

            //var minigameIntro = (MinigameIntro)Activator.CreateInstance(_miniGames[0], this);

       

            ScreenManager.AddScreen(new Countdown(3, minigameIntro), null);
        }


        private List<Type> ViableMiniGames(int numberOfPlayers)
        {
            var miniGames = new List<Type>();
            var players = GetSupportedPlayers(numberOfPlayers);

            foreach (var type in Assembly.GetAssembly(typeof(MinigameIntro)).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(MinigameIntro))))
            {
                var fieldInfo = type.GetField("SupportedPlayers",
                    BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
                if (fieldInfo == null) continue;
                var sp = (SupportedPlayers)fieldInfo.GetValue(null);

                if ((sp & players) == players)
                    miniGames.Add(type);
            }

            return miniGames;
        }

        private SupportedPlayers GetSupportedPlayers(int numberOfPlayers)
        {
            switch (numberOfPlayers)
            {
                case 1:
                    return SupportedPlayers.Two;
                case 2:
                    return SupportedPlayers.Two;
                case 3:
                    return SupportedPlayers.Three;
                case 4:
                    return SupportedPlayers.Four;
                default:
                    return SupportedPlayers.None;
            }
        }


        public static List<PowerUp> PowerUps()
        {
            var powerUps = new List<PowerUp>();

            foreach (Type type in Assembly.GetAssembly(typeof(PowerUp)).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(PowerUp))))
            {
                powerUps.Add((PowerUp)Activator.CreateInstance(type));
            }

            return powerUps;
        }

        public void UpdateEffects()
        {
            if (CurrentPlayer.Effect != Effect.DoubleRoll)
            {
                NextPlayer();
            }
            else
            {
                CurrentPlayer.Effect = Effect.None;
            }

            if (CurrentPlayer.PlayerIndex == PlayerIndex.One && CurrentPlayer.Effect != Effect.DoubleRoll)
            {
                _miniGame = true;
            }

            if (CurrentPlayer.Effect == Effect.Freeze)
            {
                CurrentPlayer.Effect = Effect.None;
                NextPlayer();
            }
        }
    }
}
