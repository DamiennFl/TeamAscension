// <copyright file="MenuState.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using Ascension.Content.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Ascension.Content.States
{
    /// <summary>
    /// The menu state.
    /// </summary>
    public class MenuState : State
    {
        private readonly List<Components> components;
        private Texture2D backGround;
        private int screenHeight;
        private int screenWidth;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuState"/> class.
        /// </summary>
        /// <param name="game">game.</param>
        /// <param name="graphicsDevice">GD.</param>
        /// <param name="content">content.</param>
        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            var buttonTexture = this.content.Load<Texture2D>("Controls/Button");
            var buttonFont = this.content.Load<SpriteFont>("Fonts/Font");
            this.backGround = this.content.Load<Texture2D>("Backgrounds/AscensionTitle");
            this.screenHeight = graphicsDevice.Viewport.Height;
            this.screenWidth = graphicsDevice.Viewport.Width;

            var playGameButton = new Button(buttonTexture, buttonFont)
            {
                Pos = new Vector2(235, 400),
                Text = "Play",
            };

            playGameButton.Click += this.PlayGameButton_Click;

            this.components = new List<Components>()
            {
                playGameButton,
            };
        }

        /// <summary>
        /// Draws the menu state.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <param name="spriteBatch">The sprite batch.</param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(this.backGround, new Rectangle(0, 0, this.screenWidth, this.screenHeight), Color.White);

            foreach (var component in this.components)
            {
                component.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();
        }

        /// <summary>
        /// Post update for the menu state.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void PostUpdate(GameTime gameTime)
        {
            // Removes Sprites if they are not needed
        }

        /// <summary>
        /// Updates the menu state.
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            foreach (var component in this.components)
            {
                component.Update(gameTime);
            }
        }

        private void PlayGameButton_Click(object sender, EventArgs e)
        {
            this.game.ChangeState(new GameState(this.game, this.graphicsDevice, this.content));
        }
    }
}
