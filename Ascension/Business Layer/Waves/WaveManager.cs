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

        public WaveManager(List<Wave> waves, EnemyManager enemyManager)
        {
            this.waves = waves;
            this.waveTimeElapsed = 0f;
            this.currentWaveIndex = 0;
            this.enemyManager = enemyManager;
        }

        public void Update(GameTime gameTime)
        {
            if (this.currentWaveIndex < this.waves.Count) {
                Wave currentWave = this.waves[currentWaveIndex];
                this.waveTimeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

                
            }
            //    Iterate through each Wave
            //    if (this.currentWaveIndex < this.waves.Count)
            //    {
            //        Get the currentWave
            //       Wave currentWave = this.waves[this.currentWaveIndex];
            //        Increase the timeElapsed
            //        this.waveTimeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            //        If no enemies have been spawned for this wave, select a spawn area,
            //        a position within the area, and an initial velocity based on the spawn position.
            //        if (this.enemiesSpawned == 0)
            //            {
            //                Random random = new Random();
            //                List<Rectangle> spawnAreas = this.playArea.SpawnAreaRectangles;
            //                this.spawnArea = spawnAreas[random.Next(spawnAreas.Count)];

            //                this.spawnPosition = new Vector2(
            //                    random.Next(this.spawnArea.Left + (this.spawnArea.Width / 4), this.spawnArea.Right - (this.spawnArea.Width / 4)),
            //                    random.Next(this.spawnArea.Top + (this.spawnArea.Height / 4), this.spawnArea.Bottom - (this.spawnArea.Height / 4)));

            //                this.spawnVelocity = this.GetInitialVelocity(this.spawnArea);
            //            }

            //        If it is time to spawn an enemy:
            //        if (this.enemiesSpawned < currentWave.EnemyCount && this.waveTimeElapsed >= currentWave.SpawnInterval)
            //        {
            //            Spawn the enemy, reset the time elapsed, and increase the amount of spawned enemies.
            //            this.SpawnEnemy(currentWave, this.spawnPosition, this.spawnVelocity);
            //            this.waveTimeElapsed = 0f;
            //            this.enemiesSpawned++;
            //        }

            //        If all enemies have been spawned for the Wave, and they are all dead,
            //        move onto the next Wave.
            //        if (this.enemiesSpawned == currentWave.EnemyCount && this.Enemies.Count == 0)
            //            {
            //                this.waveTimeElapsed = currentWave.Duration + 1;
            //            }

            //        Move enemies offscreen if the Wave is done.
            //        if (this.waveTimeElapsed >= currentWave.Duration)
            //        {
            //            foreach (Enemy enemy in this.Enemies)
            //            {
            //                SetOffScreenVelocity(enemy, this.playArea);
            //                enemy.MovementPattern = this.movementFactory.CreateMovementPattern("GoOffScreen");
            //            }

            //            Reset the Wave specific variables
            //            this.currentWaveIndex++;
            //            this.enemiesSpawned = 0;
            //            this.waveTimeElapsed = 0f;
            //        }
            //    }

            //    Delete dead enemies
            //    this.IsDead();

            //    Update all enemies
            //    foreach (var enemy in this.Enemies)
            //    {
            //        enemy.Update(gameTime);
            //        If enemies hit the border, they reverse their direction.
            //        this.borderManager.CheckAndReverseVelocity(enemy);
            //    }
            //}
        }
    }
}
