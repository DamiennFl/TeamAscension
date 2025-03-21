﻿// <copyright file="SecondState.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ascension.Content.Controls;
using Ascension.Content.States;
using Ascension.Enemies;
using Ascension.Enemies.EnemyFormation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Ascension.States
{
    /// <summary>
    /// The second state.
    /// </summary>
    internal class SecondState : FirstState
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

        private BasicEnemyFactory basicEnemyFactory;

        private BossEnemyFactory bossEnemyFactory;



        /// <summary>
        /// Initializes a new instance of the <see cref="SecondState"/> class.
        /// </summary>
        /// <param name="game">The game itself.</param>
        /// <param name="graphicsDevice">Graphics device.</param>
        /// <param name="content">Content manager.</param>
        /// <param name="currentPlayer">Current player.</param>
        /// <param name="currentEnemyFormation">Enemy formation.</param>
        public SecondState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, Player currentPlayer, List<EnemyFormation> currentEnemyFormation)
            : base(game, graphicsDevice, content)
        {
            this.screenHeight = graphicsDevice.Viewport.Height;
            this.screenWidth = graphicsDevice.Viewport.Width;
            this.backGround = content.Load<Texture2D>("Backgrounds/Stage2");
            this.components = new List<Components>();
            this.player = currentPlayer;
            this.enemyFormations = currentEnemyFormation;

            // Initialize factories
            this.basicEnemyFactory = new BasicEnemyFactory(content, graphicsDevice);
            this.bossEnemyFactory = new BossEnemyFactory(content, graphicsDevice); // Already initialized

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


            if (this.finalBossTimer >= 30)
            {
                this.game.ChangeState(new ThirdState(this.game, this.graphicsDevice, this.content, this.player, this.enemyFormations));
            }
        }
    }
}