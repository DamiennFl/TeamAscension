// <copyright file="Enemy.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Ascension.Business_Layer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Ascension
{
    /// <summary>
    /// Abstract class for enemies.
    /// </summary>
    public abstract class Enemy : IMovable, ICollidable, IEntity
    {
        /// <summary>
        /// The texture for the enemy.
        /// </summary>
        protected Texture2D texture;

        protected SpriteFont font;

        /// <summary>
        /// Gets or sets the scale at which the enemy is rendered.
        /// </summary>
        protected float Scale { get; set; } = 0.4f; // Default scale for all enemies

        /// <summary>
        /// Initializes a new instance of the <see cref="Enemy"/> class.
        /// </summary>
        /// <param name="velocity">Speed of the enemy.</param>
        /// <param name="position">Postion of the enemy.</param>
        /// <param name="texture">Texture of the enemy.</param>
        /// <param name="bulletType">The bullet type.</param>
        public Enemy(Vector2 velocity, Vector2 position, int health, Texture2D texture, string bulletType)
        {
            this.Velocity = velocity;
            this.texture = texture;
            this.Position = position;
            this.BulletType = bulletType;
            this.Health = health;
        }

        /// <summary>
        /// Event for when a bullet is fired
        /// </summary>
        public event Action<Vector2, Vector2, bool, string> BulletFired;

        /// <summary>
        /// Gets or sets queue of movement patterns.
        /// </summary>
        public IMovementPattern MovementPattern { get; set; }

        /// <summary>
        /// Gets the bounding box of the enemy.
        /// </summary>
        public Rectangle Bounds
        {
            get
            {
                // Calculate scaled dimensions
                int scaledWidth = (int)(this.texture.Width * this.Scale);
                int scaledHeight = (int)(this.texture.Height * this.Scale);

                // Position is the center point, calculate top-left for the rectangle
                int x = (int)this.Position.X - (scaledWidth / 2);
                int y = (int)this.Position.Y - (scaledHeight / 2);

                return new Rectangle(x, y, scaledWidth, scaledHeight);
            }
        }

        /// <summary>
        /// Draws the bounding box of the enemy.
        /// </summary>
        /// <param name="spriteBatch">Sprite.</param>
        public void DrawBounds(SpriteBatch spriteBatch)
        {
            Texture2D texture = this.texture;
            Rectangle bounds = this.Bounds;
            Color color = Color.Red;

            // Draw top line
            spriteBatch.Draw(texture, new Rectangle(bounds.Left, bounds.Top, bounds.Width, 1), color);
            // Draw bottom line
            spriteBatch.Draw(texture, new Rectangle(bounds.Left, bounds.Bottom, bounds.Width, 1), color);
            // Draw left line
            spriteBatch.Draw(texture, new Rectangle(bounds.Left, bounds.Top, 1, bounds.Height), color);
            // Draw right line
            spriteBatch.Draw(texture, new Rectangle(bounds.Right, bounds.Top, 1, bounds.Height), color);
        }

        /// <summary>
        /// Gets the collision layer identifier.
        /// </summary>
        public string CollisionLayer => "Enemy";

        /// <summary>
        /// Gets or sets a value indicating the bullet type.
        /// </summary>
        public string BulletType { get; set; }

        /// <summary>
        /// Gets or sets the speed of the enemy.
        /// </summary>
        public Vector2 Velocity { get; set; }

        /// <summary>
        /// Gets or sets the position of the enemy.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Gets or sets the health of the enemy.
        /// </summary>
        public int Health { get; set; }

        /// <summary>
        /// Gets or Sets a value indicating whether the enemy is invincible.
        /// </summary>
        public bool IsInvincible { get; set; } = false;

        /// <summary>
        /// Gets a value indicating whether the enemy is dead.
        /// </summary>
        public bool IsDead => this.Health <= 0;

        /// <summary>
        /// Updates the enemy.
        /// </summary>
        /// <param name="gameTime">Time of game.</param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Draws the enemy.
        /// </summary>
        /// <param name="spriteBatch">Sprites.</param>
        public abstract void Draw(SpriteBatch spriteBatch);

        /// <summary>
        /// Shoot method calls event.
        /// </summary>
        /// <param name="velo">bulet velocity.</param>
        /// <param name="isPlayerBullet">if it is a user's bullet.</param>
        /// <param name="bulletType">type of bullet.</param>
        public virtual void Shoot(Vector2 velo, bool isPlayerBullet, string bulletType)
        {
            this.BulletFired?.Invoke(this.Position, velo, isPlayerBullet, bulletType);
        }

        /// <summary>
        /// Shoots a bullet (default behavior).
        /// </summary>
        public virtual void Shoot()
        {
            this.BulletFired?.Invoke(this.Position, new Vector2(0, 2), false, this.BulletType);
        }

        /// <summary>
        /// Shoots bullets in a star pattern.
        /// </summary>
        public virtual void BurstShoot()
        {
            float angle = -0.9F;
            for (int i = 0; i < 3; i++)
            {
                Vector2 bulletVelocity = new Vector2(angle, 2);
                this.Shoot(bulletVelocity, false, this.BulletType);
                angle += 0.9F;
            }
        }

        /// <summary>
        /// Shoots bullets in a circular pattern.
        /// </summary>
        public void CircularShoot()
        {
            int numberOfBullets = 12; // Total bullets in the circles
            float bulletSpeed = 2f; // Adjust the speed as needed
            float angleIncrement = MathF.PI * 2 / numberOfBullets;

            for (int i = 0; i < numberOfBullets; i++)
            {
                float angle = i * angleIncrement;
                Vector2 bulletVelocity = new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * bulletSpeed;
                this.Shoot(bulletVelocity, false, this.BulletType);
            }
        }

        public void BulletWaveWithGaps()
        {
            int numberOfBullets = 40;
            float bulletSpeed = 3f;
            float angleIncrement = MathF.PI * 2 / numberOfBullets;
            int gapIndex = Random.Shared.Next(0, numberOfBullets / 2); // Creates two gaps

            for (int i = 0; i < numberOfBullets; i++)
            {
                if (i == gapIndex || i == gapIndex + (numberOfBullets / 2))
                    continue; // Creates two symmetrical gaps

                float angle = i * angleIncrement;
                Vector2 bulletVelocity = new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * bulletSpeed;
                this.Shoot(bulletVelocity, false, this.BulletType);
            }
        }

        public void AlternatingRingsShoot()
        {
            int numberOfBullets = 30;
            int numberOfRings = 3;
            float baseSpeed = 2f;
            float angleIncrement = MathF.PI * 2 / numberOfBullets;

            for (int ring = 0; ring < numberOfRings; ring++)
            {
                float speedMultiplier = 1f + (ring * 0.5f); // Each ring gets faster

                for (int i = 0; i < numberOfBullets; i++)
                {
                    float angle = i * angleIncrement;
                    Vector2 bulletVelocity = new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * (baseSpeed * speedMultiplier);
                    this.Shoot(bulletVelocity, false, this.BulletType);
                }
            }
        }

        public void ExpandingSpiralShoot()
        {
            int numberOfBullets = 24;
            float bulletSpeed = 2f;
            float angleOffset = 0f;
            float angleIncrement = MathF.PI * 2 / numberOfBullets;

            for (int i = 0; i < numberOfBullets; i++)
            {
                float angle = i * angleIncrement + angleOffset;
                Vector2 bulletVelocity = new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * bulletSpeed;
                this.Shoot(bulletVelocity, false, this.BulletType);
            }

            angleOffset += 0.1f; // Slowly shifts the starting angle, making a spiral
        }

        public void RealityCollapse()
        {
            this.BulletCone();
            this.BulletWall();
            this.XCrossfire();
            this.GalaxySpiralShoot();
            this.WavePatternShoot();
        }

        public void BulletCone()
        {
            int bulletRows = 5; // Number of rows in the cone
            int bulletsPerRow = 3; // Bullets per row, increasing each time
            float baseSpeed = 3f;
            float spreadAngle = MathF.PI / 6; // 30-degree spread

            for (int row = 0; row < bulletRows; row++)
            {
                float rowSpeed = baseSpeed + row * 0.5f; // Increasing speed per row
                float angleStep = spreadAngle / (bulletsPerRow - 1);
                float startAngle = MathF.PI / 2 - spreadAngle / 2; // Centered spread downwards

                for (int i = 0; i < bulletsPerRow; i++)
                {
                    float angle = startAngle + (i * angleStep);
                    Vector2 bulletVelocity = new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * rowSpeed;
                    this.Shoot(bulletVelocity, false, this.BulletType);
                }

                bulletsPerRow += 2; // Increase bullets per row for a wider cone
            }
        }

        public void BulletWall()
        {
            int bulletsPerRow = 15;
            int bulletRows = 3;
            float bulletSpeed = 1f;
            float spacing = 0.5f; // Adjust for tighter or looser grids

            for (int row = 0; row < bulletRows; row++)
            {
                float yOffset = row * spacing; // Staggers rows slightly for an interwoven effect

                for (int i = 0; i < bulletsPerRow; i++)
                {
                    float xPos = (i - bulletsPerRow / 2) * spacing;
                    Vector2 bulletVelocity = new Vector2(0, bulletSpeed); // Straight downward movement
                    this.Shoot(new Vector2(xPos, yOffset) + bulletVelocity, false, this.BulletType);
                }
            }
        }

        public void XCrossfire()
        {
            int arms = 4; // Four diagonal directions
            int bulletsPerArm = 6; // Each arm has three bullets
            float baseSpeed = 3f;
            float angleOffset = MathF.PI / 4; // 45-degree diagonal arms

            for (int arm = 0; arm < arms; arm++)
            {
                float angle = arm * MathF.PI / 2 + angleOffset; // 90-degree separations

                for (int j = 0; j < bulletsPerArm; j++)
                {
                    float speedMultiplier = 1f + j * 0.3f; // Vary speed for staggered effect
                    Vector2 bulletVelocity = new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * (baseSpeed * speedMultiplier);
                    this.Shoot(bulletVelocity, false, this.BulletType);
                }
            }
        }

        public void GalaxySpiralShoot()
        {
            int totalBullets = 100;
            float angleIncrement = MathF.PI * 10 / totalBullets; // Multiple rotations
            float radiusIncrement = 0.5f;
            float currentRadius = 0f;

            for (int i = 0; i < totalBullets; i++)
            {
                float angle = i * angleIncrement;
                currentRadius += radiusIncrement;
                Vector2 bulletVelocity = new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * currentRadius * 0.05f;
                this.Shoot(bulletVelocity, false, this.BulletType);
            }
        }

        public void FireworkExplosionShoot()
        {
            int bursts = 5;
            int bulletsPerBurst = 20;
            float baseSpeed = 2f;

            for (int burst = 0; burst < bursts; burst++)
            {
                float angleOffset = Random.Shared.NextSingle() * MathF.PI * 2;

                for (int i = 0; i < bulletsPerBurst; i++)
                {
                    float angle = i * (MathF.PI * 2 / bulletsPerBurst) + angleOffset;
                    float speed = baseSpeed + Random.Shared.NextSingle();
                    Vector2 bulletVelocity = new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * speed;
                    this.Shoot(bulletVelocity, false, this.BulletType);
                }
            }
        }

        public void WavePatternShoot()
        {
            int bulletsPerWave = 15;
            float waveCount = 3;
            float amplitude = 1.5f;
            float frequency = 0.3f;

            for (int wave = 0; wave < waveCount; wave++)
            {
                for (int i = -bulletsPerWave / 2; i < bulletsPerWave / 2; i++)
                {
                    float x = i * 0.2f;
                    float y = amplitude * MathF.Sin(frequency * x + wave);
                    Vector2 bulletVelocity = new Vector2(x, y + 2f);
                    this.Shoot(bulletVelocity, false, this.BulletType);
                }
            }
        }

        public void FlowerBloomShoot()
        {
            int petals = 8;
            int bulletsPerPetal = 10;
            float growthSpeed = 1f;

            for (int petal = 0; petal < petals; petal++)
            {
                float angleOffset = petal * (MathF.PI * 2 / petals);

                for (int i = 0; i < bulletsPerPetal; i++)
                {
                    float progress = (float)i / bulletsPerPetal;
                    float angle = angleOffset + progress * MathF.PI / petals;
                    float radius = progress * 5f;
                    Vector2 bulletVelocity = new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * radius * growthSpeed;
                    this.Shoot(bulletVelocity, false, this.BulletType);
                }
            }
        }

        public void ChaoticRandomShoot()
        {
            int totalBullets = 50;

            for (int i = 0; i < totalBullets; i++)
            {
                float angle = Random.Shared.NextSingle() * MathF.PI * 2;
                float speed = 1f + Random.Shared.NextSingle() * 3f;
                Vector2 bulletVelocity = new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * speed;
                this.Shoot(bulletVelocity, false, this.BulletType);
            }
        }

        public void TornadoShoot()
        {
            int spirals = 3;
            int bulletsPerSpiral = 50;
            float spiralSpacing = 0.1f;

            for (int s = 0; s < spirals; s++)
            {
                for (int i = 0; i < bulletsPerSpiral; i++)
                {
                    float progress = (float)i / bulletsPerSpiral;
                    float angle = progress * MathF.PI * 4 + s * MathF.PI * 2 / spirals;
                    float radius = progress * 5f;
                    Vector2 bulletVelocity = new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * radius * 0.3f;
                    this.Shoot(bulletVelocity, false, this.BulletType);
                }
            }
        }

        public void RadialBurstShoot()
        {
            int bursts = 3;
            int bulletsPerBurst = 20;
            float baseSpeed = 2f;
            float delayBetweenBursts = 0.5f; // Time between bursts in seconds

            for (int burst = 0; burst < bursts; burst++)
            {
                Task.Delay(TimeSpan.FromSeconds(burst * delayBetweenBursts)).ContinueWith(_ =>
                {
                    for (int i = 0; i < bulletsPerBurst; i++)
                    {
                        float angle = i * (MathF.PI * 2 / bulletsPerBurst);
                        Vector2 bulletVelocity = new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * baseSpeed;
                        this.Shoot(bulletVelocity, false, this.BulletType);
                    }
                });
            }
        }

        /// <summary>
        /// Activates the invincibility of the enemy, if Need be.
        /// </summary>
        public void ActivateInvincibility()
        {
        }
    }
}