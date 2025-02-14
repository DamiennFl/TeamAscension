﻿// <copyright file="GameState.cs" company="Team Ascension">
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

        protected Rectangle rect;

        private float enemySpawnTimer = 0f;
        private float enemySpawnInterval = 0.5f; // Spawn every 0.5 seconds
        protected Player player;

        private List<Enemy> enemies = new List<Enemy>();
        private BasicEnemyFactory basicEnemyFactory;

        public FirstState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            this.backGround = content.Load<Texture2D>("Backgrounds/Stage1");
            this.borderTexture = new Texture2D(graphicsDevice, 1, 1);
            this.borderTexture.SetData(new[] { Color.AliceBlue });
            this.player = new Player(content.Load<Texture2D>("ball"), new Vector2(graphicsDevice.Viewport.Width / 4, graphicsDevice.Viewport.Height / 2));

            this.basicEnemyFactory = new BasicEnemyFactory(content, graphicsDevice);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            this.graphicsDevice.Clear(Color.Black);

            this.BorderDraw(spriteBatch);

            // Adjust the background rectangle to fit nicely within the borderRect and borderWidth
            this.DrawBackground(spriteBatch, this.backGround);

            // Player drawn here
            this.player.Draw(spriteBatch);

            // Enemies drawn here
            foreach (var currEnemy in this.enemies)
            {
                currEnemy.Draw(spriteBatch);
            }

            this.BorderBuffer(spriteBatch);

            spriteBatch.End();
        }

        /// <summary>
        /// Draws the background for us.
        /// </summary>
        /// <param name="spriteBatch">Our spriteBatch.</param>
        public void DrawBackground(SpriteBatch spriteBatch, Texture2D backGround)
        {
            int adjustedX = this.borderRect.X + this.borderWidth;
            int adjustedY = this.borderRect.Y + this.borderWidth;
            int adjustedWidth = this.borderRect.Width - (2 * this.borderWidth);
            int adjustedHeight = this.borderRect.Height - (2 * this.borderWidth);
            this.rect = new Rectangle(adjustedX, adjustedY, adjustedWidth, adjustedHeight);
            spriteBatch.Draw(backGround, this.rect, Color.White);
        }

        /// <summary>
        /// Draws the border for us.
        /// </summary>
        /// <param name="spriteBatch">our sprite.</param>
        public void BorderDraw(SpriteBatch spriteBatch)
        {
            // Top part of rectangle
            spriteBatch.Draw(this.borderTexture, new Rectangle(this.borderRect.X, this.borderRect.Y, this.borderRect.Width, this.borderWidth), this.borderColor);

            // Bottom part of rectangle
            spriteBatch.Draw(this.borderTexture, new Rectangle(this.borderRect.X, this.borderRect.Y + this.borderRect.Height - this.borderWidth, this.borderRect.Width, this.borderWidth), this.borderColor);

            // Left part of rectangle
            spriteBatch.Draw(this.borderTexture, new Rectangle(this.borderRect.X, this.borderRect.Y, this.borderWidth, this.borderRect.Height), this.borderColor);

            // Right part of rectangle
            spriteBatch.Draw(this.borderTexture, new Rectangle(this.borderRect.X + this.borderRect.Width - this.borderWidth, this.borderRect.Y, this.borderWidth, this.borderRect.Height), this.borderColor);
        }

        /// <summary>
        /// Draw a buffer for enemies. to spawn in.
        /// </summary>
        /// <param name="spriteBatch">our sprite.</param>
        public void BorderBuffer(SpriteBatch spriteBatch)
        {
            // Top part of rectangle
            spriteBatch.Draw(this.borderTexture, new Rectangle(this.borderRect.X - 40, this.borderRect.Y - 40, this.borderRect.Width + 90, this.borderWidth + 40), this.borderColor);

            // Bottom part of rectangle
            spriteBatch.Draw(this.borderTexture, new Rectangle(this.borderRect.X - 30, this.borderRect.Y + this.borderRect.Height - this.borderWidth, this.borderRect.Width + 30, this.borderWidth + 40), this.borderColor);

            // Left part of rectangle
            spriteBatch.Draw(this.borderTexture, new Rectangle(this.borderRect.X - 60, this.borderRect.Y - 30, this.borderWidth + 60, this.borderRect.Height + 60), this.borderColor);

            // Right part of rectagnle
            spriteBatch.Draw(this.borderTexture, new Rectangle(this.borderRect.X + this.borderRect.Width - this.borderWidth, this.borderRect.Y - 30, this.borderWidth + 300, this.borderRect.Height + 30), this.borderColor);
        }

        public override void PostUpdate(GameTime gameTime)
        {
            // throw new NotImplementedException();
        }

        /// <summary>
        /// Our update method!.
        /// </summary>
        /// <param name="gameTime">.</param>
        public override void Update(GameTime gameTime)
        {
            float updatedPlayerSpeed = this.player.playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            this.midBossTime += (float)gameTime.ElapsedGameTime.TotalSeconds; // when to change to midboss state

            //if (this.IsBossTime(15f))
            //{
            //    this.game.ChangeState(new SecondState(this.game, this.graphicsDevice, this.content, this.player));
            //}

            foreach (var currEnemy in this.enemies)
            {
                // Updates the current enemy
                currEnemy.Update(gameTime);
            }

            this.enemySpawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (this.enemySpawnTimer > this.enemySpawnInterval)
            {
                this.SpawnEnemy();
                this.enemySpawnTimer = 0;
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
                    (this.borderRect.Y - 23) + this.borderWidth); // Spawn at the top of the border

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
            }
            while (!positionIsValid);

            // Create and add the new enemy
            Enemy enemy = this.basicEnemyFactory.CreateEnemy(spawnPosition, "EnemyA");
            this.enemies.Add(enemy);
        }
    }
}
