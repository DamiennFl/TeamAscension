using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace Ascension.Collision
{
    /// <summary>
    /// Manages collision detection between game objects.
    /// </summary>
    public class CollisionManager
    {
        private readonly List<ICollidable> collidables = new List<ICollidable>();
        private readonly Dictionary<string, List<string>> collisionMatrix = new Dictionary<string, List<string>>();

        /// <summary>
        /// Registers a collidable object with the collision system.
        /// </summary>
        /// <param name="collidable">The object to register.</param>
        public void Register(ICollidable collidable)
        {
            Debug.WriteLine($"Registering colliable, count: {this.collidables.Count}");
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
            for (int i = 0; i < this.collidables.Count; i++)
            {
                for (int j = i + 1; j < this.collidables.Count; j++)
                {
                    var a = this.collidables[i];
                    var b = this.collidables[j];

                    // Skip if these layers shouldn't collide
                    if (!this.ShouldCollide(a.CollisionLayer, b.CollisionLayer))
                    {
                        continue;
                    }

                    if (this.CheckCollision(a, b))
                    {
                        Debug.WriteLine("COLLISION DETECTED!");

                        a.OnCollision(b);
                        b.OnCollision(a);
                    }
                }
            }
        }

        private bool ShouldCollide(string layer1, string layer2)
        {
            if (this.collisionMatrix.ContainsKey(layer1))
            {
                return this.collisionMatrix[layer1].Contains(layer2);
            }

            return false;
        }

        private bool CheckCollision(ICollidable a, ICollidable b)
        {
            return a.Bounds.Intersects(b.Bounds);
        }
    }
}