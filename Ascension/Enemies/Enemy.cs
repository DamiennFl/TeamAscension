using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascension.Enemies
{
    /// <summary>
    /// Abstract Enemy class.
    /// </summary>
    internal abstract class Enemy
    {
        // Speed
        // Position
        // Sprite/Texture

        public Texture2D texture;
        
        public float Speed { get; set; }

        public Vector2 Position { get; set; }

        public Enemy(float speed, Texture2D texture)
        {
            this.Speed = speed;
            this.texture = texture;
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
