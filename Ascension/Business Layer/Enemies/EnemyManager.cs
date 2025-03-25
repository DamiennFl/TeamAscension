using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Ascension
{
    internal class EnemyManager
    {
        private EnemyFactory factory;
        private MovementFactory movementFactory;

        public List<Enemy> Enemies { get; }

        private List<Wave> waves;

        private float timeSinceLastSpawn;
        private int currentWaveIndex;
        private int enemiesSpawned;

        private BulletManager bulletManager;

        private CollisionManager collisionManager;

        public EnemyManager(ContentManager contentManager, GraphicsDevice graphicsDevice, CollisionManager collisionManager, BulletManager bulletManager, List<Wave> waves)
        {
            this.factory = new ConcreteEnemyFactory(contentManager, graphicsDevice, collisionManager);
            this.movementFactory = new MovementFactory();
            this.Enemies = new List<Enemy>();
            this.waves = waves;
            this.timeSinceLastSpawn = 0f;
            this.currentWaveIndex = 0;
            this.enemiesSpawned = 0;
            this.bulletManager = bulletManager;
            this.collisionManager = collisionManager;
        }

        public void SpawnEnemy(Wave wave)
        {
            Vector2 position = new Vector2(100, 100);
            Vector2 velocity = new Vector2(2, 1);

            Enemy enemy = wave.EnemyType switch
            {
                "EnemyA" => this.factory.CreateEnemyA(position, velocity),
                "EnemyB" => this.factory.CreateEnemyB(position, velocity),
                "MidBoss" => this.factory.CreateMidBoss(position, velocity),
                "FinalBoss" => this.factory.CreateFinalBoss(position, velocity),
                _ => throw new ArgumentException("Unknown enemy type inputted")
            };

            IMovementPattern movementPattern = this.movementFactory.CreateMovementPattern(wave.MovementPattern, wave.Duration);
            enemy.MovementPattern = movementPattern;
            this.Enemies.Add(enemy);

            this.bulletManager.RegisterEnemy(enemy);
            this.collisionManager.Register(enemy);
        }

        public void Update(GameTime gameTime)
        {
            // Process the current wave
            if (this.currentWaveIndex < this.waves.Count)
            {
                Wave currentWave = this.waves[this.currentWaveIndex];
                this.timeSinceLastSpawn += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (this.enemiesSpawned < currentWave.EnemyCount && this.timeSinceLastSpawn >= currentWave.SpawnInterval)
                {
                    this.SpawnEnemy(currentWave);
                    this.timeSinceLastSpawn = 0f;
                    this.enemiesSpawned++;
                }

                // Move to the next wave if the current wave duration has passed
                if (this.timeSinceLastSpawn >= currentWave.Duration)
                {
                    this.currentWaveIndex++;
                    this.enemiesSpawned = 0;
                    this.timeSinceLastSpawn = 0f;
                }
            }

            // Update all enemies
            foreach (var enemy in this.Enemies)
            {
                enemy.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var item in this.Enemies)
            {
                item.Draw(spriteBatch);
            }
        }
    }
}
