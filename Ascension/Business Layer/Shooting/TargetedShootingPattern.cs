using Microsoft.Xna.Framework;

namespace Ascension.Business_Layer.Shooting
{
    /// <summary>
    /// Shooting pattern that fires bullets towards the player's position.
    /// </summary>
    public class TargetedShootingPattern : IShootingPattern
    {
        /// <summary>
        /// Shoots a bullet towards the player's position.
        /// </summary>
        /// <param name="shooter">The entity that is shooting.</param>
        public void Shoot(IEntity shooter)
        {
            Vector2 playerPosition = Player.PlayerPosition;
            Vector2 direction = playerPosition - shooter.Position;

            if (direction != Vector2.Zero)
            {
                direction.Normalize();
                float bulletSpeed = 5f;
                Vector2 bulletVelocity = direction * bulletSpeed;
                shooter.FireBullet(bulletVelocity);
            }
        }
    }
}