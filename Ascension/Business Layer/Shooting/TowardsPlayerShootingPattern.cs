// <copyright file="TowardsPlayerShootingPattern.cs" company="Team Ascension">
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
    /// Shooting pattern that fires bullets towards the player's position.
    /// </summary>
    public class TowardsPlayerShootingPattern : IShootingPattern
    {
        /// <summary>
        /// Shooting pattern that fires bullets towards the player's position.
        /// </summary>
        /// <param name="shooter">Entity shooting.</param>
        public async void Shoot(IEntity shooter)
        {
            int numberOfBullets = 36; // Controls spacing smoothness
            float spawnRadius = 60f;  // Distance from the boss
            float bulletSpeed = 2f;

            Vector2 playerPosition = Player.PlayerPosition;
            Vector2 shooterCenter = shooter.Position;

            for (int i = 0; i < numberOfBullets * 1.5; i++)
            {
                // Calculate precise angle PER bullet
                float angle = MathHelper.TwoPi * ((float)i / numberOfBullets);
                Vector2 spawnOffset = new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * spawnRadius;
                Vector2 spawnPos = shooterCenter + spawnOffset;

                // Direction toward the player
                Vector2 direction = playerPosition - spawnPos;
                if (direction != Vector2.Zero)
                    direction.Normalize();

                Vector2 bulletVelocity = direction * bulletSpeed;

                shooter.FireBulletFromPosition(spawnPos, bulletVelocity);

                await Task.Delay(20); // Delay to "draw" the circle visually
            }
        }
    }
}