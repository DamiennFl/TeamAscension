// <copyright file="FirstState.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Ascension;

namespace Ascension
{
    /// <summary>
    /// The game state.
    /// </summary>
    public class MainGameState : State
    {
        /// <summary>
        /// Player object.
        /// </summary>
        protected Player player;

        /// <summary>
        /// Time for midboss.
        /// </summary>
        private float midBossTime = 0f;

        /// <summary>
        /// Defines a play area.
        /// </summary>
        private PlayArea playArea;

        private EnemyManager enemyManager;

        private CollisionManager collisionManager;

        private BulletManager bulletManager;

        private List<Wave> waves = new List<Wave>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MainGameState"/> class.
        /// </summary>
        /// <param name="game">The game itself.</param>
        /// <param name="graphicsDevice">Graphics device for the first state.</param>
        /// <param name="content">Content manager for the first state.</param>
        public MainGameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager contentManager)
            : base(game, graphicsDevice, contentManager)
        {
            this.playArea = new PlayArea(graphicsDevice, contentManager);
            this.player = new Player(graphicsDevice, contentManager, this.playArea);

            this.InitCollisions();
            this.InitWaves();
            this.InitBulletManager();
            this.InitEnemyManager();
            this.bulletManager.RegisterPlayer(this.player);
        }

        /// <summary>
        /// Draw method for drawing the game state.
        /// </summary>
        /// <param name="gameTime">Time of the game.</param>
        /// <param name="spriteBatch">Sprites used.</param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            this.graphicsDevice.Clear(Color.Black);

            this.playArea.DrawBackground(spriteBatch);

            // Adjust the background rectangle to fit nicely within the borderRect and borderWidth
            this.playArea.BorderDraw(spriteBatch);

            // Player drawn here
            this.player.Draw(spriteBatch);

            this.enemyManager.Draw(spriteBatch);

            this.bulletManager.Draw(spriteBatch);

            this.playArea.BorderBuffer(spriteBatch);

            spriteBatch.End();
        }

        /// <summary>
        /// Post update method.
        /// </summary>
        /// <param name="gameTime">Time of the game.</param>
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
            this.midBossTime += (float)gameTime.ElapsedGameTime.TotalSeconds; // when to change to midboss state

            if (this.IsBossTime(60f))
            {
                Debug.WriteLine("I switched states");
                this.game.ChangeState(new GameWinState(this.game, this.graphicsDevice, this.contentManager));
            }

            this.enemyManager.Update(gameTime);

            this.bulletManager.Update(gameTime);

            this.player.Update(gameTime);

            this.collisionManager.Update();

            if (this.player.LossCondition())
            {
                this.game.ChangeState(new GameOverState(this.game, this.graphicsDevice, this.contentManager));
            }
        }

        /// <summary>
        /// Check if it is time for the boss.
        /// </summary>
        /// <param name="bossTime">Time the boss should spawn.</param>
        /// <returns>True if time for boss to spawn, false if not.</returns>
        private bool IsBossTime(float bossTime)
        {
            return this.midBossTime >= bossTime;
        }

        private void InitCollisions()
        {
            // Initialize collision system
            this.collisionManager = new CollisionManager();
            this.collisionManager.AddCollisionLayer("Player", "EnemyBullet");
            this.collisionManager.AddCollisionLayer("Enemy", "PlayerBullet");
            this.collisionManager.Register(this.player);
        }

        private void InitWaves()
        {
            float duration = 5;
            string enemyType = "EnemyA";
            int enemyCount = 5;
            float spawnInterval = 0.3f;
            int health = 10;
            string movementPattern = "ZigZag";

            Wave testWave = new Wave(duration, enemyType, enemyCount, spawnInterval, health, movementPattern);
            this.waves.Add(testWave);

            float duration2 = 10;
            string enemyType2 = "EnemyB";
            int enemyCount2 = 10;
            float spawnInterval2 = 0.3f;
            int health2 = 10;
            string movementPattern2 = "Linear";

            Wave testWave2 = new Wave(duration2, enemyType2, enemyCount2, spawnInterval2, health2, movementPattern2);
            this.waves.Add(testWave2);
        }

        private void InitEnemyManager()
        {
            this.enemyManager = new EnemyManager(this.contentManager, this.graphicsDevice, this.collisionManager, this.bulletManager, this.waves, this.playArea);
        }

        private void InitBulletManager()
        {
            this.bulletManager = new BulletManager(this.contentManager, this.collisionManager);
        }
    }
}