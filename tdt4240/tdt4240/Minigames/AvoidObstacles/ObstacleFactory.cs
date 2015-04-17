using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tdt4240.Minigames.AvoidObstacles
{
    class ObstacleFactory
    {
        private Texture2D _obstacleTexture;
        private readonly Random _random;

        public ObstacleFactory()
        {
            _random = new Random();
        }

        public void SetTexture(Texture2D texture)
        {
            _obstacleTexture = texture;
        }


        public Obstacle GenerateObstacle()
        {
            return new Obstacle(_obstacleTexture, new Vector2(ScreenManager.MaxWidth + _obstacleTexture.Width, _random.Next(_obstacleTexture.Height, ScreenManager.MaxHeight-_obstacleTexture.Height)));
        }
    }
}
