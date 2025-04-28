using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Ascension.Business_Layer.Shooting
{
    /// <summary>
    /// Shooting pattern that fires bullets towards the player and two diagonal directions.
    /// </summary>
    public class TripleSpiralShootingPattern : IShootingPattern
    {
        /// <summary>
        /// Shooting pattern that fires bullets in a spiral pattern towards the player and two diagonal directions.
        /// </summary>
        /// <param name="shooter">Entity shooting.</param>
        public async void Shoot(IEntity shooter)
        {
            int numberOfBullets = 25; // Controls spacing smoothness
            float spawnRadius = 30f;  // Initial distance from the boss
            float bulletSpeed = 3f;
            float radiusIncrement = 1f; // Amount to increase the radius per iteration

            Vector2 playerPosition = Player.PlayerPosition;
            Vector2 shooterCenter = shooter.Position;

            for (int i = 0; i < numberOfBullets * 2.5; i++)
            {
                // Calculate spiral spawn position
                float angle = MathHelper.TwoPi * ((float)i / numberOfBullets);
                Vector2 spawnOffset = new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * spawnRadius;
                Vector2 spawnPos = shooterCenter + spawnOffset;

                // --- First bullet: Towards player ---
                Vector2 directionToPlayer = playerPosition - spawnPos;
                if (directionToPlayer != Vector2.Zero)
                {
                    directionToPlayer.Normalize();
                }

                Vector2 velocityToPlayer = directionToPlayer * bulletSpeed;
                shooter.FireBulletFromPosition(spawnPos, velocityToPlayer);

                // --- Second bullet: Diagonal upward left (45 degrees up-left) ---
                Vector2 upwardLeft = new Vector2(-1f, -1f);
                upwardLeft.Normalize();
                Vector2 velocityUpLeft = upwardLeft * bulletSpeed;
                shooter.FireBulletFromPosition(spawnPos, velocityUpLeft);

                // --- Third bullet: Diagonal upward right (45 degrees up-right) ---
                Vector2 upwardRight = new Vector2(1f, -1f);
                upwardRight.Normalize();
                Vector2 velocityUpRight = upwardRight * bulletSpeed;
                shooter.FireBulletFromPosition(spawnPos, velocityUpRight);

                // Increment the spawn radius
                spawnRadius += radiusIncrement;

                await Task.Delay(20); // Delay to "draw" the triple spiral
            }
        }
    }
}