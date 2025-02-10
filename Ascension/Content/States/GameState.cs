// <copyright file="GameState.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

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
    public class GameState : State
    {
        /// <summary>
        /// Texture for the ball.
        /// </summary>
        private Texture2D ballTexture;

        /// <summary>
        /// Position of the ball.
        /// </summary>
        private Vector2 ballPosition;

        /// <summary>
        /// Border rectangle.
        /// </summary>
        private Rectangle borderRect = new Rectangle(40, 40, 460, 720);

        /// <summary>
        /// Our Border width.
        /// </summary>
        private int borderWidth = 4;

        /// <summary>
        /// Border color.
        /// </summary>
        private Color borderColor = Color.Black;

        private float ballSpeed;
        private Texture2D borderTexture;

        private List<Enemy> enemies = new List<Enemy>();
        private BasicEnemyFactory enemyFactory;
        private float enemySpawnTimer = 0f;
        private float enemySpawnInterval = 3f; // Spawn every 3 seconds

        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            this.ballTexture = content.Load<Texture2D>("ball");
            this.ballPosition = new Vector2(graphicsDevice.Viewport.Width / 4, graphicsDevice.Viewport.Height / 2);
            this.borderTexture = new Texture2D(graphicsDevice, 1, 1);
            this.borderTexture.SetData(new[] { Color.White });
            this.ballSpeed = 100f;

            this.enemyFactory = new BasicEnemyFactory(content, graphicsDevice);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(this.borderTexture, new Rectangle(this.borderRect.X, this.borderRect.Y, this.borderRect.Width, this.borderWidth), this.borderColor);
            spriteBatch.Draw(this.borderTexture, new Rectangle(this.borderRect.X, this.borderRect.Y + this.borderRect.Height - this.borderWidth, this.borderRect.Width, this.borderWidth), this.borderColor);
            spriteBatch.Draw(this.borderTexture, new Rectangle(this.borderRect.X, this.borderRect.Y, this.borderWidth, this.borderRect.Height), this.borderColor);
            spriteBatch.Draw(this.borderTexture, new Rectangle(this.borderRect.X + this.borderRect.Width - this.borderWidth, this.borderRect.Y, this.borderWidth, this.borderRect.Height), this.borderColor);

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

            // Draw enemies
            foreach (var enemy in this.enemies)
            {
                enemy.Draw(spriteBatch);
            }

            spriteBatch.End();
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
            float updatedBallSpeed = this.ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds * 5;

            this.BallMovement(updatedBallSpeed);
            this.StayInBorder();

            // Update enemies
            foreach (var enemy in this.enemies)
            {
                enemy.Update(gameTime);
            }

            // Spawn enemies
            this.enemySpawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (this.enemySpawnTimer > this.enemySpawnInterval)
            {
                this.SpawnEnemy();
                this.enemySpawnTimer = 0;
            }
        }

        /// <summary>
        /// Moves the ball.
        /// </summary>
        /// <param name="updatedBallSpeed">Updated ball speed.</param>
        private void BallMovement(float updatedBallSpeed)
        {
            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.W))
            {
                this.ballPosition.Y -= updatedBallSpeed;
            }

            if (kstate.IsKeyDown(Keys.S))
            {
                this.ballPosition.Y += updatedBallSpeed;
            }

            if (kstate.IsKeyDown(Keys.A))
            {
                this.ballPosition.X -= updatedBallSpeed;
            }

            if (kstate.IsKeyDown(Keys.D))
            {
                this.ballPosition.X += updatedBallSpeed;
            }

            if (kstate.IsKeyDown(Keys.LeftShift))
            {
                this.ballSpeed = 150f;
            }
            else
            {
                this.ballSpeed = 100f;
            }
        }

        /// <summary>
        /// Keeps the ball in the border.
        /// </summary>
        private void StayInBorder()
        {
            int radius = this.ballTexture.Width / 8;

            // Left border
            if (this.ballPosition.X - radius < this.borderRect.X)
            {
                this.ballPosition.X = this.borderRect.X + radius;
            }

            // Right border
            else if (this.ballPosition.X + radius > this.borderRect.X + this.borderRect.Width - (2 * this.borderWidth))
            {
                this.ballPosition.X = this.borderRect.X + this.borderRect.Width - (2 * this.borderWidth) - radius;
            }

            // Top border
            if (this.ballPosition.Y - radius < this.borderRect.Y)
            {
                this.ballPosition.Y = this.borderRect.Y + radius;
            }

            // Bottom border
            else if (this.ballPosition.Y + radius > this.borderRect.Y + this.borderRect.Height - (2 * this.borderWidth))
            {
                this.ballPosition.Y = this.borderRect.Y + this.borderRect.Height - (2 * this.borderWidth) - radius;
            }
        }

        private void SpawnEnemy()
        {
            // Calculate a random X position along the top border
            Random random = new Random();
            float randomX = random.Next(
                this.borderRect.X + this.borderWidth,
                this.borderRect.X + this.borderRect.Width - this.borderWidth);

            Vector2 spawnPosition = new Vector2(
                randomX,
                this.borderRect.Y + this.borderWidth); // Spawn at the top of the border

            Enemy enemy = this.enemyFactory.CreateEnemy(spawnPosition);
            this.enemies.Add(enemy);
        }
    }
}
