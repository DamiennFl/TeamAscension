// <copyright file="BulletWallShootingPattern.cs" company="Team Ascension">
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
    /// Shooting pattern that creates a wall of bullets.
    /// </summary>
    public class BulletWallShootingPattern : IShootingPattern
    {
        /// <summary>
        /// Shooting pattern that creates a wall of bullets.
        /// </summary>
        /// <param name="shooter">Entity shooting.</param>
        public void Shoot(IEntity shooter)
        {
            int bulletsPerRow = 15;
            int bulletRows = 4;
            float bulletSpeed = 3f;
            float spacing = 20f; // Increased spacing for better separation

            for (int row = 0; row < bulletRows; row++)
            {
                float yOffset = row * spacing; // Staggers rows slightly for an interwoven effect

                for (int i = 0; i < bulletsPerRow; i++)
                {
                    float xPos = (i - (bulletsPerRow / 2)) * spacing;
                    float randomOffset = Random.Shared.NextSingle() * 0.2f; // Adds randomness to the position
                    Vector2 bulletVelocity = new Vector2(randomOffset, bulletSpeed); // Straight downward movement
                    if (shooter.IsPlayer)
                    {
                        bulletVelocity = -bulletVelocity;
                    }

                    Vector2 spawnPosition = new Vector2(shooter.Position.X + xPos, shooter.Position.Y + yOffset);
                    shooter.FireBulletFromPosition(spawnPosition, bulletVelocity);
                }
            }
        }
    }
}
