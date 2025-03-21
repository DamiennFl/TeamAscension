// <copyright file="DamagePlayerCommand.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System.Diagnostics;
using Ascension;

/// <summary>
/// Represents a command that applies damage to a player upon collision.
/// </summary>
/// <remarks>
/// This command follows the Command pattern and implements ICollisionCommand interface.
/// It decreases the player's health by the specified damage amount when executed.
/// </remarks>
public class DamagePlayerCommand : ICollisionCommand
{
    /// <summary>
    /// The player entity that will receive damage from this command.
    /// </summary>
    private readonly Player player;

    /// <summary>
    /// The amount of damage to be applied to the player.
    /// </summary>
    private readonly int damage;

    /// <summary>
    /// Initializes a new instance of the <see cref="DamagePlayerCommand"/> class.
    /// </summary>
    /// <param name="player">The player who will receive damage.</param>
    /// <param name="damage">The amount of damage to be dealt to the player.</param>
    public DamagePlayerCommand(Player player, int damage)
    {
        this.player = player;
        this.damage = damage;
    }

    /// <summary>
    /// Executes damage to the player by reducing their health by the specified damage amount.
    /// </summary>
    public void Execute()
    {
        this.player.Health -= this.damage;
        Debug.WriteLine($"Player Health: {this.player.Health}");
    }
}