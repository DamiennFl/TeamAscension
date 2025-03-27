// <copyright file="FirstState.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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

            //this.playArea.DrawSpawnRectangles(spriteBatch);

            this.enemyManager.Draw(spriteBatch);

            this.bulletManager.Draw(spriteBatch);

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

            if (this.IsBossTime(190f))
            {
                Debug.WriteLine("I switched states");
                this.game.ChangeState(new GameWinState(this.game, this.graphicsDevice, this.contentManager));
            }

            this.enemyManager.Update(gameTime);

            this.bulletManager.Update(gameTime);

            this.player.Update(gameTime);

            this.collisionManager.Update();

            if (this.player.IsDead)
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

        /// <summary>
        /// Initializes the waves for the game.
        /// </summary>
        private void InitWaves()
        {

            float durationA = 10;
            string enemyTypeA = "EnemyA";
            int enemyCountA = 8;
            float spawnIntervalA = 0.5f;
            string movementPatternA = "Linear";
            int healthA = 5;
            string bulletTypeA = "A";

            Wave waveA = new Wave(durationA, enemyTypeA, enemyCountA, spawnIntervalA, healthA, movementPatternA, bulletTypeA);
            this.waves.Add(waveA);

            float durationB = 40;
            string enemyTypeB = "EnemyB";
            int enemyCountB = 5;
            float spawnIntervalB = 0.3f;
            string movementPatternB = "Wave";
            int healthB = 10;
            string bulletTypeB = "B";

            Wave waveB = new Wave(durationB, enemyTypeB, enemyCountB, spawnIntervalB, healthB, movementPatternB, bulletTypeB);
            this.waves.Add(waveB);

            float durationMid = 60;
            string enemyTypeMid = "MidBoss";
            int enemyCountMid = 1;
            float spawnIntervalMid = 0.3f;
            string movementPatternMid = "ZigZag";
            int healthMid = 40;
            string bulletTypeMid = "B";

            Wave waveMid = new Wave(durationMid, enemyTypeMid, enemyCountMid, spawnIntervalMid, healthMid, movementPatternMid, bulletTypeMid);
            this.waves.Add(waveMid);

            float durationC = 20;
            string enemyTypeC = "EnemyA";
            int enemyCountC = 10;
            float spawnIntervalC = 0.5f;
            string movementPatternC = "ZigZag";
            int healthC = 10;
            string bulletTypeC = "A";

            Wave waveC = new Wave(durationC, enemyTypeC, enemyCountC, spawnIntervalC, healthC, movementPatternC, bulletTypeC);
            this.waves.Add(waveC);

            float duration = 90;
            string enemyType = "FinalBoss";
            int enemyCount = 1;
            float spawnInterval = 0.3f;
            string movementPattern = "GoMiddle";
            int health = 150;
            string bulletType = "A";

            Wave testWave = new Wave(duration, enemyType, enemyCount, spawnInterval, health, movementPattern, bulletType);
            this.waves.Add(testWave);
        }

        /// <summary>
        /// Initializes the enemy manager.
        /// </summary>
        private void InitEnemyManager()
        {
            this.enemyManager = new EnemyManager(this.contentManager, this.graphicsDevice, this.collisionManager, this.bulletManager, this.waves, this.playArea);
        }

        /// <summary>
        /// Initializes the bullet manager.
        /// </summary>
        private void InitBulletManager()
        {
            this.bulletManager = new BulletManager(this.collisionManager, this.contentManager);
            this.player.bulletManager = this.bulletManager;
        }
    }
}