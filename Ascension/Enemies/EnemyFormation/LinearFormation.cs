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
        private System.Random random = new System.Random();

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
                        this.MidBossFormation();
                        break;
                    case "EnemyB":
                        this.EnemyBFormation();
                        break;
                    case "FinalBoss":
                        this.FinalBossFormation();
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

        private void FinalBossFormation()
        {

            // Calculate enemy start position
            float enemyXPosition = this.FormationStartPosition.X + (this.enemiesSpawned * this.enemySpacing);
            Vector2 enemyPosition = new Vector2(enemyXPosition, this.FormationStartPosition.Y);
            Enemy newEnemy = this.enemyFactory.CreateEnemy(enemyPosition, this.enemyType);

            // Define positions
            Vector2 middleScreenPosition = new Vector2(400, 300); // Adjust as per your screen resolution
            Vector2 offScreenPosition = new Vector2(enemyPosition.X, -100); // Adjust Y to move off-screen

            // Add movement patterns
            newEnemy.AddMovementPattern(new SwoopMovementPattern(this.FormationStartPosition, 100f, 15f, true)); // Swoop for 15 seconds
            newEnemy.AddMovementPattern(new WaitPattern(5f)); // Sit at the middle of the screen for 15 seconds

            // Add sporadic movement patterns
            float totalRandomMovementTime = 15f;
            float movementDuration = 2f; // Duration for each random movement segment
            int numberOfMovements = (int)(totalRandomMovementTime / movementDuration);

            for (int i = 0; i < numberOfMovements; i++)
            {
                Vector2 randomTarget = GetRandomPosition();
                newEnemy.AddMovementPattern(new LinearMovementPattern(randomTarget, this.enemyVelocity));
                newEnemy.AddMovementPattern(new WaitPattern(0.5f)); // Optional wait between movements
            }

            // Add final movement pattern to move off the screen
            newEnemy.AddMovementPattern(new LinearMovementPattern(offScreenPosition, this.enemyVelocity)); // Move off the screen

            this.enemies.Add(newEnemy);
            this.enemiesSpawned++;
            this.timeSinceLastSpawn = 0;
        }
    
    private void MidBossFormation()
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
            newEnemy.AddMovementPattern(new WaitPattern(15f)); // Wait for 3 seconds
            newEnemy.AddMovementPattern(new LinearMovementPattern(new Vector2(0, 100), this.enemyVelocity)); // Move back to start position

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

        /// <summary>
        /// Formations for ebemy B.
        /// </summary>
        private void EnemyBFormation()
        {
            // Calculate enemy start position
            float enemyXPosition = this.FormationStartPosition.X + (this.enemiesSpawned * this.enemySpacing);
            Vector2 enemyPosition = new Vector2(enemyXPosition, this.FormationStartPosition.Y);
            Enemy newEnemy = this.enemyFactory.CreateEnemy(enemyPosition, this.enemyType);

            // Assign random movement patterns for 10 seconds
            float totalRandomMovementTime = 10f;
            float movementDuration = 2f; // Duration for each random movement segment
            int numberOfMovements = (int)(totalRandomMovementTime / movementDuration);

            for (int i = 0; i < numberOfMovements; i++)
            {
                Vector2 randomTarget = GetRandomPosition();
                newEnemy.AddMovementPattern(new LinearMovementPattern(randomTarget, this.enemyVelocity));
                newEnemy.AddMovementPattern(new WaitPattern(0.5f)); // Optional wait between movements
            }

            // Add final movement pattern to move off the screen
            Vector2 offScreenPosition = new Vector2(enemyPosition.X, -100); // Adjust Y to move off-screen
            newEnemy.AddMovementPattern(new LinearMovementPattern(offScreenPosition, this.enemyVelocity));

            this.enemies.Add(newEnemy);
            this.enemiesSpawned++;
            this.timeSinceLastSpawn = 0;
        }

        /// <summary>
        /// Gets a random position on the screen.
        /// </summary>
        /// <returns>a vector.</returns>
        private Vector2 GetRandomPosition()
        {
            // Define screen boundaries (adjust as per your game resolution)
            float screenWidth = 800f;
            float screenHeight = 600f;

            float randomX = (float)this.random.NextDouble() * screenWidth;
            float randomY = (float)this.random.NextDouble() * screenHeight;

            return new Vector2(randomX, randomY);
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