﻿using Ascension.Content.Controls;
using Ascension.Content.States;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascension.States
{
    internal class SecondState : FirstState
    {
        private readonly List<Components> components;

        private int screenHeight;

        private int screenWidth;


        public SecondState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, Player currentPlayer) : base(game, graphicsDevice, content)
        {
            //this.backGround = content.Load<Texture2D>("MidBossBackground");
            this.screenHeight = graphicsDevice.Viewport.Height;
            this.screenWidth = graphicsDevice.Viewport.Width;
            this.components = new List<Components>();
            this.player = currentPlayer;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            this.graphicsDevice.Clear(Color.Thistle);

            this.BorderDraw(spriteBatch);

            this.player.Draw(spriteBatch);

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            float updatedPlayerSpeed = this.player.playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds * 5;
            this.player.PlayerMovement(updatedPlayerSpeed);
            this.player.StayInBorder(this.borderRect, this.borderWidth);
        }

    }
}
