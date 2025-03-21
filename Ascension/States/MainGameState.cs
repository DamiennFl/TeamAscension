// <copyright file="FirstState.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System.Collections.Generic;
using Ascension.Enemies;
using Ascension.Enemies.EnemyFormation;
using Ascension.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Ascension.Content.States
{
    /// <summary>
    /// The game state.
    /// </summary>
    public class MainGameState : State
    {
        /// <summary>
        /// Player object.
        /// </summary>
#pragma warning disable SA1401 // Fields should be private
        protected Player player;


        /// <summary>
        /// Time for midboss.
        /// </summary>
        private float midBossTime = 0f;

        /// <summary>
        /// Timer for spawning enemies.
        /// </summary>
        private float enemySpawnTimer = 0f;

        private bool check = true;

        /// <summary>
        /// Interval for spawning enemies.
        /// </summary>
        private float enemySpawnInterval = 0.5f; // Spawn every 0.5 seconds

        /// <summary>
        /// List of enemy formations.
        /// </summary>
        private List<EnemyFormation> enemyFormations = new List<EnemyFormation>();

        /// <summary>
        /// Basic enemy factory.
        /// </summary>
        private BasicEnemyFactory basicEnemyFactory;

        private BossEnemyFactory bossEnemyFactory;

        /// <summary>
        /// Defines a play area.
        /// </summary>
        private PlayArea playArea;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainGameState"/> class.
        /// </summary>
        /// <param name="game">The game itself.</param>
        /// <param name="graphicsDevice">Graphics device for the first state.</param>
        /// <param name="content">Content manager for the first state.</param>
        public MainGameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            this.playArea = new PlayArea(graphicsDevice, content);
            this.player = new Player(graphicsDevice, content);

            this.basicEnemyFactory = new BasicEnemyFactory(content, graphicsDevice);
            this.bossEnemyFactory = new BossEnemyFactory(content, graphicsDevice); // Already initialized

            // Initialize a LinearFormation
            Vector2 formationStartPosition = new Vector2(100, 0); // Start position off-screen
            Vector2 formationEndPosition = new Vector2(100, 100); // End position on-screen
            int numEnemies = 7;
            float spawnDelay = 0.5f;
            Vector2 enemyVelocity = new Vector2(0, 100);
            float enemySpacing = 50f;
            string enemyType = "EnemyA";

            LinearFormation linearFormation = new LinearFormation(formationStartPosition, formationEndPosition, numEnemies, spawnDelay, enemyVelocity, enemySpacing, this.basicEnemyFactory, enemyType);
            this.enemyFormations.Add(linearFormation);

            // Initialize a SwoopFormation
            Vector2 swoopFormationStartPosition = new Vector2(200, 0); // Start position off-screen
            int swoopNumEnemies = 5;
            float swoopSpawnDelay = 0.5f;
            Vector2 swoopEnemyVelocity = new Vector2(0, 100);
            float swoopEnemySpacing = 50f;
            string swoopEnemyType = "EnemyA";

            SwoopFormation swoopFormation = new SwoopFormation(swoopFormationStartPosition, swoopNumEnemies, swoopSpawnDelay, swoopEnemyVelocity, swoopEnemySpacing, this.basicEnemyFactory, swoopEnemyType);
            this.enemyFormations.Add(swoopFormation);
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

            foreach (var formation in this.enemyFormations)
            {
                formation.Draw(spriteBatch);
            }

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
            float updatedPlayerSpeed = this.player.PlayerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            this.midBossTime += (float)gameTime.ElapsedGameTime.TotalSeconds; // when to change to midboss state

            if (this.IsBossTime(20f) && this.check)
            {
                this.check = false;
                // Initialize a MidBoss LinearFormation
                Vector2 midBossStart = new Vector2(100, 0);
                Vector2 midBossEnd = new Vector2(100, 100);
                int midBossCount = 1;
                float midBossSpawnDelay = 0.5f;
                Vector2 midBossVelocity = new Vector2(0, 100);
                float midBossSpacing = 50f;
                string midBossType = "MidBoss";

                LinearFormation midBossFormation = new LinearFormation(
                    midBossStart,
                    midBossEnd,
                    midBossCount,
                    midBossSpawnDelay,
                    midBossVelocity,
                    midBossSpacing,
                    this.bossEnemyFactory,
                    midBossType);
                this.enemyFormations.Add(midBossFormation);

                // Initialize an EnemyB LinearFormation
                Vector2 enemyBStart = new Vector2(300, 0);
                Vector2 enemyBEnd = new Vector2(300, 100);
                int enemyBCount = 3;
                float enemyBSpawnDelay = 1f;
                Vector2 enemyBVelocity = new Vector2(0, 100);
                float enemyBSpacing = 50f;
                string enemyBType = "EnemyB";

                LinearFormation enemyBFormation = new LinearFormation(
                    enemyBStart,
                    enemyBEnd,
                    enemyBCount,
                    enemyBSpawnDelay,
                    enemyBVelocity,
                    enemyBSpacing,
                    this.basicEnemyFactory, // Use appropriate factory
                    enemyBType);

                this.enemyFormations.Add(enemyBFormation);
            }

            foreach (var formation in this.enemyFormations)
            {
                formation.Update(gameTime);
            }

            this.player.PlayerMovement(updatedPlayerSpeed);
            this.player.StayInBorder(this.playArea.BorderRectangle, this.playArea.BorderWidth);
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
    }
}