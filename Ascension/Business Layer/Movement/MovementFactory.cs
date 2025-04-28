// <copyright file="MovementFactory.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System;
using Ascension.Business_Layer.Movement;

namespace Ascension
{
    /// <summary>
    /// Factory class for creating movement patterns.
    /// </summary>
    internal class MovementFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MovementFactory"/> class.
        /// </summary>
        public MovementFactory() { }

        /// <summary>
        /// Creates a movement pattern based on the provided type.
        /// </summary>
        /// <param name="type">Type of movement.</param>
        /// <returns>IMovementPattern.</returns>
        /// <exception cref="NotImplementedException">Exception if movement isn't implemented.</exception>
        public IMovementPattern CreateMovementPattern(string type)
        {
            switch (type)
            {
                case "Linear":
                    return new LinearMovementPattern();
                case "Wave":
                    return new WaveMovementPattern();
                case "ZigZag":
                    return new ZigZagMovementPattern();
                case "GoMiddle":
                    return new GoMiddleMovementPattern();
                case "GoOffScreen":
                    return new GoOffScreenMovementPattern();
                default:
                    throw new NotImplementedException($"No Movement Pattern by name: {type}");
            }
        }
    }
}