// <copyright file="GameState.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using Ascension.States;
using Ascension.Enemies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascension.Content.States
{

    /// <summary>
    /// The game state.
    /// </summary>
    public class FirstState : State
    {
        /// <summary>
        /// Border rectangle.
        /// </summary>
        protected Rectangle borderRect = new Rectangle(40, 40, 460, 720);

        /// <summary>
        /// Our Border width.
        /// </summary>
        protected int borderWidth = 4;

        /// <summary>
        /// Border color.
        /// </summary>
        protected Color borderColor = Color.Black;

        private float midBossTime = 0f;

        protected Texture2D borderTexture;

        private Texture2D backGround;

        protected Player player;

        private List<Enemy> enemies = new List<Enemy>();

        public FirstState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            this.backGround = content.Load<Texture2D>("Backgrounds/AscensionTitle");
            this.borderTexture = new Texture2D(graphicsDevice, 1, 1);
            this.borderTexture.SetData(new[] { Color.AliceBlue });
            this.player = new Player(content.Load<Texture2D>("ball"), new Vector2(graphicsDevice.Viewport.Width / 4, graphicsDevice.Viewport.Height / 2), 100f);

            BasicEnemyFactory enemyFactory = new BasicEnemyFactory(content, graphicsDevice);
            this.enemies.Add(enemyFactory.CreateEnemy(new Vector2(40, 40), "EnemyA"));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            this.graphicsDevice.Clear(Color.White);

            this.BorderDraw(spriteBatch);

            // Adjust the background rectangle to fit nicely within the borderRect and borderWidth
            int adjustedX = this.borderRect.X + this.borderWidth;
            int adjustedY = this.borderRect.Y + this.borderWidth;
            int adjustedWidth = this.borderRect.Width - (2 * this.borderWidth);
            int adjustedHeight = this.borderRect.Height - (2 * this.borderWidth);

            spriteBatch.Draw(this.backGround, new Rectangle(adjustedX, adjustedY, adjustedWidth, adjustedHeight), Color.White);
            this.player.Draw(spriteBatch);

            spriteBatch.End();
        }

        /// <summary>
        /// Draws the border for us.
        /// </summary>
        /// <param name="spriteBatch">our sprite.</param>
        public void BorderDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.borderTexture, new Rectangle(this.borderRect.X, this.borderRect.Y, this.borderRect.Width, this.borderWidth), this.borderColor);
            spriteBatch.Draw(this.borderTexture, new Rectangle(this.borderRect.X, this.borderRect.Y + this.borderRect.Height - this.borderWidth, this.borderRect.Width, this.borderWidth), this.borderColor);
            spriteBatch.Draw(this.borderTexture, new Rectangle(this.borderRect.X, this.borderRect.Y, this.borderWidth, this.borderRect.Height), this.borderColor);
            spriteBatch.Draw(this.borderTexture, new Rectangle(this.borderRect.X + this.borderRect.Width - this.borderWidth, this.borderRect.Y, this.borderWidth, this.borderRect.Height), this.borderColor);
        }

        public override void PostUpdate(GameTime gameTime)
        {
           // throw new NotImplementedException();
        }

        /// <summary>
        /// Our update method.
        /// </summary>
        /// <param name="gameTime">.</param>
        public override void Update(GameTime gameTime)
        {
            float updatedPlayerSpeed = this.player.playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds * 5;

            this.midBossTime += (float)gameTime.ElapsedGameTime.TotalSeconds; // when to change to midboss state

            if (this.IsBossTime(3f))
            {
                this.game.ChangeState(new SecondState(this.game, this.graphicsDevice, this.content, this.player));
            }

            this.player.PlayerMovement(updatedPlayerSpeed);
            this.player.StayInBorder(this.borderRect, this.borderWidth);

        }

        /// <summary>
        /// This tells the game that we need to switch to the Mid game boss Fight.
        /// </summary>
        /// <param name="bossTime">Amount of time in seconds before the boss fight triggers.</param>
        /// <returns>true of false.</returns>
        private bool IsBossTime(float bossTime)
        {
            if (this.midBossTime >= bossTime)
            {
                return true;
            }

            return false;
        }
    }
}
