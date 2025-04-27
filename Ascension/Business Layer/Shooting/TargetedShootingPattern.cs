using Microsoft.Xna.Framework;
using System.Threading.Tasks;

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
        public async void Shoot(IEntity shooter)
        {
            Vector2 playerPosition = Player.PlayerPosition;
            Vector2 direction = playerPosition - shooter.Position;
            int count = 20;

            if (direction != Vector2.Zero)
            {
                direction.Normalize();
                float bulletSpeed = 6f;
                //Vector2 bulletVelocity = direction * bulletSpeed;
                for (int i = 0; i < count; i++)
                {
                    Vector2 direction1 = playerPosition - shooter.Position;
                    direction1.Normalize();
                    Vector2 bulletVelocity = direction1 * bulletSpeed;
                    shooter.FireBullet(bulletVelocity);
                    await Task.Delay(100);
                }
            }
        }
    }
}