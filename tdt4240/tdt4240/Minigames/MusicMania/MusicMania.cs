using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataTypes;
using Microsoft.Xna.Framework.Audio;
using tdt4240.Boards;

namespace tdt4240.Minigames.MusicMania
{
    class MusicMania : MiniGame
    {
        public new static SupportedPlayers SupportedPlayers = SupportedPlayers.All;

        public SpriteFont Font;
        private Texture2D _blankTexture;
        private Texture2D _arrow;
        private Dictionary<Player, MusicManiaPlayer> _players;
        private enum GameState { Loading, InGame, Winner }
        private GameState _gameState;
        private MusicManiaSong _musicData;
        private SoundEffect _music;
        private DateTime _timeMusicStarted;
        private TimeSpan _timeSinceMusicStarted;
        private float _colorMultiplier;
        private const float RelativeTargetLineThickness = 0.01f;
        public const float RelativeTargetOffset = 0.7f;
        private static readonly Random Rnd = new Random();

        private static readonly List<String> AllSongs = new List<String>
        {
            "stars-and-boxes"
        };

        public MusicMania(Board board)
            : base(board)
        {
            Title = "Music Mania";
            _gameState = GameState.Loading;
        }

        public override void Activate(bool instancePreserved)
        {
            base.Activate(instancePreserved);
            if (instancePreserved) return;

            Background = new Background("minigames/ColorMatch/background");
            ScreenManager.AddScreen(Background, null);

            Font = content.Load<SpriteFont>("fonts/dice");
            _blankTexture = content.Load<Texture2D>("blank");
            _arrow = content.Load<Texture2D>("minigames/MusicMania/arrow");

            InitSong(GetRandomSongKey());
        }

        private String GetRandomSongKey()
        {
            var i = Rnd.Next(AllSongs.Count);
            return AllSongs[i];
        }

        private void InitSong(String key)
        {
            _musicData = content.Load<MusicManiaSong>("minigames/MusicMania/" + key + "/data");
            _music = content.Load<SoundEffect>("minigames/MusicMania/"+ key + "/music");
            MusicPlayer.GetInstance().StartSong(_music, false);
            _timeMusicStarted = DateTime.Now + TimeSpan.FromSeconds(_musicData.StartsAt);

            _players = new Dictionary<Player, MusicManiaPlayer>();
            foreach (var player in PlayerManager.Instance.Players)
            {
                var arrows = _musicData.Arrows.Select(
                    arrow => new Arrow(this, player, _arrow, arrow.Direction, TimeSpan.FromSeconds(arrow.Time))
                    ).ToList();
                var musicManiaplayer = new MusicManiaPlayer(this, player, arrows);
                _players.Add(player, musicManiaplayer);
            }

            _gameState = GameState.InGame;
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            if (!_gameState.Equals(GameState.InGame)) return;
            foreach (Player player in PlayerManager.Instance.Players)
            {
                _players[player].HandleInput(input);
            }
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            _timeSinceMusicStarted = TimeSinceMusicStarted();
            if (_gameState == GameState.InGame)
            {
                if (_timeSinceMusicStarted > _music.Duration)
                {
                    _gameState = GameState.Winner;
                }
                foreach (var player in PlayerManager.Instance.Players)
                {
                    _players[player].Update();
                }

                _colorMultiplier = (float)(1 - (_musicData.Tempo * _timeSinceMusicStarted.TotalSeconds / 60) % 1);
            }
            else if (_gameState == GameState.Winner)
            {
                if (_timeSinceMusicStarted > _music.Duration + TimeSpan.FromSeconds(1))
                {
                    NotifyDone(GetWinner().PlayerIndex);
                }

                _colorMultiplier = 1;
            }

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            if (_gameState != GameState.InGame) return;

            var spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            for (var i = 0; i < PlayerManager.Instance.Players.Count; i++)
            {
                var player = PlayerManager.Instance.Players[i];

                // Draw player column
                var columnDestinationRect = new Rectangle(
                    (int)(i * 0.25 * ScreenManager.GetWidth()),
                    0,
                    (int)(0.25 * ScreenManager.GetWidth()),
                    ScreenManager.GetHeight()
                    );
                spriteBatch.Draw(_blankTexture, columnDestinationRect, Color.Multiply(player.Color, _colorMultiplier));

                // Draw arrows
                _players[player].Draw(spriteBatch);

                // Draw target area
                var upperBoundDestionationRect = new Rectangle(
                    (int)(i * 0.25 * ScreenManager.GetWidth()),
                    (int)((RelativeTargetOffset - RelativeTargetLineThickness) * ScreenManager.GetHeight()),
                    (int)(0.25 * ScreenManager.GetWidth()),
                    (int)(RelativeTargetLineThickness * ScreenManager.GetHeight())
                    );
                spriteBatch.Draw(_blankTexture, upperBoundDestionationRect, Color.White);
                
                var lowerBoundDestionationRect = new Rectangle(
                    (int)(i * 0.25 * ScreenManager.GetWidth()),
                    (int)((RelativeTargetOffset + Arrow.SizeFactor) * ScreenManager.GetHeight()),
                    (int)(0.25 * ScreenManager.GetWidth()),
                    (int)(RelativeTargetLineThickness * ScreenManager.GetHeight())
                    );
                spriteBatch.Draw(_blankTexture, lowerBoundDestionationRect, Color.White);
            }

            spriteBatch.End();
        }

        public TimeSpan TimeSinceMusicStarted()
        {
            return DateTime.Now - _timeMusicStarted;
        }

        private Player GetWinner()
        {
            var maxScore = 0;
            Player winner = null;
            foreach (var player in PlayerManager.Instance.Players)
            {
                var score = _players[player].Score;
                if (score > maxScore)
                {
                    maxScore = score;
                    winner = player;
                }
            }
            return winner;
        }

        public override void NotifyDone(PlayerIndex winningPlayerIndex)
        {
            base.NotifyDone(winningPlayerIndex);
        }

    }
}
