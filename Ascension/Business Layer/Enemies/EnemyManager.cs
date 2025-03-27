using System;
using System.Collections.Generic;
using Ascension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Ascension;
using Ascension.Business_Layer;

internal class EnemyManager
{
    private EnemyFactory factory;
    private MovementFactory movementFactory;

    public List<Enemy> Enemies { get; }

    private List<Wave> waves;

    private float waveDuration;
    private int enemiesSpawned;
    private int currentWaveIndex;

    private BulletManager bulletManager;

    private CollisionManager collisionManager;

    private BorderManager borderManager;

    private PlayArea playArea;

    private Rectangle selectedSpawnArea; // Field to store the selected spawn area for the current wave
    private Vector2 spawnPosition; // Field to store the spawn position for the current wave
    private Vector2 spawnVelocity;

    public EnemyManager(ContentManager contentManager, GraphicsDevice graphicsDevice, CollisionManager collisionManager, BulletManager bulletManager, List<Wave> waves, PlayArea playArea)
    {
        this.factory = new ConcreteEnemyFactory(contentManager, graphicsDevice);
        this.movementFactory = new MovementFactory();
        this.Enemies = new List<Enemy>();
        this.waves = waves;
        this.waveDuration = 0f;
        this.enemiesSpawned = 0;
        this.currentWaveIndex = 0;
        this.bulletManager = bulletManager;
        this.collisionManager = collisionManager;
        this.playArea = playArea;
        this.borderManager = new BorderManager(this.playArea);
    }

    public void Update(GameTime gameTime)
    {
        if (this.currentWaveIndex < this.waves.Count)
        {
            Wave currentWave = this.waves[this.currentWaveIndex];
            this.waveDuration += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (this.enemiesSpawned == 0) // Select the spawn area, position, and velocity once per wave
            {
                Random random = new Random();
                List<Rectangle> spawnAreas = this.playArea.SpawnAreaRectangles;
                this.selectedSpawnArea = spawnAreas[random.Next(spawnAreas.Count)];

                this.spawnPosition = new Vector2(
                    random.Next(this.selectedSpawnArea.Left + (this.selectedSpawnArea.Width / 4), this.selectedSpawnArea.Right - (this.selectedSpawnArea.Width / 4)),
                    random.Next(this.selectedSpawnArea.Top + (this.selectedSpawnArea.Height / 4), this.selectedSpawnArea.Bottom - (this.selectedSpawnArea.Height / 4)));

                this.spawnVelocity = this.GetInitialVelocity(this.selectedSpawnArea);
            }

            if (this.enemiesSpawned < currentWave.EnemyCount && this.waveDuration >= currentWave.SpawnInterval)
            {
                this.SpawnEnemy(currentWave, this.spawnPosition, this.spawnVelocity);
                this.waveDuration = 0f;
                this.enemiesSpawned++;
            }

            if (this.enemiesSpawned == currentWave.EnemyCount && this.Enemies.Count == 0)
            {
                this.waveDuration = currentWave.Duration + 1;
            }

            // Move to the next wave if the current wave duration has passed
            if (this.waveDuration >= currentWave.Duration)
            {
                foreach (Enemy enemy in this.Enemies)
                {
                    enemy.MovementPattern = this.movementFactory.CreateMovementPattern("GoOffScreen");
                }

                this.currentWaveIndex++;
                this.enemiesSpawned = 0;
                this.waveDuration = 0f;
            }
        }

        this.IsDead();

        // Update all enemies
        foreach (var enemy in this.Enemies)
        {
            enemy.Update(gameTime);
            this.borderManager.CheckAndReverseVelocity(enemy);
        }
    }

    private void SpawnEnemy(Wave wave, Vector2 position, Vector2 velocity)
    {
        Enemy enemy = wave.EnemyType switch
        {
            "EnemyA" => this.factory.CreateEnemyA(position, velocity, wave.Health, wave.BulletType),
            "EnemyB" => this.factory.CreateEnemyB(position, velocity, wave.Health, wave.BulletType),
            "MidBoss" => this.factory.CreateMidBoss(position, velocity, wave.Health, wave.BulletType),
            "FinalBoss" => this.factory.CreateFinalBoss(position, velocity, wave.Health, wave.BulletType),
            _ => throw new ArgumentException("Unknown enemy type inputted")
        };

        IMovementPattern movementPattern = this.movementFactory.CreateMovementPattern(wave.MovementPattern);
        enemy.MovementPattern = movementPattern;
        this.Enemies.Add(enemy);

        this.bulletManager.RegisterEnemy(enemy);
        this.collisionManager.Register(enemy);
    }

    private void IsDead()
    {
        var enemies = this.Enemies.ToArray();
        foreach (var enemy in enemies)
        {
            if (enemy.IsDead)
            {
                this.Enemies.Remove(enemy);
                this.collisionManager.Unregister(enemy);
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var enemy in this.Enemies)
        {
            enemy.Draw(spriteBatch);
            enemy.DrawBounds(spriteBatch);
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