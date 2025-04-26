using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascension.Business_Layer.Shooting
{
    /// <summary>
    /// Shooting pattern that shoots bullets in a burst pattern.
    /// </summary>
    public class BurstShootingPattern : IShootingPattern
    {
        /// <summary>
        /// Shooting pattern that shoots bullets in a burst pattern.
        /// </summary>
        /// <param name="shooter">Entity shooting.</param>
        public void Shoot(IEntity shooter)
        {
            if (shooter.IsPlayer)
            {
                float angle = 0.9F;
                for (int i = 0; i < 3; i++)
                {
                    Vector2 bulletVelocity = new Vector2(angle, -7f);
                    shooter.FireBullet(bulletVelocity);
                    angle -= 0.9F;
                }
            }
            else
            {
                float angle = -0.9F;
                for (int i = 0; i < 3; i++)
                {
                    Vector2 bulletVelocity = new Vector2(angle, 2);
                    shooter.FireBullet(bulletVelocity);
                    angle += 0.9F;
                }
            }
        }
    }
}
