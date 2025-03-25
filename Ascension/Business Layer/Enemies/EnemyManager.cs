﻿using System;
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

        private PlayArea playArea;

        public EnemyManager(ContentManager contentManager, GraphicsDevice graphicsDevice, CollisionManager collisionManager, BulletManager bulletManager, List<Wave> waves, PlayArea playArea)
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
            this.playArea = playArea;
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
                    // TODO
                    //if (currentWave.IsBossWave)
                    //{
                    //    this.bulletManager.ClearScreen();
                    //}

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
                CheckAndReverseVelocity(enemy);
            }
        }

        private void SpawnEnemy(Wave wave)
        {
            //Vector2 position = this.GetRandomSpawnPosition();
            //Vector2 velocity = this.GetInitialVelocity();
            Vector2 position = new Vector2(100, 100);
            Vector2 velocity = new Vector2(1, 1);

            Enemy enemy = wave.EnemyType switch
            {
                "EnemyA" => this.factory.CreateEnemyA(position, velocity),
                "EnemyB" => this.factory.CreateEnemyB(position, velocity),
                "MidBoss" => this.factory.CreateMidBoss(position, velocity),
                "FinalBoss" => this.factory.CreateFinalBoss(position, velocity),
                _ => throw new ArgumentException("Unknown enemy type inputted")
            };

            IMovementPattern movementPattern = this.movementFactory.CreateMovementPattern(wave.MovementPattern);
            enemy.MovementPattern = movementPattern;
            this.Enemies.Add(enemy);

            this.bulletManager.RegisterEnemy(enemy);
            this.collisionManager.Register(enemy);
        }

        private void CheckAndReverseVelocity(Enemy enemy)
        {
            Rectangle border = this.playArea.BorderRectangle;
            Vector2 position = enemy.Position;
            Vector2 velocity = enemy.Velocity;
            Rectangle bounds = enemy.Bounds;

            int topHalfHeight = border.Height / 2;
            float sineOffset = 0f;
            if (enemy.MovementPattern is WaveMovementPattern)
            {
                sineOffset = 25f;
            }

            if (position.X <= border.Left + sineOffset + (bounds.Width / 2) || position.X >= border.Right - sineOffset - (bounds.Width / 2))
            {
                velocity.X = -velocity.X;
            }

            if (position.Y <= border.Top + sineOffset + (bounds.Height / 2) || position.Y >= border.Top + topHalfHeight - (bounds.Height / 2))
            {
                velocity.Y = -velocity.Y;
            }

            enemy.Velocity = velocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var enemy in this.Enemies)
            {
                enemy.Draw(spriteBatch);
                enemy.DrawBounds(spriteBatch);
            }
        }

        //private Vector2 GetRandomSpawnPosition()
        //{
        //    Random random = new Random();
        //}

        //private Vector2 GetInitialVelocity(Vector2 spawnPosition)
        //{

        //}
    }
}
