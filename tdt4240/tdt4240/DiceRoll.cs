using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tdt4240
{
    class DiceRoll : GameScreen
    {
        private SpriteFont font;
        private Vector2 position = new Vector2(350, 120);
        private ContentManager content;
        private int target;
        private Random rnd = new Random();
        private IDiceRoller rollee;
        private Player roller;
        private string text = "";
        private int time = 2000;
        private int elapsed = 0;
        private int total = 0;
        private int step = 100;
        private int wait = 0;

        public DiceRoll(int min, int max, IDiceRoller rollee,  Player roller) : base()
        {
            
            this.target = rnd.Next(min, max);
            this.rollee = rollee;
            this.roller = roller;
        }

        public override void Activate(bool instancePreserved)
        {
            base.Activate(instancePreserved);

            if (!instancePreserved)
            {
                if (content == null)
                    content = new ContentManager(ScreenManager.Game.Services, "Content");
                font = content.Load<SpriteFont>("dice");
            }
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            elapsed += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsed > step)
            {
                text = "" + rnd.Next(0, 100);
                total += elapsed;
                elapsed = 0;
            }

            if (total > time)
            {
                text = "" + target;
                wait += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            if (wait > time)
            {
                rollee.DiceResultHandler(roller, target);
                ScreenManager.RemoveScreen(this);
            }
                

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            spriteBatch.DrawString(font, text, position, Color.HotPink);

            spriteBatch.End();
        }
    }
}
