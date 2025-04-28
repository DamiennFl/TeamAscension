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
        /// Shoots bullets in a circular pattern, starting with a bullet aimed directly at the player.  
        /// </summary>  
        /// <param name="shooter">The shooter entity.</param>  
        /// <param name="playerPosition">The position of the player.</param>  
        public void Shoot(IEntity shooter)
        {
            int numberOfBullets = 32; // Total bullets in the circle  
            float bulletSpeed = 2f; // Adjust the speed as needed  
            float angleIncrement = MathF.PI * 2 / numberOfBullets;

            // Calculate the angle to the player  
            Vector2 directionToPlayer = Player.PlayerPosition - shooter.Position;
            float initialAngle = MathF.Atan2(directionToPlayer.Y, directionToPlayer.X);

            for (int i = 0; i < numberOfBullets; i++)
            {
                float angle = initialAngle + i * angleIncrement;
                Vector2 bulletVelocity = new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * bulletSpeed;
                shooter.FireBullet(bulletVelocity);
            }
        }
    }
}
