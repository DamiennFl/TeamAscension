using System;
using System.Collections.Generic;
using Ascension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Ascension.Business_Layer;
using Ascension.Business_Layer.Shooting;

public class EnemyManager
{   
    /// <summary>
    /// EnemyFactory to create enemies.
    /// </summary>
    private EnemyFactory factory;

    /// <summary>
    /// MovementFactory to create movement patterns.
    /// </summary>
    private MovementFactory movementFactory;

    /// <summary>
    /// ShootingPatternFactory to create shooting patterns.
    /// </summary>
    private ShootingPatternFactory shootingPatternFactory;

    /// <summary>
    /// Gets the list of Enemies for this EnemyManager.
    /// </summary>
    public List<Enemy> Enemies { get; }

    /// <summary>
    /// A count of the enemies spawned.
    /// </summary>
    private int enemiesSpawned;

    /// <summary>
    /// A BulletManager to register enemies to the OnBulletFired event.
    /// </summary>
    private BulletManager bulletManager;

    /// <summary>
    /// A CollisionManager to register spawned enemies as Collidable.
    /// </summary>
    private CollisionManager collisionManager;

    /// <summary>
    /// A BorderManager to track and manage the border/areas of the Game.
    /// </summary>
    private BorderManager borderManager;

    /// <summary>
    /// The PlayArea for the Game, derived from State.
    /// </summary>
    private PlayArea playArea;

    /// <summary>
    /// The spawnArea for the Enemies in a Wave.
    /// </summary>
    private Rectangle spawnArea;

    /// <summary>
    /// The spawnPosition for the Enemies in a Wave.
    /// </summary>
    private Vector2 spawnPosition;

    /// <summary>
    /// The spawnVelocity for Enemies in a Wave.
    /// </summary>
    private Vector2 spawnVelocity;

    /// <summary>
    /// Initializes a new instance of the <see cref="EnemyManager"/> class.
    /// </summary>
    /// <param name="contentManager">The contentManager of the Game.</param>
    /// <param name="graphicsDevice">The graphicsDevice of the Game.</param>
    /// <param name="collisionManager">A CollisionManager reference to register Enemies.</param>
    /// <param name="bulletManager">A BulletManager reference for Enemies to Shoot.</param>
    /// <param name="waves">A list of Waves to interpret.</param>
    /// <param name="playArea">The playArea of the Game.</param>
    public EnemyManager(ContentManager contentManager, GraphicsDevice graphicsDevice, CollisionManager collisionManager, BulletManager bulletManager, PlayArea playArea)
    {
        this.factory = new ConcreteEnemyFactory(contentManager, graphicsDevice);
        this.movementFactory = new MovementFactory();
        this.shootingPatternFactory = new ShootingPatternFactory();
        this.Enemies = new List<Enemy>();
        this.enemiesSpawned = 0;
        this.bulletManager = bulletManager;
        this.collisionManager = collisionManager;
        this.playArea = playArea;
        this.borderManager = new BorderManager(this.playArea);
    }

    /// <summary>
    /// The Update method updates everything related to the Enemy subsystem,
    /// such as spawning, movement, and despawning.
    /// </summary>
    /// <param name="gameTime">The gameTime to stay synchronized with the Game.</param>
    public void Update(GameTime gameTime)
    {
        // Iterate through each Wave
        if (this.currentWaveIndex < this.Waves.Count)
        {
            // Get the currentWave
            Wave currentWave = this.Waves[this.currentWaveIndex];
            // Increase the timeElapsed
            this.waveTimeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // If no enemies have been spawned for this wave, select a spawn area,
            // a position within the area, and an initial velocity based on the spawn position.
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

            // If it is time to spawn an enemy:
            if (this.enemiesSpawned < currentWave.EnemyCount && this.waveTimeElapsed >= currentWave.SpawnInterval)
            {
                // Spawn the enemy, reset the time elapsed, and increase the amount of spawned enemies.
                this.SpawnEnemy(currentWave, this.spawnPosition, this.spawnVelocity);
                this.waveTimeElapsed = 0f;
                this.enemiesSpawned++;
            }

            // If all enemies have been spawned for the Wave, and they are all dead,
            // move onto the next Wave.
            if (this.enemiesSpawned == currentWave.EnemyCount && this.Enemies.Count == 0)
            {
                this.waveTimeElapsed = currentWave.Duration + 1;
            }

            // Move enemies offscreen if the Wave is done.
            if (this.waveTimeElapsed >= currentWave.Duration)
            {
                foreach (Enemy enemy in this.Enemies)
                {
                    SetOffScreenVelocity(enemy, this.playArea);
                    enemy.MovementPattern = this.movementFactory.CreateMovementPattern("GoOffScreen");
                }

                // Reset the Wave specific variables
                this.currentWaveIndex++;
                this.enemiesSpawned = 0;
                this.waveTimeElapsed = 0f;
            }
        }

        // Delete dead enemies
        this.IsDead();

        // Update all enemies
        foreach (var enemy in this.Enemies)
        {
            enemy.Update(gameTime);
            // If enemies hit the border, they reverse their direction.
            this.borderManager.CheckAndReverseVelocity(enemy);
        }
    }

    /// <summary>
    /// SpawnEnemy spawns enemies based on a Wave's info, a position, and a velocity.
    /// </summary>
    /// <param name="wave">A Wave to get the information about what to spawn.</param>
    /// <param name="position">The position to spawn the Enemy at.</param>
    /// <param name="velocity">The velocity for the Enemy.</param>
    /// <exception cref="ArgumentException">Throws an Exception if an invalid enemy type is inputted.</exception>
    private void SpawnEnemy(Wave wave, Vector2 position, Vector2 velocity)
    {
        // Spawn Enemy
        Enemy enemy = wave.EnemyType switch
        {
            "EnemyA" => this.factory.CreateEnemyA(position, velocity, wave.Health, wave.BulletType),
            "EnemyB" => this.factory.CreateEnemyB(position, velocity, wave.Health, wave.BulletType),
            "MidBoss" => this.factory.CreateMidBoss(position, velocity, wave.Health, wave.BulletType),
            "FinalBoss" => this.factory.CreateFinalBoss(position, velocity, wave.Health, wave.BulletType),
            _ => throw new ArgumentException("Unknown enemy type inputted")
        };

        // Apply movement
        IMovementPattern movementPattern = this.movementFactory.CreateMovementPattern(wave.MovementPattern);
        enemy.MovementPattern = movementPattern;

        // Apply shooting pattern
        // IShootingPattern shootingPattern = this.shootingPatternFactory.CreateShootingPattern(wave.ShootingPattern);
        enemy.ShootingPattern = new StandardShootingPattern(); // CHANGE THIS WITH THE WAVE BUILDER SEEN ABOVE ^^

        // Add to list of enemies
        this.Enemies.Add(enemy);

        // Register collisions and bullet shoot event for Enemy
        this.bulletManager.RegisterEnemy(enemy);
        this.collisionManager.Register(enemy);
    }

    /// <summary>
    /// Checks every Enemy in the list to see if it is dead, and removes it if so.
    /// </summary>
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

    /// <summary>
    /// Draw method for the Enemies.
    /// </summary>
    /// <param name="spriteBatch">spriteBatch to draw into.</param>
    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var enemy in this.Enemies)
        {
            enemy.Draw(spriteBatch);
            enemy.DrawBounds(spriteBatch);
        }
    }

    /// <summary>
    /// Calculate the Initial velocity of the Enemy.
    /// </summary>
    /// <param name="spawnArea">spawnArea that the Enemy is spawning in.</param>
    /// <returns>A Vector2 representing the Velocity.</returns>
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

    /// <summary>
    /// SetOffScreenVelocity sets the Enemy's velocity based on the closest
    /// border once the Wave is over.
    /// </summary>
    /// <param name="enemy">The Enemy to apply the new velocity to.</param>
    /// <param name="playArea">The playArea.</param>
    private void SetOffScreenVelocity(Enemy enemy, PlayArea playArea)
    {
        Vector2 velocity = Vector2.Zero;

        // Find the nearest left, right, or top border based on the spawnArea
        float leftDistance = Math.Abs(enemy.Position.X - playArea.BorderRectangle.Left);
        float rightDistance = Math.Abs(enemy.Position.X - playArea.BorderRectangle.Right);
        float topDistance = Math.Abs(enemy.Position.Y - playArea.BorderRectangle.Top);

        float minDistance = Math.Min(leftDistance, Math.Min(rightDistance, topDistance));

        // Set the velocity based on closest border
        if (minDistance == leftDistance)
        {
            velocity.X = -2.5f;
            velocity.Y = (float)(-2.5f + (5 * new Random().NextDouble()));
        }
        else if (minDistance == rightDistance)
        {
            velocity.X = 2.5f;
            velocity.Y = (float)(-2.5f + (5 * new Random().NextDouble()));
        }
        else if (minDistance == topDistance)
        {
            velocity.X = (float)(-2.5f + (5 * new Random().NextDouble()));
            velocity.Y = -2.5f;
        }

        // Update velocity
        enemy.Velocity = velocity;
    }
}