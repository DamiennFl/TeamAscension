using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascension.Enemies.EnemyFormation
{
    /// <summary>
    /// LinearFormation spawns enemies in a linear formation.
    /// </summary>
    internal class LinearFormation : EnemyFormation
    {
        // TODO: Write linear formation
        private float spawnDelay;
        private float timeSinceLastSpawn;
        private int numEnemies;
        private int enemiesSpawned;
        private Vector2 enemyVelocity;
        private float enemySpacing;
        private EnemyFactory enemyFactory;

        public LinearFormation(Vector2 formationPosition, int numEnemies, float spawnDelay, Vector2 enemyVelocity, float enemySpacing, EnemyFactory factory) : base(formationPosition)
        {
            this.numEnemies = numEnemies;
            this.spawnDelay = spawnDelay;
            this.timeSinceLastSpawn = 0;
            this.enemiesSpawned = 0;
            this.enemyVelocity = enemyVelocity;
            this.enemySpacing = enemySpacing;
            this.enemyFactory = factory;
        }

        public override void Update(GameTime gameTime)
        {
            this.timeSinceLastSpawn += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (this.enemiesSpawned < this.numEnemies && this.timeSinceLastSpawn >= this.spawnDelay)
            {
                // Calculate position for the new enemy.
                Vector2 enemyPosition = this.FormationStartPosition + new Vector2(this.enemiesSpawned * this.enemySpacing, 0);
                Enemy newEnemy = this.enemyFactory.CreateEnemy(enemyPosition, "EnemyA");
                // newEnemy.AddMovementComponent(new LinearMovement(_enemyVelocity));
                this.enemies.Add(newEnemy);
                this.enemiesSpawned++;
                this.timeSinceLastSpawn = 0;
            }

            // Update all spawned enemies
            foreach (var enemy in this.enemies)
            {
                enemy.Update(gameTime);
            }
        }
    }
}