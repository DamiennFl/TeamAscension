// <copyright file="DeactivateBulletCommand.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using Ascension;

/// <summary>
/// Command to deactivate a bullet entity in the game.
/// </summary>
/// <remarks>
/// Implements the ICollisionCommand interface to handle bullet deactivation when a collision occurs.
/// </remarks>
public class DeactivateBulletCommand : ICollisionCommand
{
    /// <summary>
    /// The bullet to be deactivated.
    /// </summary>
    private readonly Bullet bullet;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeactivateBulletCommand"/> class.
    /// </summary>
    /// <param name="bullet">The bullet to be deactivated.</param>
    public DeactivateBulletCommand(Bullet bullet)
    {
        this.bullet = bullet;
    }

    /// <summary>
    /// Executes the command by setting the bullet's IsActive property to false.
    /// </summary>
    public void Execute() => this.bullet.IsActive = false;
}