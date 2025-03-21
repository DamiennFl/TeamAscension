using Microsoft.Xna.Framework;
using Ascension.Enemies.EnemyMovement;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Ascension.Enemies
{
    internal class SwoopFormation : EnemyFormation
    {
        private float spawnDelay;
        private float timeSinceLastSpawn;
        private string enemyType;
        private int numEnemies;
        private int enemiesSpawned;
        private Vector2 enemyVelocity;
        private float enemySpacing;
        private EnemyFactory enemyFactory;
        private float deleteTime;

        public SwoopFormation(Vector2 startPosition, int numEnemies, float spawnDelay, Vector2 enemyVelocity, float enemySpacing, EnemyFactory factory, string enemyType)
    : base(startPosition)
        {
            // Set the start position to the middle of the game area
            this.FormationStartPosition = new Vector2(250, 500); // Middle of the game area horizontally, near the top vertically
            this.numEnemies = numEnemies;
            this.spawnDelay = spawnDelay;
            this.timeSinceLastSpawn = 0;
            this.enemiesSpawned = 0;
            this.enemyVelocity = enemyVelocity;
            this.enemySpacing = enemySpacing;
            this.enemyFactory = factory;
            this.enemyType = enemyType;
        }

        public override void Update(GameTime gameTime)
        {
            this.timeSinceLastSpawn += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (this.enemiesSpawned < this.numEnemies && this.timeSinceLastSpawn >= this.spawnDelay)
            {
                this.timeSinceLastSpawn = 0;
                Vector2 enemyPosition = new Vector2(100, 100); // Middle of the game area horizontally, near the top vertically
                float targetXPosition = enemyPosition.X + (this.enemiesSpawned * this.enemySpacing);
                Vector2 targetPosition = new Vector2(targetXPosition, 100);


                // Create a new enemy at the formation start position
                Enemy enemy = this.enemyFactory.CreateEnemy(this.FormationStartPosition, this.enemyType);

                // Assign the swoop movement pattern to the enemy
                enemy.AddMovementPattern(new SwoopMovementPattern(this.FormationStartPosition, 100f, 2f, true)); // Swoop to the right
                enemy.AddMovementPattern(new WaitPattern(2f)); // Wait for 1 second (optional)
                // enemy.AddMovementPattern(new SwoopMovementPattern(new Vector2(155, 710), 100f, 2f, false)); // Swoop to the left
                enemy.AddMovementPattern(new LinearMovementPattern(targetPosition, this.enemyVelocity)); // Move back to the start position
                enemy.AddMovementPattern(new WaitPattern(2f)); // Wait for 1 second (optional)
                enemy.AddMovementPattern(new LinearMovementPattern(new Vector2(0, 100), this.enemyVelocity)); // Move down

                // Add the enemy to the formation
                this.enemies.Add(enemy);

                this.enemiesSpawned++;
            }

            // Update all enemies in the formation
            foreach (var enemy in this.enemies)
            {
                enemy.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var enemy in this.enemies)
            {
                enemy.Draw(spriteBatch);
            }
        }
    }
}