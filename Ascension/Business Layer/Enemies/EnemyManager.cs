﻿// <copyright file="EnemyManager.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using Ascension;
using Ascension.Business_Layer;
using Ascension.Business_Layer.Shooting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// EnemyManager is responsible for managing the enemy subsystem.
/// </summary>
public class EnemyManager
{
    /// <summary>
    /// EnemyFactory to create enemies.
    /// </summary>
    private EnemyFactory factory;

    /// <summary>
    /// MovementFactory to create movement patterns.
    /// </summary>
    private MovementFactory movementFactory;

    /// <summary>
    /// ShootingPatternFactory to create shooting patterns.
    /// </summary>
    private ShootingPatternFactory shootingPatternFactory;

    /// <summary>
    /// A BulletManager to register enemies to the OnBulletFired event.
    /// </summary>
    private BulletManager bulletManager;

    /// <summary>
    /// A CollisionManager to register spawned enemies as Collidable.
    /// </summary>
    private CollisionManager collisionManager;

    /// <summary>
    /// The spawnArea for the Enemies in a Wave.
    /// </summary>
    private Rectangle spawnArea;

    /// <summary>
    /// The spawnPosition for the Enemies in a Wave.
    /// </summary>
    private Vector2 spawnPosition;

    /// <summary>
    /// The spawnVelocity for Enemies in a Wave.
    /// </summary>
    private Vector2 spawnVelocity;

    /// <summary>
    /// borderManager manages the borders of the Game.
    /// </summary>
    private BorderManager borderManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="EnemyManager"/> class.
    /// </summary>
    /// <param name="contentManager">The contentManager of the Game.</param>
    /// <param name="graphicsDevice">The graphicsDevice of the Game.</param>
    /// <param name="collisionManager">A CollisionManager reference to register Enemies.</param>
    /// <param name="bulletManager">A BulletManager reference for Enemies to Shoot.</param>
    /// <param name="waves">A list of Waves to interpret.</param>
    /// <param name="playArea">The playArea of the Game.</param>
    public EnemyManager(ContentManager contentManager, GraphicsDevice graphicsDevice, CollisionManager collisionManager, BulletManager bulletManager, PlayArea playArea)
    {
        this.factory = new ConcreteEnemyFactory(contentManager, graphicsDevice);
        this.movementFactory = new MovementFactory();
        this.shootingPatternFactory = new ShootingPatternFactory();
        this.Enemies = new List<Enemy>();
        this.bulletManager = bulletManager;
        this.collisionManager = collisionManager;
        this.borderManager = new BorderManager(playArea);
    }

    /// <summary>
    /// Gets the list of Enemies for this EnemyManager.
    /// </summary>
    public List<Enemy> Enemies { get; }

    /// <summary>
    /// The Update method updates everything related to the Enemy subsystem,
    /// such as spawning, movement, and despawning.
    /// </summary>
    /// <param name="gameTime">The gameTime to stay synchronized with the Game.</param>
    public void Update(GameTime gameTime)
    {
        foreach (Enemy enemy in this.Enemies)
        {
            enemy.Update(gameTime);
            this.borderManager.CheckAndReverseVelocity(enemy);
        }
    }

    /// <summary>
    /// SpawnEnemy spawns enemies based on a Wave's info, a position, and a velocity.
    /// </summary>
    /// <param name="enemyType">Type of enemy being spawned.</param>
    /// <param name="position">The position to spawn the Enemy at.</param>
    /// <param name="velocity">The velocity for the Enemy.</param>
    /// <param name="health">The health of the Enemy.</param>"
    /// <param name="bulletType">The type of bullet the Enemy will shoot.</param>
    /// <param name="movementPattern">The movement pattern of the Enemy.</param>
    /// <param name="shootingPatterns">The shooting patterns of the Enemy.</param>
    /// <exception cref="ArgumentException">Throws an Exception if an invalid enemy type is inputted.</exception>
    public void SpawnEnemy(string enemyType, Vector2 position, Vector2 velocity, int health, string bulletType, string movementPattern, Dictionary<string, string> shootingPatterns)
    {
        // Spawn Enemy
        Enemy enemy = enemyType switch
        {
            "EnemyA" => this.factory.CreateEnemyA(position, velocity, health, bulletType),
            "EnemyB" => this.factory.CreateEnemyB(position, velocity, health, bulletType),
            "MidBoss" => this.factory.CreateMidBoss(position, velocity, health, bulletType),
            "FinalBoss" => this.factory.CreateFinalBoss(position, velocity, health, bulletType),
            _ => throw new ArgumentException("Unknown enemy type inputted")
        };

        // Apply movement
        enemy.MovementPattern = this.movementFactory.CreateMovementPattern(movementPattern);

        // Apply shooting pattern
        // IShootingPattern shootingPattern = this.shootingPatternFactory.CreateShootingPattern(wave.ShootingPattern);
        foreach (var shootingPattern in shootingPatterns)
        {
            enemy.ShootingPatterns.Add(this.shootingPatternFactory.CreateShootingPattern(shootingPattern.Key), shootingPattern.Value); // CHANGE THIS WITH THE WAVE BUILDER SEEN ABOVE ^^
        }

        // Add to list of enemies
        this.Enemies.Add(enemy);

        // Register collisions and bullet shoot event for Enemy
        this.bulletManager.RegisterEnemy(enemy);
        this.collisionManager.Register(enemy);
    }

    /// <summary>
    /// Checks every Enemy in the list to see if it is dead, and removes it if so.
    /// </summary>
    public void IsDead()
    {
        var enemies = this.Enemies.ToArray();
        foreach (var enemy in enemies)
        {
            if (enemy.IsDead)
            {
                this.Enemies.Remove(enemy);
                this.collisionManager.Unregister(enemy);
            }
        }
    }

    /// <summary>
    /// Draw method for the Enemies.
    /// </summary>
    /// <param name="spriteBatch">spriteBatch to draw into.</param>
    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var enemy in this.Enemies)
        {
            enemy.Draw(spriteBatch);
            enemy.DrawBounds(spriteBatch);
        }
    }

    /// <summary>
    /// MoveEnemiesOffScreen sets the velocity of all enemies to go offscreen
    /// </summary>
    /// <param name="playArea">Play area for game.</param>
    public void MoveEnemiesOffScreen(PlayArea playArea)
    {
        foreach (Enemy enemy in this.Enemies)
        {
            this.SetOffScreenVelocity(enemy, playArea);
            enemy.MovementPattern = this.movementFactory.CreateMovementPattern("GoOffScreen");
        }
    }

    /// <summary>
    /// SetOffScreenVelocity sets the Enemy's velocity based on the closest
    /// border once the Wave is over.
    /// </summary>
    /// <param name="enemy">The Enemy to apply the new velocity to.</param>
    /// <param name="playArea">The playArea.</param>
    private void SetOffScreenVelocity(Enemy enemy, PlayArea playArea)
    {
        Vector2 velocity = Vector2.Zero;

        // Find the nearest left, right, or top border based on the spawnArea
        float leftDistance = Math.Abs(enemy.Position.X - playArea.BorderRectangle.Left);
        float rightDistance = Math.Abs(enemy.Position.X - playArea.BorderRectangle.Right);

        float minDistance = Math.Min(leftDistance, rightDistance);

        // Set the velocity based on closest border
        if (minDistance == leftDistance)
        {
            velocity.X = -2.5f;
            velocity.Y = (float)(-2.5f + (5 * new Random().NextDouble()));
        }
        else if (minDistance == rightDistance)
        {
            velocity.X = 2.5f;
            velocity.Y = (float)(-2.5f + (5 * new Random().NextDouble()));
        }

        // Update velocity
        enemy.Velocity = velocity;
    }
}