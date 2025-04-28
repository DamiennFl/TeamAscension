// <copyright file="MainGameState.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Diagnostics;
using Ascension.Business_Layer.Shooting;
using Ascension.Business_Layer.Waves;
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

        /// <summary>
        /// Wave manager for the game.
        /// </summary>
        private WaveManager waveManager;

        /// <summary>
        /// Enemy manager for the game.
        /// </summary>
        private EnemyManager enemyManager;

        /// <summary>
        /// Collision manager for the game.
        /// </summary>
        private CollisionManager collisionManager;

        /// <summary>
        /// Bullet manager for the game.
        /// </summary>
        private BulletManager bulletManager;

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
            this.player = new Player(graphicsDevice, contentManager, this.playArea, 4);
            this.player.ShootingPattern = new StandardShootingPattern();

            this.InitCollisions();
            this.InitBulletManager();
            this.InitEnemyManager();
            this.InitWaveManager();
            this.player.Bomb = new Bomb(this.enemyManager, this.bulletManager);
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

            if (!this.waveManager.WavesLeft())
            {
                this.game.ChangeState(new GameWinState(this.game, this.graphicsDevice, this.contentManager));
            }

            this.waveManager.Update(gameTime);

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
        /// Initializes the collision system.
        /// </summary>
        private void InitCollisions()
        {
            // Initialize collision system
            this.collisionManager = new CollisionManager();
            this.collisionManager.AddCollisionLayer("Player", "EnemyBullet");
            this.collisionManager.AddCollisionLayer("Enemy", "PlayerBullet");
            this.collisionManager.Register(this.player);
        }

        /// <summary>
        /// Initializes the enemy manager.
        /// </summary>
        private void InitEnemyManager()
        {
            this.enemyManager = new EnemyManager(this.contentManager, this.graphicsDevice, this.collisionManager, this.bulletManager, this.playArea);
        }

        /// <summary>
        /// Initializes the bullet manager.
        /// </summary>
        private void InitBulletManager()
        {
            this.bulletManager = new BulletManager(this.collisionManager, this.contentManager);
            this.player.bulletManager = this.bulletManager;
        }

        /// <summary>
        /// Initializes the wave manager.
        /// </summary>
        private void InitWaveManager()
        {
            this.waveManager = new WaveManager(this.enemyManager, this.playArea);
        }
    }
}