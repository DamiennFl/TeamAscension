// <copyright file="WaveManager.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Ascension.Business_Layer.Waves
{
    /// <summary>
    /// WaveManager manages the waves.
    /// </summary>
    internal class WaveManager
    {
        /// <summary>
        /// The list of Waves.
        /// </summary>
        private List<Wave> waves;

        /// <summary>
        /// Global variable to track the current duration of the Wave.
        /// </summary>
        private float waveTimeElapsed;

        /// <summary>
        /// An index for the current wave.
        /// </summary>
        private int currentWaveIndex;

        /// <summary>
        /// enemyManager to manipulate the enemies in the game.
        /// </summary>
        private EnemyManager enemyManager;

        /// <summary>
        /// The playArea of the game.
        /// </summary>
        private PlayArea playArea;

        /// <summary>
        /// The borderManager of the game, based on playArea.
        /// </summary>
        private BorderManager borderManager;

        /// <summary>
        /// The selected spawnArea.
        /// </summary>
        private Rectangle spawnArea;

        /// <summary>
        /// The selected spawnPosition.
        /// </summary>
        private Vector2 spawnPosition;

        /// <summary>
        /// The selected spawnVelocity.
        /// </summary>
        private Vector2 spawnVelocity;

        /// <summary>
        /// waveBuilder is used to generate the waves.
        /// </summary>
        private WaveBuilder waveBuilder;

        /// <summary>
        /// The number of Enemies spawned in this Wave.
        /// </summary>
        private int enemiesSpawned;

        /// <summary>
        /// Initializes a new instance of the <see cref="WaveManager"/> class.
        /// </summary>
        /// <param name="enemyManager">The game's enemyManager.</param>
        /// <param name="playArea">The playArea of the Game.</param>
        public WaveManager(EnemyManager enemyManager, PlayArea playArea)
        {
            this.waveTimeElapsed = 0f;
            this.currentWaveIndex = 0;
            this.enemyManager = enemyManager;
            this.playArea = playArea;
            this.borderManager = new BorderManager(this.playArea);
            this.waveBuilder = new WaveBuilder();
            this.waves = this.waveBuilder.GenerateWaves();
            this.enemiesSpawned = 0;
        }

        /// <summary>
        /// Update method for WaveManager.
        /// </summary>
        /// <param name="gameTime">The gameTime of the Game.</param>
        public void Update(GameTime gameTime)
        {
            // Iterate through waves
            if (this.currentWaveIndex < this.waves.Count)
            {
                // Get current Wave and iterate time
                Wave currentWave = this.waves[this.currentWaveIndex];
                this.waveTimeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

                // Get the inital spawn and velocity for the Enemies of the Wave if none have been spawned.
                if (this.enemiesSpawned == 0)
                {
                    Random random = new Random();
                    List<Rectangle> spawnAreas = this.playArea.SpawnAreaRectangles;
                    this.spawnArea = spawnAreas[random.Next(spawnAreas.Count)];

                    this.spawnPosition = new Vector2(
                        random.Next(this.spawnArea.Left + (this.spawnArea.Width / 4), this.spawnArea.Right - (this.spawnArea.Width / 4)),
                        random.Next(this.spawnArea.Top + (this.spawnArea.Height / 4), this.spawnArea.Bottom - (this.spawnArea.Height / 4)));

                    this.spawnVelocity = this.GetInitialVelocity(this.spawnArea);
                }

                // Spawn Enemies
                if (this.enemiesSpawned < currentWave.EnemyCount && this.waveTimeElapsed >= currentWave.SpawnInterval)
                {
                    this.enemyManager.SpawnEnemy(currentWave.EnemyType, this.spawnPosition, this.spawnVelocity, currentWave.Health, currentWave.BulletType, currentWave.MovementPattern, currentWave.ShootingPatterns);
                    this.waveTimeElapsed = 0f;
                    this.enemiesSpawned++;
                }

                // If all enemies have been spawned and the wave's timer is over, move to next wave
                if (this.enemiesSpawned == currentWave.EnemyCount && this.enemyManager.Enemies.Count == 0)
                {
                    this.waveTimeElapsed = currentWave.Duration + 1;
                }

                // Reset variables for next wave
                if (this.waveTimeElapsed >= currentWave.Duration)
                {
                    this.enemyManager.MoveEnemiesOffScreen(this.playArea);
                    this.currentWaveIndex++;
                    this.waveTimeElapsed = 0f;
                    this.enemiesSpawned = 0;
                }

                this.enemyManager.IsDead();
            }
        }

        /// <summary>
        /// WavesLeft returns a bool representing if there are Waves left to go through.
        /// </summary>
        /// <returns>True if there are still Waves to go through, false otherwise.</returns>
        public bool WavesLeft()
        {
            return this.waves.Count > 0;
        }

        /// <summary>
        /// GetInitialVelocity generates an initial velocity based on Enemy spawn location.
        /// </summary>
        /// <param name="spawnArea">spawnArea for Enemies.</param>
        /// <returns>Returns a Vector2 velocity for the Enemies.</returns>
        private Vector2 GetInitialVelocity(Rectangle spawnArea)
        {
            Random random = new Random();
            Vector2 velocity = Vector2.Zero;

            // Left spawn area
            if (spawnArea == this.playArea.SpawnAreaRectangles[0])
            {
                velocity.X = 2.5f;
                velocity.Y = (float)(random.NextDouble() * -0.75);
            }

            // Right spawn area
            else if (spawnArea == this.playArea.SpawnAreaRectangles[1])
            {
                velocity.X = -2.5f;
                velocity.Y = (float)(random.NextDouble() * -0.75);
            }

            return velocity;
        }
    }
}
