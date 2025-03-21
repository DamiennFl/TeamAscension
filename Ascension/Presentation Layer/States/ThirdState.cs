// <copyright file="ThirdState.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using Ascension.Enemies;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ascension.Content.States;

namespace Ascension.States
{
    /// <summary>
    /// The third state.
    /// </summary>
    internal class ThirdState : FirstState
    {       

        /// <summary>
        /// The list of components.
        /// </summary>
        private readonly List<Components> components;

        /// <summary>
        /// Screen height.
        /// </summary>
        private int screenHeight;

        private float finalBossTimer;

        /// <summary>
        /// Screen width.
        /// </summary>
        private int screenWidth;

        /// <summary>
        /// The background texture.
        /// </summary>
        private Texture2D backGround;

        /// <summary>
        /// Enemy formations.
        /// </summary>
        private List<EnemyFormation> enemyFormations;

        private ConcreteEnemyFactory basicEnemyFactory;

        private BossEnemyFactory bossEnemyFactory;



        /// <summary>
        /// Initializes a new instance of the <see cref="SecondState"/> class.
        /// </summary>
        /// <param name="game">The game itself.</param>
        /// <param name="graphicsDevice">Graphics device.</param>
        /// <param name="content">Content manager.</param>
        /// <param name="currentPlayer">Current player.</param>
        /// <param name="currentEnemyFormation">Enemy formation.</param>
        public ThirdState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, Player currentPlayer, List<EnemyFormation> currentEnemyFormation)
            : base(game, graphicsDevice, content)
        {
            this.screenHeight = graphicsDevice.Viewport.Height;
            this.screenWidth = graphicsDevice.Viewport.Width;
            this.backGround = content.Load<Texture2D>("Backgrounds/Stage3");
            this.components = new List<Components>();
            this.player = currentPlayer;
            this.enemyFormations = currentEnemyFormation;

            // Initialize factories
            this.basicEnemyFactory = new ConcreteEnemyFactory(content, graphicsDevice);
            this.bossEnemyFactory = new BossEnemyFactory(content, graphicsDevice); // Already initialized

            // Initialize a MidBoss LinearFormation
            Vector2 finalBossStart = new Vector2(100, 0);
            Vector2 finalBossEnd = new Vector2(100, 100);
            int finalBossCount = 1;
            float finalBossSpawnDelay = 0.5f;
            Vector2 finalBossVelocity = new Vector2(0, 300);
            float finalBossSpacing = 50f;
            string finalBossType = "FinalBoss";

            LinearFormation finalBossFormation = new LinearFormation(
                finalBossStart,
                finalBossEnd,
                finalBossCount,
                finalBossSpawnDelay,
                finalBossVelocity,
                finalBossSpacing,
                this.bossEnemyFactory,
                finalBossType);

            this.enemyFormations.Add(finalBossFormation);

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
        }

        /// <summary>
        /// Draw method for second state.
        /// </summary>
        /// <param name="gameTime">Time of game.</param>
        /// <param name="spriteBatch">Sprites.</param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            this.graphicsDevice.Clear(Color.Black);

            this.BorderDraw(spriteBatch);

            this.DrawBackground(spriteBatch, this.backGround);

            foreach (var formation in this.enemyFormations)
            {
                formation.Draw(spriteBatch);
            }

            this.player.Draw(spriteBatch);

            this.BorderDraw(spriteBatch);
            this.BorderBuffer(spriteBatch);

            spriteBatch.End();
        }

        /// <summary>
        /// Update method for second state.
        /// </summary>
        /// <param name="gameTime">Time of the game.</param>
        public override void Update(GameTime gameTime)
        {
            float updatedPlayerSpeed = this.player.PlayerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds * 5;

            this.finalBossTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (var formation in this.enemyFormations)
            {
                formation.Update(gameTime);
            }

            this.player.PlayerMovement(updatedPlayerSpeed);
            this.player.StayInBorder(this.borderRect, this.borderWidth);

        }
    }
}
