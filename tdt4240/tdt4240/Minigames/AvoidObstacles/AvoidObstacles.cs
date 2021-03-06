﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tdt4240.Boards;
using tdt4240.Minigames.BirdHunt;

namespace tdt4240.Minigames.AvoidObstacles
{
    class AvoidObstacles : MiniGame
    {
        private const double ObstacleSpawnRate = 0.035;

        private const int ScreenPadding = 10;

        private readonly int _numberOfPlayers;
        private readonly List<PlayerObject> _playerObjects;
        private readonly List<Obstacle> _obstacles;
        private readonly ObstacleFactory _obstacleFactory;
        public new static SupportedPlayers SupportedPlayers = SupportedPlayers.All;
        private readonly Random _random;
        private Boolean _activated;

        public AvoidObstacles(Board board)
            : base(board)
        {
            Title = "Avoid the birds";
            _obstacleFactory = new ObstacleFactory();
            _numberOfPlayers = PlayerManager.Instance.NumberOfPlayers;
            _random = new Random();
            _playerObjects = new List<PlayerObject>();
            _obstacles = new List<Obstacle>();
            _activated = false;
        }
        public override void Activate(bool instancePreserved)
        {
            base.Activate(instancePreserved);


            if (!instancePreserved)
            {
                MusicPlayer.GetInstance().StartLoopingSong("6");
                _obstacleFactory.SetTexture(content.Load<Texture2D>("minigames/BirdHunt/Bird"));
                var playerTexture = content.Load<Texture2D>("minigames/AvoidObstacles/Helicopter");
                Background = new Background("background");
                ScreenManager.AddScreen(Background, null);


                for (var i = 0; i < _numberOfPlayers; i++)
                {
                    var startPosition = (ScreenManager.MaxHeight - (playerTexture.Height * 2)) * ((float)i / _numberOfPlayers);
                    _playerObjects.Add(
                        new PlayerObject(
                            PlayerManager.Instance.Players[i],
                            playerTexture,
                            new Vector2(playerTexture.Width, startPosition + playerTexture.Height)
                        )
                    );
                }
            }

            _activated = true;
        }

        /// <summary>
        /// Allows the screen to run logic, such as updating the transition position.
        /// Unlike HandleInput, this method is called regardless of whether the screen
        /// is active, hidden, or in the middle of a transition.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            if (!_activated) return;

            foreach (var obstacle in _obstacles)
            {
                obstacle.UpdateSpeed();
                obstacle.Position += obstacle.Speed;
            }

            List<PlayerObject> deadPlayer = new List<PlayerObject>();
            foreach (var playerObject in _playerObjects)
            {
                if (playerObject.Score >= 10)
                {
                    deadPlayer.Add(playerObject);
                    continue;
                }

                playerObject.Position += playerObject.Speed;
                playerObject.Score += Intersects(playerObject, _obstacles);
                //PlayerCrash(playerObject, _playerObjects);
                playerObject.Position += new Vector2(playerObject.KnockBack, 0);
            }
            deadPlayer.ForEach(player => _playerObjects.Remove(player));
            if (_playerObjects.Count == 1 && _numberOfPlayers > 1)
                NotifyDone(_playerObjects[0].Player.PlayerIndex);
            if (_playerObjects.Count == 0)
                NotifyDone(deadPlayer[0].Player.PlayerIndex);

            if (_random.NextDouble() < ObstacleSpawnRate)
            {
                _obstacles.Add(_obstacleFactory.GenerateObstacle());
            }
            //if the obstacles reaches the left side of the screen it will be removed
            _obstacles.RemoveAll(obstacle => obstacle.Position.X < -obstacle.Texture.Width);
        }

        private int Intersects(PlayerObject playerObject, List<Obstacle> obstacles)
        {
            var crashes = 0;

            foreach (var obstacle in obstacles)
            {
                if (obstacle.Bounds.Intersects(playerObject.Bounds) && !obstacle.CrashedPlayers.Contains(playerObject))
                {
                    crashes++;
                    obstacle.CrashedPlayers.Add(playerObject);
                }
            }
            return crashes;
        }

        private void PlayerCrash(PlayerObject player, List<PlayerObject> playerObjects)
        {
            foreach (var otherPlayer in playerObjects)
            {
                if (player == otherPlayer)
                    continue;

                if (player.Bounds.Intersects(otherPlayer.Bounds))
                {
                    var knockback = Math.Sign(player.Position.Y - otherPlayer.Position.Y);
                    player.KnockBack = knockback * PlayerObject.KnockbackValue;
                    otherPlayer.KnockBack = knockback * PlayerObject.KnockbackValue * -1;
                    player.Score++;
                    otherPlayer.Score++;
                }


            }
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {

            foreach (var playerObject in _playerObjects)
            {
                playerObject.Speed += playerObject.Player.Input.GetThumbstickVector();
            }
        }
        /// <summary>
        /// This is called when the screen should draw itself.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            foreach (var obstacle in _obstacles)
            {
                spriteBatch.Draw(obstacle.Texture, obstacle.Position * ScreenManager.GetScalingFactor(), null, Color.White, 0f, new Vector2(0, 0), ScreenManager.GetScalingFactor(), SpriteEffects.None, 0f);
            }

            foreach (var playerObject in _playerObjects)
            {
                // Draw score strings
                var score = playerObject.Score.ToString();
                var scoreStringSize = ScreenManager.Font.MeasureString(score);
                var desiredScoreHeight = 0.1 * ScreenManager.GetHeight();
                var scoreStringScale = (float)desiredScoreHeight / scoreStringSize.Y;
                spriteBatch.DrawString(
                    ScreenManager.Font,
                    score,
                    new Vector2(
                        (int)(((int)playerObject.Player.PlayerIndex + 0.5) * 0.25 * ScreenManager.GetWidth() - 0.5 * scoreStringSize.X * scoreStringScale),
                        (int)(0.02 * ScreenManager.GetHeight())
                    ),
                    playerObject.Color,
                    0,
                    new Vector2(0, 0),
                    scoreStringScale,
                    SpriteEffects.None,
                    0
                );

                // Draw helicopter
                spriteBatch.Draw(
                    playerObject.Texture,
                    playerObject.Position * ScreenManager.GetScalingFactor(),
                    null,
                    playerObject.Color,
                    0f,
                    new Vector2(0, 0),
                    ScreenManager.GetScalingFactor(),
                    SpriteEffects.None,
                    0f
                );
            }



            spriteBatch.End();
        }
    }
}
