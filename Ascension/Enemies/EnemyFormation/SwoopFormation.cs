using Microsoft.Xna.Framework;
using Ascension.Enemies.EnemyMovement;
using Microsoft.Xna.Framework.Graphics;

namespace Ascension.Enemies.EnemyFormation
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

                // Create a new enemy at the formation start position
                Enemy enemy = this.enemyFactory.CreateEnemy(this.FormationStartPosition, this.enemyType);

                // Assign the swoop movement pattern to the enemy
                enemy.AddMovementPattern(new SwoopMovementPattern(this.FormationStartPosition, 100f, 3f, true)); // Swoop to the right

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