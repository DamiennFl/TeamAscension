using Ascension.Content.Controls;
using Ascension.Content.States;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascension.States
{
    internal class SecondState : FirstState
    {
        private readonly List<Components> components;

        private int screenHeight;

        private int screenWidth;

        public SecondState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, Vector2 currentBallPosition) : base(game, graphicsDevice, content)
        {
            //this.backGround = content.Load<Texture2D>("MidBossBackground");
            this.screenHeight = graphicsDevice.Viewport.Height;
            this.screenWidth = graphicsDevice.Viewport.Width;
            this.components = new List<Components>();
            this.ballPosition = currentBallPosition;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            this.graphicsDevice.Clear(Color.Thistle);

            this.BorderDraw(spriteBatch);

            spriteBatch.Draw(
                this.ballTexture,
                this.ballPosition,
                null,
                Color.White,
                0f,
                new Vector2(this.ballTexture.Width / 4, this.ballTexture.Height / 4),
                new Vector2(0.25F, 0.25F),
                SpriteEffects.None,
                0f);

            spriteBatch.End();
        }



        public override void Update(GameTime gameTime)
        {
            float updatedBallSpeed = this.ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds * 5;
            this.BallMovement(updatedBallSpeed);
            this.StayInBorder();
        }

    }
}
