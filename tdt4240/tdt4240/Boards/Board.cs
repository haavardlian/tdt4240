using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using tdt4240.Menu;

namespace tdt4240.Boards
{
    class Board : GameScreen
    {
        private Texture2D _backgroundTexture;
        private Texture2D _pieceTexture;
        private Texture2D _tileTexture;
        private Texture2D _downTexture;
        private Texture2D _starTexture;
        private ContentManager _content;
        private SpriteFont _font;
        private readonly Vector2[] _offsets = { new Vector2(-20, -20), new Vector2(20, -20), new Vector2(-20, 20), new Vector2(20, 20) };
        private Player _currentPlayer;
        private readonly List<BoardPosition> _positions = new List<BoardPosition>();

        private List<Type> _miniGames;
        private List<Type> _powerUps;

        public void MiniGameDone(PlayerIndex winningPlayerIndex)
        {
            var powerUp = GetRandomPowerUp();
            var winner = PlayerManager.Instance.GetPlayer(winningPlayerIndex);
            winner.AddPowerUp(powerUp);

            ScreenManager.AddScreen(new MinigameWinnerScreen(winner, powerUp), null);
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

            AddPositions();

            _positions.Last().NavigateTo += OnFinish;
            _positions.Last().Icon = _starTexture;

            _miniGames = ViableMiniGames(PlayerManager.Instance.NumberOfPlayers);
            _powerUps = PowerUps();
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
                ScreenManager.AddScreen(new PauseMenuScreen(), player.PlayerIndex);
                break;
            }

            var currentPlayerInput = _currentPlayer.Input;

            if (currentPlayerInput.IsButtonPressed(GameButtons.X))
            {
                ScreenManager.AddScreen(new DiceRoll(HandleDiceRollResult), _currentPlayer.PlayerIndex);
            }

            ////Tempcode for testing minigame functionality

            //foreach (Player player in PlayerManager.Instance.Players)
            //{
            //    if (player.Input.IsButtonPressed(GameButtons.A))
            //    {
            //        StartMinigame();
            //        //ScreenManager.AddScreen(new MinigameDemoIntro(), null);
            //    }
            //}
        }

        public void LoadContent()
        {
            //_backgroundTexture = _content.Load<Texture2D>("board/board");
            _pieceTexture = _content.Load<Texture2D>("board/piece2");
            _font = _content.Load<SpriteFont>("fonts/menufont");
            _tileTexture = _content.Load<Texture2D>("board/tile");
            _downTexture = _content.Load<Texture2D>("powers/down");
            _starTexture = _content.Load<Texture2D>("powers/star");
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
            ScreenManager.GraphicsDevice.Clear(Color.CornflowerBlue);
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

                spriteBatch.Draw(_pieceTexture, pos, null, player.Color, 0f, new Vector2(32, 45) * ScreenManager.GetScalingFactor(), ScreenManager.GetScalingFactor(), SpriteEffects.None, 0f);
            }
            
            
            spriteBatch.End();
        }

        public void HandleDiceRollResult(int result)
        {
            Console.WriteLine(_currentPlayer.PlayerIndex + " rolled a " + result);
            var index = _positions.IndexOf(_currentPlayer.BoardPosition) + result;

            if (index >= _positions.Count)
            {
                _currentPlayer.BoardPosition = _positions.Last();
                return;
            }
             

            _currentPlayer.BoardPosition = _positions[index];

            var nextPlayerNumber = (int) _currentPlayer.PlayerIndex + 1;

            if (nextPlayerNumber >= PlayerManager.Instance.NumberOfPlayers)
            {
                nextPlayerNumber = 0;
            }
                

            _currentPlayer = PlayerManager.Instance.GetPlayer((PlayerIndex)nextPlayerNumber);

            if (nextPlayerNumber == 0)
            {
                StartMinigame();
            }

        }

        private void StartMinigame()
        {
            var random = new Random();
            var gameIndex = random.Next(_miniGames.Count);

            var minigameIntro = (MinigameIntro)Activator.CreateInstance(_miniGames[gameIndex], this);

            //MiniGame minigame = (MiniGame)Activator.CreateInstance(_miniGames[1], this);

            ScreenManager.AddScreen(minigameIntro, null);
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


        private List<Type> PowerUps()
        {
            var powerUps = new List<Type>();

            foreach (Type type in Assembly.GetAssembly(typeof(PowerUp)).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(PowerUp))))
            {
                    powerUps.Add(type);
            }

            return powerUps;
        }

        private PowerUp GetRandomPowerUp()
        {

            var random = new Random();
            var powerUpIndex = random.Next(_powerUps.Count);

            return (PowerUp)Activator.CreateInstance(_powerUps[powerUpIndex]);
        }
    }
}
