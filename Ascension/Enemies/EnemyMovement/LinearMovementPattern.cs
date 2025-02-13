using Microsoft.Xna.Framework;

namespace Ascension.Enemies.EnemyMovement
{
    /// <summary>
    /// Linear Movement Pattern for Enemies.
    /// </summary>
    internal class LinearMovementPattern : IMovementPattern
    {
        public Vector2 Velocity { get; set; }

        public LinearMovementPattern(GameTime gameTime, Vector2 velocity)
        {
            // Update player movement based on velocity.
            this.Velocity = velocity;
        }

        public void Update(GameTime gameTime, Enemy enemy)
        {
            enemy.Position += this.Velocity;
        }
    }
}
