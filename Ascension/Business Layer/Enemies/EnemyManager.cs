using Ascension.Enemies;
using System;
using System.Collections.Generic;
using Ascension.Waves;
using Microsoft.Xna.Framework;
using Ascension.Business_Layer.Movement;
using Ascension.Enemies.EnemyMovement;

namespace Ascension.Business_Layer.Enemies
{
    internal class EnemyManager
    {
        private EnemyFactory factory;
        private MovementFactory movementFactory;

        public List<Enemy> Enemies { get; }

        private List<Wave> waves;

        // Stuff passed in from Wave
        // EnemyManager manager = new EnemyManager()
        public EnemyManager(EnemyFactory factory)
        {
            this.factory = factory;
            this.movementFactory = new MovementFactory();
            this.Enemies = new List<Enemy>();
        }

        // Spawn enemies for a duration, at an interval.
        private void SpawnEnemy(Wave wave)
        {
            for (int i = 0; i < wave.EnemyCount; i++)
            {
                Vector2 position = new Vector2(100, 100);
                Vector2 velocity = new Vector2(1, 1);

                Enemy enemy = wave.EnemyType switch
                {
                    "EnemyA" => this.factory.CreateEnemyA(position, velocity),
                    "EnemyB" => this.factory.CreateEnemyB(position, velocity),
                    "MidBoss" => this.factory.CreateMidBoss(position, velocity),
                    "FinalBoss" => this.factory.CreateFinalBoss(position, velocity),
                    _ => throw new ArgumentException("Unknown enemy type")
                };

                IMovementPattern movementPattern = this.movementFactory.CreateMovementPattern(wave.MovementPattern, wave.Duration);
                enemy.MovementPattern = movementPattern;
                this.Enemies.Add(enemy);
            }
        }

        // Remove enemies from this list
        private void KillEnemies()
        {

        }


        public void ProcessWaves(GameTime gameTime, List<Wave> waves)
        {
            // Main Wave Manager
            foreach (Wave wave in waves)
            {
                float timeElapsed = 0;
                float timeSinceLastSpawn = 0;
                int enemiesSpawned = 0;
                while (timeElapsed < wave.Duration)
                {
                    if (enemiesSpawned < wave.EnemyCount && timeSinceLastSpawn >= wave.SpawnInterval)
                    {
                        this.SpawnEnemy(wave);
                    }

                    timeSinceLastSpawn += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

                    MoveEnemyOffScreen(gameTime);
                }
            }
        }

        private void MoveEnemyOffScreen(GameTime gameTime)
        {
            // Move enemy to closest border with some randomization
            // Check off-screen position, if they are then remove from the list
        }

        public void Update(GameTime gameTime)
        {
            foreach (var enemy in this.Enemies)
            {
                enemy.Update(gameTime);
            }
        }

    }
}
