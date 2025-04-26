// <copyright file="BulletConeShootingPattern.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Ascension.Business_Layer.Shooting
{
    /// <summary>
    /// Shooting pattern that creates a cone of bullets.
    /// </summary>
    public class BulletConeShootingPattern : IShootingPattern
    {
        /// <summary>
        /// Shooting pattern that creates a cone of bullets.
        /// </summary>
        /// <param name="shooter">Entity shooting.</param>
        public void Shoot(IEntity shooter)
        {
            int bulletRows = 5; // Number of rows in the cone
            int bulletsPerRow = 3; // Bullets per row, increasing each time
            float baseSpeed = 3f;
            float spreadAngle = MathF.PI / 6; // 30-degree spread

            for (int row = 0; row < bulletRows; row++)
            {
                float rowSpeed = baseSpeed + (row * 0.5f); // Increasing speed per row
                float angleStep = spreadAngle / (bulletsPerRow - 1);
                float startAngle = (MathF.PI / 2) - (spreadAngle / 2); // Centered spread downwards

                for (int i = 0; i < bulletsPerRow; i++)
                {
                    float angle = startAngle + (i * angleStep);
                    Vector2 bulletVelocity = new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * rowSpeed;
                    if (shooter.IsPlayer)
                    {
                        bulletVelocity = -bulletVelocity; // Reverse direction if the shooter is a player
                    }

                    shooter.FireBullet(bulletVelocity);
                }

                bulletsPerRow += 2; // Increase bullets per row for a wider cone
            }
        }
    }
}
