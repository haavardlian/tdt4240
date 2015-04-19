using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using tdt4240.Menu;
using tdt4240.Minigames.MinigameDemo;

namespace tdt4240.Boards
{
    public class Board : GameScreen
    {
        private Texture2D _backgroundTexture;
        private Texture2D _pieceTexture;
        private Texture2D _tileTexture;
        private Texture2D _playerBackground;
        private Texture2D _emptyPowerUp;
        private Texture2D _downTexture;
        private Texture2D _starTexture;
        private ContentManager _content;
        private SpriteFont _font;
        private readonly Vector2[] _offsets = { new Vector2(-20, -20), new Vector2(20, -20), new Vector2(-20, 20), new Vector2(20, 20) };
        private Player _currentPlayer;
        private readonly List<BoardPosition> _positions = new List<BoardPosition>();

        private List<Vector2> _playerBackgroundPositions = new List<Vector2>();
        private List<Vector2> _playerInfoPositions = new List<Vector2>();

        private List<Type> _miniGames;
        private List<PowerUp> _powerUps;
        private PowerUp _currentPowerUp = null;
        private PlayerIndex _currentWinner;

        public Player CurrentPlayer
        {
            get
            {
                return _currentPlayer;
            }
        }

        public void NextPlayer()
        {
            var nextIndex = ((int) _currentPlayer.PlayerIndex) + 1;
            if (nextIndex >= PlayerManager.Instance.NumberOfPlayers)
                nextIndex = 0;
            _currentPlayer = PlayerManager.Instance.Players[nextIndex];
        }

        public void MiniGameDone(PlayerIndex winningPlayerIndex)
        {
            _currentWinner = winningPlayerIndex;
            var winner = PlayerManager.Instance.GetPlayer(_currentWinner);

            ScreenManager.AddScreen(new MinigameWinnerScreen(winner), null);
         
        }

        public override void Activate(bool instancePreserved)
        {
  
            if (instancePreserved) return;
            if (_content == null)
                _content = new ContentManager(ScreenManager.Game.Services, "Content");

            
            LoadContent();
            _currentPlayer = PlayerManager.Instance.Players[0];

            _positions.Add(new BoardPosition(new Vector2(370, 1000), PositionType.Start, _tileTexture));
            
            PlayerManager.Instance.Players.ForEach(player => player.BoardPosition = _positions[0]);

            for (int i = 0; i < 4; i++)
            {
                _playerBackgroundPositions.Add(new Vector2());
                _playerInfoPositions.Add(new Vector2());
            }

            AddPositions();

            _positions.Last().NavigateTo += OnFinish;
            _positions.Last().Icon = _starTexture;

            _miniGames = ViableMiniGames(PlayerManager.Instance.NumberOfPlayers);
            _powerUps = PowerUps();

            AssetManager.Instance.AddAsset<Texture2D>("powerups/empty");
            AssetManager.Instance.AddAsset<Texture2D>("powerups/freeze");
            AssetManager.Instance.AddAsset<Texture2D>("powerups/double_dice");
            AssetManager.Instance.AddAsset<Texture2D>("powerups/unknown");

            ScreenManager.Board = this;

            float x = ScreenManager.MaxWidth - _font.MeasureString("Player X").X - 5;
            float y = ScreenManager.MaxHeight - 180;

            _playerBackgroundPositions[0] = new Vector2(5, 0);
            _playerBackgroundPositions[1] = new Vector2(ScreenManager.MaxWidth, 0);
            _playerBackgroundPositions[2] = new Vector2(5, ScreenManager.MaxHeight);
            _playerBackgroundPositions[3] = new Vector2(ScreenManager.MaxWidth, ScreenManager.MaxHeight);

            _playerInfoPositions[0] = new Vector2(0, 0);
            _playerInfoPositions[1] = new Vector2(x, 0);
            _playerInfoPositions[2] = new Vector2(0, y);
            _playerInfoPositions[3] = new Vector2(x, y);

            if (PlayerManager.Instance.NumberOfPlayers >= 3)
            {
                PlayerManager.Instance.Players[2].Effect = Effect.DoubleRoll;
            }

        }

        public override void Added()
        {
            ScreenManager.AddScreen(new DiceRoll(HandleDiceRollResult), _currentPlayer.PlayerIndex);
        }

        private void OnFinish(object sender, EventArgs eventArgs)
        {
            var player = eventArgs as Player;

            Console.WriteLine(player + " is the winner!!!");
        }

        private void NavigateToPowerDown(object sender, EventArgs args)
        {
            var player = args as Player;

            var index = _positions.IndexOf(player.BoardPosition);

            player.BoardPosition = _positions[index - 5];
        }

        private void AddPositions()
        {
            var factor = -1;
            var nextX = new Vector2(96, 0);
            var prevX = new Vector2(-96, 0);
            var nextY = new Vector2(0, -96);
            var prevY = new Vector2(0, 96);
            var pos = new Vector2(466, 1000);

            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextX;
            _positions.Last().Icon = _downTexture;
            _positions.Last().NavigateTo += NavigateToPowerDown;

            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextX;
            _positions.Last().Icon = _downTexture;
            _positions.Last().NavigateTo += NavigateToPowerDown;

            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextX;
            _positions.Last().Icon = _downTexture;
            _positions.Last().NavigateTo += NavigateToPowerDown;

            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextY;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextY;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += prevX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += prevX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += prevX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += prevX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += prevX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += prevX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += prevX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += prevX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += prevX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += prevX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextY;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextY;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextY;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextY;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextY;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += prevX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += prevX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += prevX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += prevX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += prevX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += prevX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += prevX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextY;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextY;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture));
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            foreach (var player in PlayerManager.Instance.Players.Where(player => player.Input.IsButtonPressed(GameButtons.Back)))
            {
                ScreenManager.AddScreen(new PauseMenuScreen(), null);
                break;
            }
        }

        public void LoadContent()
        {
            //_backgroundTexture = _content.Load<Texture2D>("board/board");
            _pieceTexture = AssetManager.Instance.AddAsset<Texture2D>("board/piece2");
            _font = AssetManager.Instance.AddAsset<SpriteFont>("fonts/smallfont");
            _tileTexture = AssetManager.Instance.AddAsset<Texture2D>("board/tile");
            _playerBackground = AssetManager.Instance.AddAsset<Texture2D>("black_circle");
            _emptyPowerUp = AssetManager.Instance.AddAsset<Texture2D>("powerups/empty");
            _downTexture = AssetManager.Instance.AddAsset<Texture2D>("powers/down");
            _starTexture = AssetManager.Instance.AddAsset<Texture2D>("powers/star");
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
            _positions.ForEach(x => x.Draw(spriteBatch));


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
                    pos += _offsets[index];
                pos *= ScreenManager.GetScalingFactor();

                DrawStatus(spriteBatch, player);
                spriteBatch.Draw(_pieceTexture, pos, null, player.Color, 0f, new Vector2(32, 45) * ScreenManager.GetScalingFactor(), ScreenManager.GetScalingFactor(), SpriteEffects.None, 0f);
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
            int playerIndex = (int) player.PlayerIndex;

   //         spriteBatch.Draw(_playerBackground, _playerBackgroundPositions[playerIndex] * ScreenManager.GetScalingFactor(), null, Color.White, 0f,
     //           new Vector2(_playerBackground.Width/2f, _playerBackground.Height/2f), ScreenManager.GetScalingFactor(), SpriteEffects.None, 0f);

            spriteBatch.DrawString(_font, "Player " + (playerIndex + 1), _playerInfoPositions[playerIndex] * ScreenManager.GetScalingFactor(), _currentPlayer == player ? Color.HotPink : player.Color, 0.0f,
                new Vector2(0, 0), ScreenManager.GetScalingFactor(), SpriteEffects.None, 0.0f);


            var diff = 50;
            var scale = ScreenManager.GetScalingFactor() * 0.1f;

            var powerUpPosition = new Vector2(_playerInfoPositions[playerIndex].X + 40, _playerInfoPositions[playerIndex].Y + 60);
            var n = 0;

            foreach (var powerUp in player.PowerUps)
            {
                var icon = AssetManager.Instance.GetAsset<Texture2D>(powerUp.IconPath);
                spriteBatch.Draw(icon, powerUpPosition * ScreenManager.GetScalingFactor(), null, Color.White, 0f,
                    new Vector2(0, 0), scale, SpriteEffects.None, 0f);
                n++;
                powerUpPosition = new Vector2(powerUpPosition.X + diff * 1.5f, powerUpPosition.Y);
            }

            for (var i = n; i < Player.MaxPowerUps; i++)
            {

                spriteBatch.Draw(_emptyPowerUp, powerUpPosition * ScreenManager.GetScalingFactor(), null, Color.White, 0f,
                    new Vector2(0, 0), scale, SpriteEffects.None, 0f);
                powerUpPosition = new Vector2(powerUpPosition.X + diff * 1.5f, powerUpPosition.Y);
            }

                

            if (player.Effect != Effect.None)
            {
                Vector2 statusPosition = new Vector2(_playerInfoPositions[playerIndex].X, _playerInfoPositions[playerIndex].Y + 120);
                spriteBatch.DrawString(_font, player.Effect.ToString(), statusPosition * ScreenManager.GetScalingFactor(), Color.OrangeRed, 0.0f,
                    new Vector2(0, 0), ScreenManager.GetScalingFactor(), SpriteEffects.None, 0.0f);
            }
        }

        public void HandleDiceRollResult(int result)
        {
            Console.WriteLine(_currentPlayer.PlayerIndex + " rolled a " + result);
            var index = _positions.IndexOf(_currentPlayer.BoardPosition) + result;
            bool doubleRoll = false;
            if (index >= _positions.Count)
            {
                _currentPlayer.BoardPosition = _positions.Last();
                return;
            }
             

            _currentPlayer.BoardPosition = _positions[index];


            var nextPlayerNumber = (int) _currentPlayer.PlayerIndex;

            if (_currentPlayer.Effect != Effect.DoubleRoll)
            {
                nextPlayerNumber++;
            }
            else
            {
                doubleRoll = true;
                _currentPlayer.Effect = Effect.None;
            }
            if (nextPlayerNumber >= PlayerManager.Instance.NumberOfPlayers)
                nextPlayerNumber = 0;
            _currentPlayer = PlayerManager.Instance.GetPlayer((PlayerIndex)nextPlayerNumber);

            if (_currentPlayer.Effect == Effect.Freeze)
            {
                _currentPlayer.Effect = Effect.None;
                nextPlayerNumber++;
                if (nextPlayerNumber >= PlayerManager.Instance.NumberOfPlayers)
                    nextPlayerNumber = 0;
                _currentPlayer = PlayerManager.Instance.GetPlayer((PlayerIndex)nextPlayerNumber);
            }

            if (nextPlayerNumber == 0 && !doubleRoll)
            {
                StartMinigame();
            }
            else
            {
                if(_currentPlayer.PowerUps.Count > 0)
                    ScreenManager.AddScreen(new ItemSelectScreen(), _currentPlayer.PlayerIndex);
                else
                    ScreenManager.AddScreen(new DiceRoll(HandleDiceRollResult), _currentPlayer.PlayerIndex);
            }

        }

        private void StartMinigame()
        {
            var random = new Random();
            var gameIndex = random.Next(_miniGames.Count);


            var minigameIntro = (MinigameIntro)Activator.CreateInstance(_miniGames[gameIndex], this);

            //var minigameIntro = (MinigameIntro)Activator.CreateInstance(_miniGames[0], this);

       

            ScreenManager.AddScreen(new Countdown(3, minigameIntro), null);
        }


        private List<Type> ViableMiniGames(int numberOfPlayers)
        {
            var miniGames = new List<Type>();
            var players = GetSupportedPlayers(numberOfPlayers);

            foreach (Type type in Assembly.GetAssembly(typeof(MinigameIntro)).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(MinigameIntro))))
            {
                SupportedPlayers sp = (SupportedPlayers)type.GetField("SupportedPlayers",
                   BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).GetValue(null);

                if (sp.HasFlag(players))
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
            if (CurrentPlayer.Effect == Effect.Freeze)
            {
                CurrentPlayer.Effect = Effect.None;
                NextPlayer();
            }      
        }
    }
}
