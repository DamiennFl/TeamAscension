﻿// <copyright file="BulletB.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ascension.Business_Layer.Bullets
{
    internal class BulletC : Bullet
    {
        private static new IMovementPattern MovementPattern;

        /// <summary>
        /// Initializes a new instance of the <see cref="BulletB"/> class.
        /// </summary>
        /// <param name="damage">Damage of bullet.</param>
        /// <param name="velocity">Velocity of bullet.</param>
        /// <param name="bulletPosition">Position of bullet.</param>
        /// <param name="bulletTexture">Texture of bullet.</param>
        public BulletC(Vector2 velocity, Vector2 bulletPosition, Texture2D bulletTexture)
            : base(velocity, bulletPosition)
        {
            this.Velocity = velocity;
            this.BulletTexture = bulletTexture;
            this.IsActive = true;
            this.IsPlayerBullet = false;
        }
    }
}
