// <copyright file="AlternatingRingsShootingPattern.cs" company="Team Ascension">
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
    /// Shooting pattern that shoots bullets in alternating rings.
    /// </summary>
    public class AlternatingRingsShootingPattern : IShootingPattern
    {
        /// <summary>
        /// Shooting pattern that shoots bullets in alternating rings.
        /// </summary>
        /// <param name="shooter">Entity shooting.</param>
        public void Shoot(IEntity shooter)
        {
            int numberOfBullets = 30;
            int numberOfRings = 3;
            float baseSpeed = 2f;
            float angleIncrement = MathF.PI * 2 / numberOfBullets;

            for (int ring = 0; ring < numberOfRings; ring++)
            {
                float speedMultiplier = 1f + (ring * 0.5f); // Each ring gets faster

                for (int i = 0; i < numberOfBullets; i++)
                {
                    float angle = (i * angleIncrement) + ((Random.Shared.NextSingle() - 3f) * 0.2f); // Add randomness to the angle
                    Vector2 bulletVelocity = new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * (baseSpeed * speedMultiplier);
                    shooter.FireBullet(bulletVelocity);
                }
            }
        }
    }
}
