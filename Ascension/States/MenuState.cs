// <copyright file="MenuState.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using Ascension.Content.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Ascension.Content.States
{
    /// <summary>
    /// The menu state.
    /// </summary>
    public class MenuState : State
    {
        /// <summary>
        /// The components.
        /// </summary>
        private readonly List<Components> components;

        /// <summary>
        /// The background texture.
        /// </summary>
        private Texture2D backGround;

        /// <summary>
        /// The screen height and width.
        /// </summary>
        private int screenHeight;

        /// <summary>
        /// The screen width.
        /// </summary>
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

            // TODO: Find a better way to implement this button initialization

            // Our Play Game button
            var playGameButton = new Button(buttonTexture, buttonFont)
            {
                Pos = new Vector2(335, 400),
                Text = "Play",
                Size = new Vector2(300, 100),
            };

            // Bindings Button
            var bindingsButton = new Button(buttonTexture, buttonFont)
            {
                Pos = new Vector2(335, 500),
                Text = "Key Bindings",
                Size = new Vector2(300, 100),
            };

            playGameButton.Click += this.PlayGameButton_Click;
            bindingsButton.Click += this.BindingsButton_Click;

            this.components = new List<Components>()
            {
                playGameButton,
                bindingsButton,
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
        /// Update for the menu state.
        /// </summary>
        /// <param name="gameTime">Time of game.</param>
        public override void Update(GameTime gameTime)
        {
            foreach (var component in this.components)
            {
                component.Update(gameTime);
            }
        }


        /// <summary>
        /// Play game button click event.
        /// </summary>
        /// <param name="sender">The play button.</param>
        /// <param name="e">Clicked.</param>
        private void PlayGameButton_Click(object sender, EventArgs e)
        {
            this.game.ChangeState(new FirstState(this.game, this.graphicsDevice, this.content));
        }


        /// <summary>
        /// Bindings button click event.
        /// </summary>
        /// <param name="sender">The binding button. </param>
        /// <param name="e">Clicked. </param>
        private void BindingsButton_Click(object sender, EventArgs e)
        {
            this.game.ChangeState(new BindingsState(this.game, this.graphicsDevice, this.content));
        }
    }
}
