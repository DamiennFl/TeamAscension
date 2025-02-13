using Microsoft.Xna.Framework;

namespace Ascension.Enemies.EnemyMovement
{
    /// <summary>
    /// Linear Movement Pattern for Enemies.
    /// </summary>
    internal class LinearMovementPattern : IMovementPattern
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LinearMovementPattern"/> class.
        /// </summary>
        /// <param name="gameTime">GameTime object to sync with game run-time.</param>
        /// <param name="velocity">Velocity vector for this MovementPattern.</param>
        public LinearMovementPattern(GameTime gameTime, Vector2 velocity)
        {
            // Update player movement based on velocity.
            this.Velocity = velocity;
        }

        /// <summary>
        /// Gets or sets the velocity of the Enemy.
        /// </summary>
        public Vector2 Velocity { get; set; }

        /// <summary>
        /// Updates the Enemy movement based on the pattern.
        /// </summary>
        /// <param name="gameTime">GameTime object to sync with game run-time.</param>
        /// <param name="enemy">Enemy to update movement of.</param>
        public void Update(GameTime gameTime, Enemy enemy)
        {
            enemy.Position += this.Velocity;
        }
    }
}
