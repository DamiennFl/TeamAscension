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
        /// <summary>
        /// The delay between spawning enemies.
        /// </summary>
        private float spawnDelay;

        /// <summary>
        /// The time since the last enemy was spawned.
        /// </summary>
        private float timeSinceLastSpawn;

        /// <summary>
        /// The type of Enemy to spawn.
        /// </summary>
        private string enemyType;

        /// <summary>
        /// The number of enemies to spawn.
        /// </summary>
        private int numEnemies;

        /// <summary>
        /// The number of enemies spawned.
        /// </summary>
        private int enemiesSpawned;

        /// <summary>
        /// The velocity of the enemies.
        /// </summary>
        private Vector2 enemyVelocity;

        /// <summary>
        /// The spacing between enemies.
        /// </summary>
        private float enemySpacing;

        /// <summary>
        /// The enemy factory to create enemies.
        /// </summary>
        private EnemyFactory enemyFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="LinearFormation"/> class.
        /// </summary>
        /// <param name="formationPosition">The start position of this LinearFormation.</param>
        /// <param name="numEnemies">The number of Enemy objects to spawn in this LinearFormation.</param>
        /// <param name="spawnDelay">The spawn delay between Enemy instances.</param>
        /// <param name="enemyVelocity">The velocity vector for the Enemy MovementPattern.</param>
        /// <param name="enemySpacing">The spacing between Enemy instances, in pixel.s</param>
        /// <param name="factory">The EnemyFactory to create Enemy instances.</param>
        /// <param name="enemyType">The type of Enemy to create for this LinearFormation.</param>
        public LinearFormation(Vector2 formationPosition, int numEnemies, float spawnDelay, Vector2 enemyVelocity, float enemySpacing, EnemyFactory factory, string enemyType) : base(formationPosition)
        {
            this.numEnemies = numEnemies;
            this.spawnDelay = spawnDelay;
            this.timeSinceLastSpawn = 0;
            this.enemiesSpawned = 0;
            this.enemyVelocity = enemyVelocity;
            this.enemySpacing = enemySpacing;
            this.enemyFactory = factory;
            this.enemyType = enemyType;
        }

        /// <summary>
        /// Update method for updating the LinearFormation.
        /// </summary>
        /// <param name="gameTime">GameTime object to sync with game run-time.</param>
        public override void Update(GameTime gameTime)
        {
            this.timeSinceLastSpawn += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (this.enemiesSpawned < this.numEnemies && this.timeSinceLastSpawn >= this.spawnDelay)
            {
                // Calculate position for the new enemy.
                Vector2 enemyPosition = this.FormationStartPosition + new Vector2(this.enemiesSpawned * this.enemySpacing, 0);
                Enemy newEnemy = this.enemyFactory.CreateEnemy(enemyPosition, this.enemyType);
                // TODO: Add movement component LIST to the enemy.
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