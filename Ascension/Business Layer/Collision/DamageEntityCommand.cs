// <copyright file="DamagePlayerCommand.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using Ascension.Business_Layer;
using System;
using System.Diagnostics;

namespace Ascension
{
    /// <summary>
    /// Represents a command that applies damage to a player upon collision.
    /// </summary>
    /// <remarks>
    /// This command follows the Command pattern and implements ICollisionCommand interface.
    /// It decreases the player's health by the specified damage amount when executed.
    /// </remarks>
    public class DamageEntityCommand : ICollisionCommand
    {
        /// <summary>
        /// The player entity that will receive damage from this command.
        /// </summary>
        private readonly IEntity entity;

        /// <summary>
        /// Initializes a new instance of the <see cref="DamageEntityCommand"/> class.
        /// </summary>
        /// <param name="player">The player who will receive damage.</param>
        /// <param name="damage">The amount of damage to be dealt to the player.</param>
        public DamageEntityCommand(IEntity currentEntity)
        {
            this.entity = currentEntity;
        }

        /// <summary>
        /// Executes damage to the player by reducing their health by the specified damage amount.
        /// </summary>
        public void Execute()
        {
            if (!this.entity.IsInvincible)
            {
                this.entity.Health -= 1;
                // Debug.WriteLine($"Entity Health: {this.entity.Health}");
                this.entity.ActivateInvincibility();
            }
        }
    }
}