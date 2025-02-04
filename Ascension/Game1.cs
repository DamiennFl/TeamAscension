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

        Rectangle borderRect = new Rectangle(40, 40, 460, 720);
        int borderWidth = 4;
        Color borderColor = Color.Black;

        float ballSpeed;
        Texture2D borderTexture;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1000;
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
            _spriteBatch.Draw(borderTexture, new Rectangle(borderRect.X, borderRect.Y, borderRect.Width, borderWidth), borderColor);
            _spriteBatch.Draw(borderTexture, new Rectangle(borderRect.X, borderRect.Y + borderRect.Height - borderWidth, borderRect.Width, borderWidth), borderColor);
            _spriteBatch.Draw(borderTexture, new Rectangle(borderRect.X, borderRect.Y, borderWidth, borderRect.Height), borderColor);
            _spriteBatch.Draw(borderTexture, new Rectangle(borderRect.X + borderRect.Width - borderWidth, borderRect.Y, borderWidth, borderRect.Height), borderColor);


            _spriteBatch.Draw(
                ballTexture,
                ballPosition,
                null,
                Color.White,
                0f,
                new Vector2(ballTexture.Width / 4, ballTexture.Height / 4),
                new Vector2((float)0.25, (float)0.25),
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
            int radius = ballTexture.Width / 8;

            // Left border
            if (ballPosition.X - radius < borderRect.X)
            {
                ballPosition.X = borderRect.X + radius;
            }
            // Right border
            else if (ballPosition.X + radius > borderRect.X + borderRect.Width - (2 *borderWidth))
            {
                ballPosition.X = borderRect.X + borderRect.Width - (2 * borderWidth) - radius;
            }

            // Top border
            if (ballPosition.Y - radius < borderRect.Y)
            {
                ballPosition.Y = borderRect.Y + radius;
            }
            // Bottom border
            else if (ballPosition.Y + radius > borderRect.Y + borderRect.Height - (2 * borderWidth))
            {
                ballPosition.Y = borderRect.Y + borderRect.Height - (2 * borderWidth) - radius;
            }
        }
    }
}
 