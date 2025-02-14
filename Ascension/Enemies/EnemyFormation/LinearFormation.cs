using Microsoft.Xna.Framework;
using Ascension.Enemies.EnemyFormation;
using Ascension.Enemies.EnemyMovement;
using Microsoft.Xna.Framework.Graphics;

namespace Ascension.Enemies.EnemyFormation
{
    internal class LinearFormation : EnemyFormation
    {
        private float spawnDelay;
        private float timeSinceLastSpawn;
        private string enemyType;
        private int numEnemies;
        private int enemiesSpawned;
        private Vector2 enemyVelocity;
        private float enemySpacing;
        private Vector2 endPosition;
        private EnemyFactory enemyFactory;
        private float deleteTime;

        public LinearFormation(Vector2 startPosition, Vector2 endPosition, int numEnemies, float spawnDelay, Vector2 enemyVelocity, float enemySpacing, EnemyFactory factory, string enemyType)
          : base(startPosition)
        {
            this.FormationStartPosition = startPosition;
            this.endPosition = endPosition;
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
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.deleteTime += elapsedTime; // Accumulate elapsed time

            if (this.enemiesSpawned < this.numEnemies && this.timeSinceLastSpawn >= this.spawnDelay)
            {
                //// Calculate enemy start and target pos
                //float enemyXPosition = this.FormationStartPosition.X + (this.enemiesSpawned * this.enemySpacing);
                //float targetXPosition = this.endPosition.X + (this.enemiesSpawned * this.enemySpacing);

                //// Calculate position for enemies to travel to
                //Vector2 enemyPosition = new Vector2(enemyXPosition, this.FormationStartPosition.Y);
                //Vector2 targetPosition = new Vector2(targetXPosition, this.endPosition.Y);
                //Vector2 bottomScreenPosition = new Vector2(enemyXPosition, 250);
                //Enemy newEnemy = this.enemyFactory.CreateEnemy(enemyPosition, this.enemyType);

                //// Add movement patterns
                //newEnemy.AddMovementPattern(new LinearMovementPattern(targetPosition, this.enemyVelocity)); // Moves to X target position
                //newEnemy.AddMovementPattern(new WaitPattern(3f)); // Wait for 3 seconds
                //newEnemy.AddMovementPattern(new LinearMovementPattern(enemyPosition, this.enemyVelocity)); // Moves back to start position
                //newEnemy.AddMovementPattern(new WaitPattern(1f)); // Wait for 9 seconds

                //// ALL WHAT I ADDED CAN BE DELTED FOR THE DEMO OR WHATEVER
                //newEnemy.AddMovementPattern(new LinearMovementPattern(bottomScreenPosition, this.enemyVelocity)); // Move down
                //newEnemy.AddMovementPattern(new WaitPattern(3f)); // Wait for 3 seconds
                //newEnemy.AddMovementPattern(new LinearMovementPattern(new Vector2(0, 100), this.enemyVelocity)); // Move back to start position

                //this.enemies.Add(newEnemy);
                //this.enemiesSpawned++;
                //this.timeSinceLastSpawn = 0;

                switch (this.enemyType)
                {
                    case "EnemyA":
                        this.NormalEnemyFormation();
                        break;
                    case "MidBoss":
                        this.BossFormation();
                        break;
                }
            }

            // This will eventually be used when a certain enemies health reaches zero, we will delete a certain enemy
            //if (this.deleteTime >= 5f)
            //{
            //    this.enemies.Clear();
            //    this.deleteTime = 0; // Reset deleteTime after clearing enemies
            //}

            foreach (var enemy in this.enemies)
            {
                enemy.Update(gameTime);
            }
        }

        private void BossFormation()
        {
            // Calculate enemy start and target pos
            float enemyXPosition = this.FormationStartPosition.X + (this.enemiesSpawned * this.enemySpacing);
            float targetXPosition = this.endPosition.X + (this.enemiesSpawned * this.enemySpacing);
            // Calculate position for enemies to travel to
            Vector2 enemyPosition = new Vector2(enemyXPosition, this.FormationStartPosition.Y);
            Vector2 targetPosition = new Vector2(targetXPosition, this.endPosition.Y);
            Vector2 bottomScreenPosition = new Vector2(enemyXPosition + 150, 250);
            Enemy newEnemy = this.enemyFactory.CreateEnemy(enemyPosition, this.enemyType);
            // Add movement patterns
            newEnemy.AddMovementPattern(new LinearMovementPattern(targetPosition, this.enemyVelocity)); // Moves to X target position
            newEnemy.AddMovementPattern(new WaitPattern(3f)); // Wait for 3 seconds
            newEnemy.AddMovementPattern(new LinearMovementPattern(enemyPosition, this.enemyVelocity)); // Moves back to start position
            newEnemy.AddMovementPattern(new WaitPattern(1f)); // Wait for 9 seconds
            // ALL WHAT I ADDED CAN BE DELTED FOR THE DEMO OR WHATEVER
            newEnemy.AddMovementPattern(new LinearMovementPattern(bottomScreenPosition, this.enemyVelocity)); // Move down

            this.enemies.Add(newEnemy);
            this.enemiesSpawned++;
            this.timeSinceLastSpawn = 0;
        }

        /// <summary>
        /// Formation for normal Enemy.
        /// </summary>
        private void NormalEnemyFormation()
        {
            // Calculate enemy start and target pos
            float enemyXPosition = this.FormationStartPosition.X + (this.enemiesSpawned * this.enemySpacing);
            float targetXPosition = this.endPosition.X + (this.enemiesSpawned * this.enemySpacing);

            // Calculate position for enemies to travel to
            Vector2 enemyPosition = new Vector2(enemyXPosition, this.FormationStartPosition.Y);
            Vector2 targetPosition = new Vector2(targetXPosition, this.endPosition.Y);
            Vector2 bottomScreenPosition = new Vector2(enemyXPosition, 250);
            Enemy newEnemy = this.enemyFactory.CreateEnemy(enemyPosition, this.enemyType);

            // Add movement patterns
            newEnemy.AddMovementPattern(new LinearMovementPattern(targetPosition, this.enemyVelocity)); // Moves to X target position
            newEnemy.AddMovementPattern(new WaitPattern(3f)); // Wait for 3 seconds
            newEnemy.AddMovementPattern(new LinearMovementPattern(enemyPosition, this.enemyVelocity)); // Moves back to start position
            newEnemy.AddMovementPattern(new WaitPattern(1f)); // Wait for 9 seconds

            // ALL WHAT I ADDED CAN BE DELTED FOR THE DEMO OR WHATEVER
            newEnemy.AddMovementPattern(new LinearMovementPattern(bottomScreenPosition, this.enemyVelocity)); // Move down
            newEnemy.AddMovementPattern(new WaitPattern(3f)); // Wait for 3 seconds
            newEnemy.AddMovementPattern(new LinearMovementPattern(new Vector2(0, 100), this.enemyVelocity)); // Move back to start position

            this.enemies.Add(newEnemy);
            this.enemiesSpawned++;
            this.timeSinceLastSpawn = 0;
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