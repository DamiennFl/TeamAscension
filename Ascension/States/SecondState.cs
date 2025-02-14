using Ascension.Content.Controls;
using Ascension.Content.States;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ascension.Enemies.EnemyFormation;

namespace Ascension.States
{
    internal class SecondState : FirstState
    {
        private readonly List<Components> components;

        private int screenHeight;

        private int screenWidth;

        private Texture2D backGround;

        private List<EnemyFormation> enemyFormations;

        public SecondState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, Player currentPlayer, List<EnemyFormation> currentEnemyFormation) : base(game, graphicsDevice, content)
        {
            //this.backGround = content.Load<Texture2D>("MidBossBackground");
            this.screenHeight = graphicsDevice.Viewport.Height;
            this.screenWidth = graphicsDevice.Viewport.Width;
            this.backGround = content.Load<Texture2D>("Backgrounds/Stage2");
            this.components = new List<Components>();
            this.player = currentPlayer;
            this.enemyFormations = currentEnemyFormation;

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            this.graphicsDevice.Clear(Color.Black);

            this.BorderDraw(spriteBatch);

            this.DrawBackground(spriteBatch, this.backGround);

            foreach (var formation in this.enemyFormations)
            {
                formation.Draw(spriteBatch);
            }

            this.player.Draw(spriteBatch);

            this.BorderDraw(spriteBatch);

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            float updatedPlayerSpeed = this.player.playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds * 5;

            foreach (var formation in this.enemyFormations)
            {
                formation.Update(gameTime);
            }

            this.player.PlayerMovement(updatedPlayerSpeed);
            this.player.StayInBorder(this.borderRect, this.borderWidth);
        }

    }
}