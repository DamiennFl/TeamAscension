// <copyright file="CollisionManager.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using Ascension.Business_Layer;
using System.Collections.Generic;
using System.Diagnostics;

namespace Ascension
{
    /// <summary>
    /// Manages collision detection between game objects.
    /// </summary>
    public class CollisionManager
    {
        /// <summary>
        /// A collection of objects that can collide with each other in the game world.
        /// </summary>
        /// <remarks>
        /// This list maintains all entities that implement the ICollidable interface,
        /// allowing the collision manager to track and process collision detection between objects.
        /// </remarks>
        private readonly List<ICollidable> collidables = new List<ICollidable>();

        /// <summary>
        /// A dictionary that defines collision relationships between different types of game objects.
        /// The key represents a collider type, and the value is a list of types it can collide with.
        /// </summary>
        private readonly Dictionary<string, List<string>> collisionMatrix = new Dictionary<string, List<string>>();

        /// <summary>
        /// Queue that stores collision commands to be processed.
        /// </summary>
        private readonly Queue<ICollisionCommand> commandQueue = new Queue<ICollisionCommand>();

        /// <summary>
        /// Registers a collidable object with the collision system.
        /// </summary>
        /// <param name="collidable">The object to register.</param>
        public void Register(ICollidable collidable)
        {
            // Debug.WriteLine($"Registering colliable, count: {this.collidables.Count}");
            this.collidables.Add(collidable);
        }

        /// <summary>
        /// Unregisters a collidable object from the collision system.
        /// </summary>
        /// <param name="collidable">The object to unregister.</param>
        public void Unregister(ICollidable collidable)
        {
            this.collidables.Remove(collidable);
        }

        /// <summary>
        /// Defines which collision layers should interact with each other.
        /// </summary>
        /// <param name="layer1">First collision layer.</param>
        /// <param name="layer2">Second collision layer.</param>
        public void AddCollisionLayer(string layer1, string layer2)
        {
            if (!this.collisionMatrix.ContainsKey(layer1))
            {
                this.collisionMatrix[layer1] = new List<string>();
            }

            if (!this.collisionMatrix.ContainsKey(layer2))
            {
                this.collisionMatrix[layer2] = new List<string>();
            }

            this.collisionMatrix[layer1].Add(layer2);
            this.collisionMatrix[layer2].Add(layer1);
        }

        /// <summary>
        /// Updates the collision system, detecting and resolving collisions.
        /// </summary>
        public void Update()
        {
            // Detect collisions
            for (int i = 0; i < this.collidables.Count; i++)
            {
                for (int j = i + 1; j < this.collidables.Count; j++)
                {
                    var a = this.collidables[i];
                    var b = this.collidables[j];
                    // Debug.Print($"This is b: {b} This is a: {a}");
                    // Skip if these layers shouldn't or don't collide

                    // Ensuring no a or b can be collided with each other
                    // if either one id null
                    // Still room to test this.
                    if (a != null & b != null)
                    {
                        if (!this.ShouldCollide(a.CollisionLayer, b.CollisionLayer) || !this.CheckCollision(a, b))
                        {
                            continue;
                        }
                    }

                    this.GenerateCommands(a, b);
                    this.GenerateCommands(b, a);
                }
            }

            // Execute queued commands
            while (this.commandQueue.Count > 0)
            {
                this.commandQueue.Dequeue().Execute();
            }
        }

        /// <summary>
        /// Generates collision-related commands based on the interaction between two collidable objects.
        /// </summary>
        /// <param name="a">The first collidable object in the collision.</param>
        /// <param name="b">The second collidable object in the collision.</param>
        /// <remarks>
        /// Current collision handling:
        /// - Player hit by enemy bullet: Damages player and deactivates bullet
        /// - Enemy hit by player bullet: Deactivates bullet.
        /// </remarks>
        private void GenerateCommands(ICollidable a, ICollidable b)
        {
            switch (a)
            {
                case IEntity player when b is Bullet bullet && !bullet.IsPlayerBullet:
                    this.commandQueue.Enqueue(new DamageEntityCommand(player));
                    this.commandQueue.Enqueue(new DeactivateBulletCommand(bullet));
                    break;

                case IEntity enemy when b is Bullet bullet && bullet.IsPlayerBullet:
                    this.commandQueue.Enqueue(new DamageEntityCommand(enemy));
                    this.commandQueue.Enqueue(new DeactivateBulletCommand(bullet));
                    break;
            }
        }

        /// <summary>
        /// Determines whether two collision layers should interact with each other.
        /// </summary>
        /// <param name="layer1">The first collision layer to check.</param>
        /// <param name="layer2">The second collision layer to check.</param>
        /// <returns>
        /// True if layer1 is configured to collide with layer2, false otherwise.
        /// Returns false if layer1 is not found in the collision matrix.
        /// </returns>
        private bool ShouldCollide(string layer1, string layer2)
        {
            if (this.collisionMatrix.ContainsKey(layer1))
            {
                return this.collisionMatrix[layer1].Contains(layer2);
            }

            return false;
        }

        /// <summary>
        /// Checks if two collidable objects are intersecting.
        /// </summary>
        /// <param name="a">The first collidable object to check.</param>
        /// <param name="b">The second collidable object to check.</param>
        /// <returns>True if the objects' bounds intersect, false otherwise.</returns>
        private bool CheckCollision(ICollidable a, ICollidable b)
        {
            return a.Bounds.Intersects(b.Bounds);
        }
    }
}