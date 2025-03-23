// <copyright file="FirstState.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System.Collections.Generic;
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
#pragma warning disable SA1401 // Fields should be private
        protected Player player;

        /// <summary>
        /// Time for midboss.
        /// </summary>
        private float midBossTime = 0f;

        /// <summary>
        /// List of enemy formations.
        /// </summary>
        //private List<EnemyFormation> enemyFormations = new List<EnemyFormation>();

        /// <summary>
        /// Basic enemy factory.
        /// </summary>
        //private BasicEnemyFactory basicEnemyFactory;

        /// <summary>
        /// Defines a play area.
        /// </summary>
        private PlayArea playArea;

        private EnemyManager enemyManager;

        private CollisionManager collisionManager;

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
            this.player = new Player(graphicsDevice, content, this.playArea);

            this.InitCollisions();
            this.InitEnemyManager();
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

            //foreach (var formation in this.enemyFormations)
            //{
            //    formation.Draw(spriteBatch);
            //}

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

            if (this.IsBossTime(20f))
            {
               this.game.ChangeState(new GameWinState(this.game, this.graphicsDevice, this.content));
            }

            this.enemyManager.Update(gameTime);

            this.player.Update(gameTime);

            this.collisionManager.Update();

            if (this.player.LossCondition())
            {
                this.game.ChangeState(new GameOverState(this.game, this.graphicsDevice, this.content));
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

        }

        private void InitEnemyManager()
        {
            this.enemyManager = new EnemyManager(content, graphicsDevice, this.collisionManager);
            this.InitWaves();
        }
    }
}