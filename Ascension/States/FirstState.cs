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
        /// Texture for the ball.
        /// </summary>
        protected Texture2D ballTexture;

        /// <summary>
        /// Position of the ball.
        /// </summary>
        protected Vector2 ballPosition;

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

        protected float ballSpeed;
        protected Texture2D borderTexture;
        private Texture2D backGround;


        private float enemySpawnTimer = 0f;
        private float enemySpawnInterval = 0.5f; // Spawn every 3 seconds
        private List<Enemy> enemies = new List<Enemy>();
        private BasicEnemyFactory basicEnemyFactory;

        public FirstState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            this.ballTexture = content.Load<Texture2D>("ball");
            this.ballPosition = new Vector2(graphicsDevice.Viewport.Width / 4, graphicsDevice.Viewport.Height / 2);
            this.backGround = content.Load<Texture2D>("Backgrounds/AscensionTitle");
            this.borderTexture = new Texture2D(graphicsDevice, 1, 1);
            this.borderTexture.SetData(new[] { Color.AliceBlue });
            this.ballSpeed = 100f;

            this.basicEnemyFactory = new BasicEnemyFactory(content, graphicsDevice);
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
            float updatedBallSpeed = this.ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds * 5;

            this.midBossTime += (float)gameTime.ElapsedGameTime.TotalSeconds; // when to change to midboss state

            if (this.IsBossTime(15f))
            {
                this.game.ChangeState(new SecondState(this.game, this.graphicsDevice, this.content, this.ballPosition));
            }

            this.BallMovement(updatedBallSpeed);
            this.StayInBorder();

            // Update enemies
            foreach (var enemy in this.enemies)
            {
                enemy.Update(gameTime);
            }

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
        protected void BallMovement(float updatedBallSpeed)
        {
            var kstate = Keyboard.GetState();

            // Vector to normalize diagonal movement
            Vector2 dir = Vector2.Zero;

            if (kstate.IsKeyDown(Keys.W))
            {
                dir.Y -= 1;
            }

            if (kstate.IsKeyDown(Keys.S))
            {
                dir.Y += 1;
            }

            if (kstate.IsKeyDown(Keys.A))
            {
                dir.X -= 1;
            }

            if (kstate.IsKeyDown(Keys.D))
            {
                dir.X += 1;
            }

            if (kstate.IsKeyDown(Keys.LeftShift))
            {
                this.ballSpeed = 120f;
            }
            else
            {
                this.ballSpeed = 60f;
            }

            // If Vector has values, normalize movement.
            if (dir != Vector2.Zero)
            {
                dir.Normalize();
            }

            // Update position after normalizing
            this.ballPosition += dir * updatedBallSpeed;
        }

        /// <summary>
        /// Keeps the ball in the border.
        /// </summary>
        protected void StayInBorder()
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

        private void SpawnEnemy()
        {
            // Define the minimum distance between enemies
            float minDistance = 60f; // Adjust this value as needed

            // Calculate a random X position along the top border
            Random random = new Random();
            Vector2 spawnPosition;
            bool positionIsValid;

            do
            {
                float randomX = random.Next(
                    this.borderRect.X + this.borderWidth,
                    this.borderRect.X + this.borderRect.Width - this.borderWidth);

                spawnPosition = new Vector2(
                    randomX,
                    this.borderRect.Y + this.borderWidth); // Spawn at the top of the border

                // Check if the new spawn position is too close to any existing enemy
                positionIsValid = true;
                foreach (var currEnemy in this.enemies)
                {
                    if (Vector2.Distance(spawnPosition, currEnemy.Position) < minDistance)
                    {
                        positionIsValid = false;
                        break;
                    }
                }
            } while (!positionIsValid);

            // Create and add the new enemy
            Enemy enemy = this.basicEnemyFactory.CreateEnemy(spawnPosition, "EnemyA");
            this.enemies.Add(enemy);
        }
    }
}
