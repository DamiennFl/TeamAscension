// <copyright file="GoOffScreenMovementPattern.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Ascension.Business_Layer.Movement
{
    /// <summary>
    /// Movement pattern that moves the enemy off-screen.
    /// </summary>
    internal class GoOffScreenMovementPattern : IMovementPattern
    {
        /// <summary>
        /// Moves the enemy off-screen.
        /// </summary>
        /// <param name="gameTime">Gametime.</param>
        /// <param name="movable">Movable.</param>
        public void Move(GameTime gameTime, IMovable movable)
        {
            movable.Velocity = new Vector2(-1, -1);
            movable.Position += movable.Velocity;
        }
    }
}
