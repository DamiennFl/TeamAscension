using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascension.Business_Layer.Waves
{
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
        /// enemyManager
        /// </summary>
        private EnemyManager enemyManager;

        private PlayArea playArea;

        private BorderManager borderManager;

        private Rectangle spawnArea;

        private Vector2 spawnPosition;

        private Vector2 spawnVelocity;

        public WaveManager(List<Wave> waves, EnemyManager enemyManager, PlayArea playArea)
        {
            this.waves = waves;
            this.waveTimeElapsed = 0f;
            this.currentWaveIndex = 0;
            this.enemyManager = enemyManager;
            this.playArea = playArea;
            this.borderManager = new BorderManager(this.playArea);
        }

        public void Update(GameTime gameTime)

        {
            if (this.currentWaveIndex < this.waves.Count)
            {
                Wave currentWave = this.waves[this.currentWaveIndex];
                this.waveTimeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

                Random random = new Random();
                List<Rectangle> spawnAreas = this.playArea.SpawnAreaRectangles;
                this.spawnArea = spawnAreas[random.Next(spawnAreas.Count)];

                this.spawnPosition = new Vector2(
                    random.Next(this.spawnArea.Left + (this.spawnArea.Width / 4), this.spawnArea.Right - (this.spawnArea.Width / 4)),
                    random.Next(this.spawnArea.Top + (this.spawnArea.Height / 4), this.spawnArea.Bottom - (this.spawnArea.Height / 4)));

                this.spawnVelocity = this.GetInitialVelocity(this.spawnArea);

                for (int i = 0; i < currentWave.EnemyCount; i++)
                {
                    if (this.waveTimeElapsed >= currentWave.SpawnInterval)
                    {
                        // Spawn the enemy, reset the time elapsed, and increase the amount of spawned enemies.
                        this.enemyManager.SpawnEnemy();
                        this.waveTimeElapsed = 0f;
                    }
                }

                if (this.enemyManager.Enemies.Count == 0)
                {
                    this.waveTimeElapsed = currentWave.Duration + 1;
                }

                if (this.waveTimeElapsed >= currentWave.Duration)
                {
                    this.enemyManager.MoveEnemiesOffScreen(this.playArea);

                    this.currentWaveIndex++;
                    this.waveTimeElapsed = 0f;
                }

                this.enemyManager.IsDead();
            }
        }

        private Vector2 GetInitialVelocity(Rectangle spawnArea)
        {
            Random random = new Random();
            Vector2 velocity = Vector2.Zero;

            // Top Spawn area
            if (spawnArea == this.playArea.SpawnAreaRectangles[0])
            {
                velocity.X = (float)(random.NextDouble() - 0.25);
                velocity.Y = 2.5f;
            }

            // Left spawn area
            else if (spawnArea == this.playArea.SpawnAreaRectangles[1])
            {
                velocity.X = 2.5f;
                velocity.Y = (float)(random.NextDouble() * -0.75);
            }

            // Right spawn area
            else if (spawnArea == this.playArea.SpawnAreaRectangles[2])
            {
                velocity.X = -2.5f;
                velocity.Y = (float)(random.NextDouble() * -0.75);
            }

            return velocity;
        }
    }
}
