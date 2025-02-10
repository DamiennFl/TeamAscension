// <copyright file="BasicEnemyFactory.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using Ascension;
using Ascension.Content.Enemies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// Concrete factory implementation for creating basic enemies.
/// </summary>
public class BasicEnemyFactory : EnemyFactory
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicEnemyFactory"/> class.
    /// </summary>
    /// <param name="contentManager">The content manager for loading assets.</param>
    /// <param name="graphicsDevice">The graphics device for rendering.</param>
    public BasicEnemyFactory(ContentManager contentManager, GraphicsDevice graphicsDevice)
        : base(contentManager, graphicsDevice)
    {
    }

    /// <summary>
    /// Creates a basic enemy at the specified position.
    /// </summary>
    /// <param name="position">The spawn position for the basic enemy.</param>
    /// <returns>A new basic enemy instance.</returns>
    public override Enemy CreateEnemy(Vector2 position)
    {
        Texture2D basicTexture = this.ContentManager.Load<Texture2D>("ball");
        return new BasicEnemy(basicTexture, position);
    }
}