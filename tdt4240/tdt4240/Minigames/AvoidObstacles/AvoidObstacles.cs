using System;
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
        private const double ObstacleSpawnRate = 0.05;

        private const int ScreenPadding = 10;
        private Vector2[] _corners;

        private readonly int _numberOfPlayers;
        private readonly List<PlayerObject> _playerObjects;
        private readonly List<Obstacle> _obstacles;
        private readonly ObstacleFactory _obstacleFactory;
        public new static SupportedPlayers SupportedPlayers = SupportedPlayers.All;
        private readonly Random _random;

        public AvoidObstacles(Board board) : base(board)
        {
            _obstacleFactory = new ObstacleFactory();
            _numberOfPlayers = PlayerManager.Instance.NumberOfPlayers;
            _random = new Random();
            _playerObjects = new List<PlayerObject>();
            _obstacles = new List<Obstacle>();

            _corners = new[]{new Vector2(ScreenPadding, ScreenPadding),
            new Vector2(ScreenManager.MaxWidth - ScreenPadding, ScreenPadding),
            new Vector2(ScreenPadding, ScreenManager.MaxHeight - ScreenPadding),
            new Vector2(ScreenManager.MaxWidth - ScreenPadding, ScreenManager.MaxHeight - ScreenPadding)};

        }
        public override void Activate(bool instancePreserved)
        {
            base.Activate(instancePreserved);



            if (!instancePreserved)
            {
                _obstacleFactory.SetTexture(content.Load<Texture2D>("minigames/BirdHunt/Bird"));
                var playerTexture = content.Load<Texture2D>("minigames/AvoidObstacles/Helicopter");
                Background = new Background("background");
                ScreenManager.AddScreen(Background, null);


                for (var i = 0; i < _numberOfPlayers; i++)
                {
                    var startPosition = (ScreenManager.MaxHeight-(playerTexture.Height*2))*(i/_numberOfPlayers);
                    _playerObjects.Add(new PlayerObject(PlayerManager.Instance.Players[i], playerTexture, new Vector2(startPosition+playerTexture.Height,playerTexture.Width)));
                    _playerObjects[i].Corner = _corners[i];
                }
            }

        }

        /// <summary>
        /// Allows the screen to run logic, such as updating the transition position.
        /// Unlike HandleInput, this method is called regardless of whether the screen
        /// is active, hidden, or in the middle of a transition.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {

            foreach (var obstacle in _obstacles)
            {
                obstacle.UpdateSpeed();
                obstacle.Position += obstacle.Speed;
            }

            foreach (var playerObject in _playerObjects)
            {
                playerObject.Score += Intersects(playerObject, _obstacles);
                PlayerCrash(playerObject, _playerObjects);
                playerObject.Position += new Vector2(playerObject.KnockBack,0);
            }
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
                if(player==otherPlayer)
                    continue;

                if (player.Bounds.Intersects(otherPlayer.Bounds))
                {
                    var knockback = Math.Sign(player.Position.Y - otherPlayer.Position.Y);
                    player.KnockBack = knockback*PlayerObject.KnockbackValue;
                    otherPlayer.KnockBack = knockback*PlayerObject.KnockbackValue*-1;
                    player.Score++;
                    otherPlayer.Score++;
                }


            }
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {

            foreach (var playerObject in _playerObjects)
            {
                playerObject.Position += playerObject.Player.Input.GetThumbstickVector()*3;
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
            spriteBatch.DrawString(ScreenManager.Font, playerObject.Score.ToString(), playerObject.Corner * ScreenManager.GetScalingFactor(), playerObject.Color);

            spriteBatch.Draw(playerObject.Texture, playerObject.Position * ScreenManager.GetScalingFactor(), null, playerObject.Color, 0f, new Vector2(0, 0), ScreenManager.GetScalingFactor(), SpriteEffects.None, 0f);
            }



        spriteBatch.End();
    }
    public override void NotifyDone(PlayerIndex winnerIndex)
    {
        base.NotifyDone(winnerIndex);
    }
    }
}
