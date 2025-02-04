using Microsoft.Xna.Framework;
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

        Rectangle borderRect = new Rectangle(50, 50, 200, 150); // x, y, width, height
        int borderWidth = 5;
        Color borderColor = Color.Black;

        float ballSpeed;
        Texture2D borderTexture;

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
            borderTexture = new Texture2D(GraphicsDevice, 100, 100);
      

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            ballTexture = Content.Load<Texture2D>("ball");
            borderTexture = new Texture2D(GraphicsDevice, 1, 1);
            borderTexture.SetData(new[] { Color.White });
            //borderTexture = new Texture2D(GraphicsDevice, 1, 1);
            //borderTexture.SetData(new[] { Color.Black });
            //_spriteBatch.Draw(borderTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.Black);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                                 Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            // The time since Update was called last.
            float updatedBallSpeed = ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds * 5;

           
            this.BallMovement(updatedBallSpeed);
            //
            this.StayInBorder();
            //
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            
          


            _spriteBatch.Begin();
            //_spriteBatch.Draw(ballTexture, ballPosition, Color.White);
            // Draw top border
            _spriteBatch.Draw(borderTexture, new Rectangle(borderRect.X, borderRect.Y, borderRect.Width, borderWidth), borderColor);
            // Draw bottom border
            _spriteBatch.Draw(borderTexture, new Rectangle(borderRect.X, borderRect.Y + borderRect.Height - borderWidth, borderRect.Width, borderWidth), borderColor);
            // Draw left border
            _spriteBatch.Draw(borderTexture, new Rectangle(borderRect.X, borderRect.Y, borderWidth, borderRect.Height), borderColor);
            // Draw right border
            _spriteBatch.Draw(borderTexture, new Rectangle(borderRect.X + borderRect.Width - borderWidth, borderRect.Y, borderWidth, borderRect.Height), borderColor);


            _spriteBatch.Draw(
                ballTexture,
                ballPosition,
                null,
                Color.White,
                0f,
                new Vector2(ballTexture.Width / 2, ballTexture.Height / 2),
                1,
                SpriteEffects.None,
                0f
            ); 
            // TODO: Add your drawing code here

            
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
                ballSpeed = 150f;
            }
            else
            {
                ballSpeed = 100f;
            }
        }

        private void StayInBorder()
        {
            int xPadding = _graphics.PreferredBackBufferWidth / 30;
            int yPadding = _graphics.PreferredBackBufferHeight / 20;

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
