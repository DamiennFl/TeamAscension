// <copyright file="CircularShootingPattern.cs" company="Team Ascension">
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
    /// Shooting pattern that shoots bullets in a circular pattern.
    /// </summary>
    public class CircularShootingPattern : IShootingPattern
    {
        /// <summary>
        /// Shoots bullets in a circular pattern.
        /// </summary>
        /// <param name="shooter">The shooter entity.</param>
        public void Shoot(IEntity shooter)
        {
            int numberOfBullets = 12; // Total bullets in the circles
            float bulletSpeed = 2f; // Adjust the speed as needed
            float angleIncrement = MathF.PI * 2 / numberOfBullets;

            for (int i = 0; i < numberOfBullets; i++)
            {
                float angle = i * angleIncrement;
                Vector2 bulletVelocity = new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * bulletSpeed;
                shooter.FireBullet(bulletVelocity);
            }
        }
    }
}
