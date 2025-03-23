// <copyright file="ICollisionCommand.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

/// <summary>
/// Defines a contract for implementing collision response commands.
/// </summary>
/// <remarks>
/// This interface follows the Command pattern to encapsulate collision response logic
/// that needs to be executed when collisions occur between game objects.
/// </remarks>
public interface ICollisionCommand
{
    /// <summary>
    /// Executes the collision response command.
    /// </summary>
    void Execute();
}