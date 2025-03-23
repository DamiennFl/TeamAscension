using Ascension.Content.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascension.States
{
    internal class GameOverState : State
    {

        private SpriteFont font;

        public GameOverState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            this.font = this.content.Load<SpriteFont>("Fonts/Font");
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            spriteBatch.DrawString(this.font, "Game Over", new Vector2(100, 100), Color.White);

            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
      
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
