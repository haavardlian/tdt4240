﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using menu.tdt4240;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace tdt4240.Boards
{
    class Board : GameScreen
    {
        private Texture2D _backgroundTexture;
        private Texture2D _pieceTexture;
        private ContentManager _content;
        private SpriteFont _font;
        private readonly Vector2[] _offsets = { new Vector2(-25, -25), new Vector2(25, -25), new Vector2(-25, 25), new Vector2(25, 25) };
        private Player _currentPlayer;
        private readonly List<BoardPosition> _positions = new List<BoardPosition>();

        private List<MiniGame> _miniGames;

        public void MiniGameDone(PlayerIndex winningPlayerIndex)
        {
            Console.WriteLine("Player has won:");
            Console.WriteLine(winningPlayerIndex);
            //TODO
            //Award winner
            //Remove minigame
        }

        public override void Activate(bool instancePreserved)
        {
            if (instancePreserved) return;
            if (_content == null)
                _content = new ContentManager(ScreenManager.Game.Services, "Content");

            _font = _content.Load<SpriteFont>("fonts/menufont");
            LoadContent();
            _currentPlayer = PlayerManager.Instance.Players[0];

            _positions.Add(new BoardPosition(new Vector2(78, 1004), PositionType.Start));
            
            PlayerManager.Instance.Players.ForEach(player => player.BoardPosition = _positions[0]);

            AddPositions();

            _miniGames = ViableMiniGames(PlayerManager.Instance.NumberOfPlayers, this);
        }

        private void AddPositions()
        {
            var factor = 2;
            _positions.Add(new BoardPosition(new Vector2(78 * (++factor), 1004), PositionType.Default));
            _positions.Add(new BoardPosition(new Vector2(78 * (++factor), 1004), PositionType.Default));
            _positions.Add(new BoardPosition(new Vector2(78 * (++factor), 1004), PositionType.Default));
            _positions.Add(new BoardPosition(new Vector2(78 * (++factor), 1004), PositionType.Default));
            _positions.Add(new BoardPosition(new Vector2(78 * (++factor), 1004), PositionType.Default));
            _positions.Add(new BoardPosition(new Vector2(78 * (++factor), 1004), PositionType.Default));
            _positions.Add(new BoardPosition(new Vector2(78 * (++factor), 1004), PositionType.Default));
            _positions.Add(new BoardPosition(new Vector2(78 * (++factor), 1004), PositionType.Default));
            _positions.Add(new BoardPosition(new Vector2(78 * (++factor), 1004), PositionType.Default));
            _positions.Add(new BoardPosition(new Vector2(78 * (++factor), 1004), PositionType.Default));
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            foreach (var player in PlayerManager.Instance.Players.Where(player => player.Input.IsButtonPressed(GameButtons.Back)))
            {
                ScreenManager.AddScreen(new PauseMenuScreen(), player.playerIndex);
                break;
            }

            var currentPlayerInput = _currentPlayer.Input;

            if (currentPlayerInput.IsButtonPressed(GameButtons.X))
            {
                ScreenManager.AddScreen(new DiceRoll(HandleDiceRollResult), _currentPlayer.playerIndex);
            }
        }

        public void LoadContent()
        {
            _backgroundTexture = _content.Load<Texture2D>("board/board");
            _pieceTexture = _content.Load<Texture2D>("board/piece");
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);
        }

        public override void Draw(GameTime gameTime)
        {
            var spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            spriteBatch.Draw(_backgroundTexture, Vector2.Zero, null, new Color(TransitionAlpha, TransitionAlpha, TransitionAlpha), 0f, Vector2.Zero, ScreenManager.GetScalingFactor(), SpriteEffects.None, 0f);

            _positions.ForEach(x => x.Draw(spriteBatch));

            foreach (var player in PlayerManager.Instance.Players)
            {
                var pos = player.BoardPosition.Position;
                pos += _offsets[(int)player.playerIndex];
                pos *= ScreenManager.GetScalingFactor();

                spriteBatch.Draw(_pieceTexture, pos, null, player.color, 0f, new Vector2(20, 10), ScreenManager.GetScalingFactor(), SpriteEffects.None, 0f);
            }
            
            
            spriteBatch.End();
        }

        public void HandleDiceRollResult(int result)
        {
            Console.WriteLine(_currentPlayer.playerIndex + " rolled a " + result);
            //TODO: Move player to new position

            var index = _positions.IndexOf(_currentPlayer.BoardPosition);

            if (index + result >= _positions.Count)
            {
                Console.WriteLine(_currentPlayer + "Is the winner!!!");
                return;
            }
             

            _currentPlayer.BoardPosition = _positions[index + result];

            var nextPlayerNumber = (int) _currentPlayer.playerIndex + 1;

            if (nextPlayerNumber >= PlayerManager.Instance.NumberOfPlayers)
                nextPlayerNumber = 0;

            _currentPlayer = PlayerManager.Instance.GetPlayer((PlayerIndex)nextPlayerNumber);
        }

        private void StartMinigame()
        {
            Random random = new Random();
            int gameIndex = random.Next(_miniGames.Count);

            ScreenManager.AddScreen(_miniGames[gameIndex], null);
        }


        private List<MiniGame> ViableMiniGames(int numberOfPlayers, Board board)
        {
            List<MiniGame> miniGames = new List<MiniGame>();
            SupportedPlayers players = GetSupportedPlayers(numberOfPlayers);

            foreach (Type type in Assembly.GetAssembly(typeof(MiniGame)).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(MiniGame))))
            {
                SupportedPlayers sp =
                (SupportedPlayers)type.GetField("SupportedPlayers",
                   BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).GetValue(null);

                if (sp.HasFlag(players))
                    miniGames.Add((MiniGame)Activator.CreateInstance(type, board));
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

    }
}
