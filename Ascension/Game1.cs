﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Threading;

namespace Ascension
{
    public class Game1 : Game
    {
        Texture2D ballTexture;
        Vector2 ballPosition;
        float ballSpeed;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 800;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            ballPosition = new Vector2(_graphics.PreferredBackBufferWidth / 4, _graphics.PreferredBackBufferHeight / 2);
            ballSpeed = 100f;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            ballTexture = Content.Load<Texture2D>("ball");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                                 Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            // The time since Update was called last.
            float updatedBallSpeed = ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds * 3;

           
            this.BallMovement(updatedBallSpeed);
            //
            this.StayInBorder();
            //
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Blue);


            _spriteBatch.Begin();

            _spriteBatch.Draw(
                ballTexture,
                ballPosition,
                null,
                Color.White,
                0f,
                new Vector2(ballTexture.Width / 2, ballTexture.Height / 2),
                new Vector2((float)0.25, (float)0.25),
                SpriteEffects.None,
                0f
            );

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void BallMovement(float updatedBallSpeed)
        {
            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.W))
            {
                ballPosition.Y -= updatedBallSpeed;
            }

            if (kstate.IsKeyDown(Keys.S))
            {
                ballPosition.Y += updatedBallSpeed;
            }

            if (kstate.IsKeyDown(Keys.A))
            {
                ballPosition.X -= updatedBallSpeed;
            }

            if (kstate.IsKeyDown(Keys.D))
            {
                ballPosition.X += updatedBallSpeed;
            }

            if (kstate.IsKeyDown(Keys.LeftShift))
            {
                ballSpeed = 200f;
            }
            else
            {
                ballSpeed = 100f;
            }
        }

        private void StayInBorder()
        {
            int xPadding = _graphics.PreferredBackBufferWidth / 30;
            int yPadding = _graphics.PreferredBackBufferHeight / 30;
            if (ballPosition.X > (_graphics.PreferredBackBufferWidth / 2) - ballTexture.Width / 2)
            {
                ballPosition.X = (_graphics.PreferredBackBufferWidth / 2) - ballTexture.Width / 2;
            }
            else if (ballPosition.X < (ballTexture.Width / 2) + xPadding)
            {
                ballPosition.X = (ballTexture.Width / 2) + xPadding;
            }

            if (ballPosition.Y > _graphics.PreferredBackBufferHeight - ballTexture.Height / 2 - yPadding)
            {
                ballPosition.Y = _graphics.PreferredBackBufferHeight - ballTexture.Height / 2 - yPadding;
            }
            else if (ballPosition.Y < (ballTexture.Height / 2 + yPadding))
            {
                ballPosition.Y = (ballTexture.Height / 2 + yPadding);
            }
        }
    }
}
