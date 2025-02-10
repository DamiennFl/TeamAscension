// <copyright file="Game1.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using Ascension.Content.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Ascension
{
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA0001:XmlCommentAnalysisDisabled", Justification = "Documentation not required.")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1516:ElementsMustBeSeparatedByBlankLine", Justification = "Formatting not required.")]

    /// <summary>
    /// Game1 class.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;


        private State currentState;
        private State nextState;

        public void ChangeState(State state)
        {
            this.nextState = state;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Game1"/> class.
        /// Our Game constructor.
        /// </summary>
        public Game1()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.graphics.PreferredBackBufferWidth = 1000;
            this.graphics.PreferredBackBufferHeight = 800;
            this.Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
        }

        /// <summary>
        /// Initializes the game.
        /// </summary>
        protected override void Initialize()
        {
            this.IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);

            this.currentState = new MenuState(this, this.GraphicsDevice, this.Content);
        }

        /// <summary>
        /// Updates the game.
        /// </summary>
        /// <param name="gameTime">Time of update.</param>
        protected override void Update(GameTime gameTime)
        {
            if (this.nextState != null)
            {
                this.currentState = this.nextState;
                this.nextState = null;
            }

            this.currentState.Update(gameTime);
            this.currentState.PostUpdate(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the game.
        /// </summary>
        /// <param name="gameTime">Time of game.</param>
        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.White);
            this.currentState.Draw(gameTime, this.spriteBatch);
            base.Draw(gameTime);
        }
    }
}