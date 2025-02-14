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

        public LinearFormation(Vector2 startPosition, Vector2? endPosition, int numEnemies, float spawnDelay, Vector2 enemyVelocity, float enemySpacing, EnemyFactory factory, string enemyType)
          : base(startPosition)
        {
            this.FormationStartPosition = startPosition;
            if (endPosition.HasValue)
            {
                this.endPosition = endPosition.Value;
            }
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
                float xPosition = this.FormationStartPosition.X + (this.enemiesSpawned * this.enemySpacing);
                Vector2 enemyPosition = new Vector2(xPosition, this.FormationStartPosition.Y);
                Enemy newEnemy = this.enemyFactory.CreateEnemy(enemyPosition, this.enemyType);

                newEnemy.AddMovementPattern(new MoveToPositionPattern(new Vector2(xPosition, 100), this.enemyVelocity));
                newEnemy.AddMovementPattern(new WaitPattern(3f)); // Wait for 3 seconds
                newEnemy.AddMovementPattern(new MoveToPositionPattern(this.FormationStartPosition, this.enemyVelocity));

                this.enemies.Add(newEnemy);
                this.enemiesSpawned++;
                this.timeSinceLastSpawn = 0;
            }

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