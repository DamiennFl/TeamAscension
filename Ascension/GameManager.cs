using Ascension.Enemies;
using Ascension.Enemies.EnemyFormation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Ascension
{
    public class GameManager
    {
        /*Want the game manager to execute everything in the game,
         here we will manage waves, player input, collision, enemies, and
        shooting. This is where most if not all of our game will run.*/

        private Player player; // The player object.
        private List<EnemyFormation> formations; // The formation object.
        private BasicEnemyFactory enemyFactory; // The enemy factory object.
        private GraphicsDevice graphicsDevice; // The graphics device object.
        private ContentManager contentManager; // The content manager object.

        public GameManager(GraphicsDevice graphicsDevice, ContentManager content)
        {
            // Initialize the game manager.
            this.graphicsDevice = graphicsDevice;
            this.contentManager = content;
        }



        public void Draw(GameTime gameTime)
        {
            // Draw the game manager.
        }

        public void Update(GameTime gameTime)
        {
            // Run the game manager.
        }

        private void InitializeFormation()
        {
            // Initialize the game manager.
        }
    }
}
