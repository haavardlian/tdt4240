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
        private IRoller rollee;
        private Player roller;
        private string text = "";
        private int time = 400;
        private int elapsed = 0;
        private int step = 100;
        private int wait = 0;
        private int max;
        int n = 0;

        private bool pressed = false;

        public DiceRoll(int max, IRoller rollee,  Player roller) : base()
        {
            
            this.target = rnd.Next(1, max);
            this.rollee = rollee;
            this.roller = roller;
            this.max = max;
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            foreach (Player player in PlayerManager.Instance.Players)
            {
                if(ControllingPlayer.Value == player.playerIndex && player.Input.IsButtonPressed(GameButtons.A))
                {
                    pressed = true;
                }
            }
        }

        public override void Activate(bool instancePreserved)
        {
            base.Activate(instancePreserved);

            if (!instancePreserved)
            {
                if (content == null)
                    content = new ContentManager(ScreenManager.Game.Services, "Content");
                font = content.Load<SpriteFont>("fonts/dice");
            }
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            elapsed += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if(pressed)
            {
                step++;
            }

            if(step >= time)
            {
                text = "" + n;
                wait += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

                if(wait > 1000)
                {
                    rollee.ResultHandler(roller, n);
                    ScreenManager.RemoveScreen(this);
                }

            }
            else if (elapsed > step)
            {
                n++;
                if (n >= 7) n = 1;
                text = "" + n;
                elapsed = 0;
            }
                
            

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(GameTime gameTime)
        {

            GameScreen[] screens = ScreenManager.GetScreens();
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();
            spriteBatch.DrawString(font, text, position, Color.HotPink);
            spriteBatch.End();
        }
    }
}
