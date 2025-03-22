// <copyright file="BindingsState.cs" company="Team Ascension">
// Copyright (c) Team Ascension. All rights reserved.
// </copyright>


using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using Ascension.Content.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Ascension.Content.States
{
    /* Currently, this class is empty. It will be used to handle the bindings of the game.
     * Thinking of possibly a different way then a state to handle this.
     * Thinking user will change current binding via combo box
     * or the user presses a key to set the binding.
     */
    internal class BindingsState : State
    {

        /// <summary>
        /// The components.
        /// </summary>
        private List<Components> components;

        private SpriteFont font;

        private Texture2D buttonTexture;

        public BindingsState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            // Add an Escape Button to return to the main menu
            this.buttonTexture = this.content.Load<Texture2D>("Controls/Button");
            this.font = this.content.Load<SpriteFont>("Fonts/Font");

            this.InitButtons();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            this.ScreenText(spriteBatch);

            foreach (var component in this.components)
            {
                component.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            
        }

        /// <summary>
        /// Updates our components every tick.
        /// </summary>
        /// <param name="gameTime">game time.</param>
        public override void Update(GameTime gameTime)
        {
            foreach (var component in this.components)
            {
                component.Update(gameTime);
            }
        }

        /// <summary>
        /// Initializes all buttons in the scene.
        /// </summary>
        private void InitButtons()
        {
            var escapeButton = new Button(this.buttonTexture, this.font)
            {
                Pos = new Vector2(-15, -5),
                Text = "Escape",
                Size = new Vector2(200, 100),
            };

            var forwardButton = new Button(this.buttonTexture, this.font)
            {
                Pos = new Vector2(250, 250),
                Text = "Forward",
                Size = new Vector2(200, 100),
            };

            var backwardButton = new Button(this.buttonTexture, this.font)
            {
                Pos = new Vector2(250, 350),
                Text = "Backward",
                Size = new Vector2(200, 100),
            };

            var rightButton = new Button(this.buttonTexture, this.font)
            {
                Pos = new Vector2(250, 450),
                Text = "Right",
                Size = new Vector2(200, 100),
            };

            var leftButton = new Button(this.buttonTexture, this.font)
            {
                Pos = new Vector2(250, 550),
                Text = "Left",
                Size = new Vector2(200, 100),
            };

            var sprintButton = new Button(this.buttonTexture, this.font)
            {
                Pos = new Vector2(250, 650),
                Text = "Sprint",
                Size = new Vector2(200, 100),
            };

            escapeButton.Click += this.EscapeButton_Click;
            forwardButton.Click += this.ForwardButton_Click;
            backwardButton.Click += this.BackwardButton_Click;
            rightButton.Click += this.RightButton_Click;
            leftButton.Click += this.LeftButton_Click;
            sprintButton.Click += this.SprintButton_Click;

            // Adding buttons to the list of components.
            this.components = new List<Components>()
            {
                escapeButton,
                forwardButton,
                backwardButton,
                rightButton,
                leftButton,
                sprintButton,
            };
        }

        /// <summary>
        /// All the text we will need to send instructions to the user.
        /// </summary>
        /// <param name="spriteBatch">sprite batch. </param>
        private void ScreenText(SpriteBatch spriteBatch)
        {
            string instructions = " How to Set  Key bindings.\n Below are Buttons corresponding to each action a player does,\n" +
                " the default bindings are shown on the right hand side.\n To set a custom binding you must hold the desired key," +
                "\n then click the button and your new binding will be set !";

            string fowardBind = $"Current Binding: {Player.PlayerMovementKeys.Up}";
            string backBind = $"Current Binding: {Player.PlayerMovementKeys.Down}";
            string rightBind = $"Current Binding: {Player.PlayerMovementKeys.Right}";
            string leftBind = $"Current Binding: {Player.PlayerMovementKeys.Left}";
            string sprintBind = $"Current Binding: {Player.PlayerMovementKeys.Sprint}";

            spriteBatch.DrawString(this.font, instructions, new Vector2(255, 10), Color.White);

            spriteBatch.DrawString(this.font, fowardBind, new Vector2(450, 290), Color.White);

            spriteBatch.DrawString(this.font, backBind, new Vector2(450, 390), Color.White);

            spriteBatch.DrawString(this.font, rightBind, new Vector2(450, 490), Color.White);

            spriteBatch.DrawString(this.font, leftBind, new Vector2(450, 590), Color.White);

            spriteBatch.DrawString(this.font, sprintBind, new Vector2(450, 690), Color.White);

        }

        /// <summary>
        /// Event handler, will send user back to the menu after keybinds are set.
        /// </summary>
        /// <param name="sender">The Escape Button. </param>
        /// <param name="e">Clicked. </param>
        private void EscapeButton_Click(object sender, EventArgs e)
        {
            this.game.ChangeState(new MenuState(this.game, this.graphicsDevice, this.content));
        }

        /// <summary>
        /// Sets the key binding.
        /// </summary>
        /// <param name="sender">Button.</param>
        /// <param name="e">Clicked. </param>
        private void ForwardButton_Click(object sender, EventArgs e)
        {
            KeyboardState key = Keyboard.GetState();
            if (key.GetPressedKeys().Length > 0)
            {
                Player.PlayerMovementKeys.Up = key.GetPressedKeys()[0];
            }
        }

        /// <summary>
        /// Sets the key binding.
        /// </summary>
        /// <param name="sender">Button.</param>
        /// <param name="e">Clicked. </param>
        private void BackwardButton_Click(object sender, EventArgs e)
        {
            KeyboardState key = Keyboard.GetState();
            if (key.GetPressedKeys().Length > 0)
            {
                Player.PlayerMovementKeys.Down = key.GetPressedKeys()[0];
            }
        }

        /// <summary>
        /// Sets the key binding.
        /// </summary>
        /// <param name="sender">Button.</param>
        /// <param name="e">Clicked. </param>
        private void RightButton_Click(object sender, EventArgs e)
        {
            KeyboardState key = Keyboard.GetState();
            if (key.GetPressedKeys().Length > 0)
            {
                Player.PlayerMovementKeys.Right = key.GetPressedKeys()[0];
            }
        }

        /// <summary>
        /// Sets the key binding.
        /// </summary>
        /// <param name="sender">Button.</param>
        /// <param name="e">Clicked. </param>
        private void LeftButton_Click(object sender, EventArgs e)
        {
            KeyboardState key = Keyboard.GetState();
            if (key.GetPressedKeys().Length > 0)
            {
                Player.PlayerMovementKeys.Left = key.GetPressedKeys()[0];
            }
        }

        /// <summary>
        /// Sets the key binding.
        /// </summary>
        /// <param name="sender">Button.</param>
        /// <param name="e">Clicked. </param>
        private void SprintButton_Click(object sender, EventArgs e)
        {
            KeyboardState key = Keyboard.GetState();
            if (key.GetPressedKeys().Length > 0)
            {
                Player.PlayerMovementKeys.Sprint = key.GetPressedKeys()[0];
            }
        }
    }
}
