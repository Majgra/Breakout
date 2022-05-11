using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class BreakoutGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Texture2D pixel;
        private Texture2D circle;

        private Paddle paddle;

        private BallSpawner ballSpawner;
        private BoxSpawner boxSpawner;
        private PowerUpsSpawner powerUpsSpawner;

        private bool gameStarted = false;

        public BreakoutGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            pixel = Content.Load<Texture2D>("pixel");

            circle = CircleCreator.Create(GraphicsDevice, 15);

            paddle = new Paddle(spriteBatch, pixel);

            ballSpawner = new BallSpawner(spriteBatch, circle);
            boxSpawner = new BoxSpawner(spriteBatch, pixel);
            powerUpsSpawner = new PowerUpsSpawner(spriteBatch, pixel);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState kstate = Keyboard.GetState();
            
            if(kstate.IsKeyDown(Keys.Space))
            {
                gameStarted = true;
            }
            
            if(gameStarted)
            {
                ballSpawner.Update(Window.ClientBounds.Width);

                gameStarted = ballSpawner.CheckBalls(paddle, Window.ClientBounds.Height, boxSpawner, powerUpsSpawner);
            }

            paddle.Update(kstate, Window.ClientBounds.Width);

            powerUpsSpawner.Update();

            PowerUp powerUp = powerUpsSpawner.CheckPowerUp(paddle);

            if(powerUp != null && powerUp.IsSlowdown)
            {
                ballSpawner.SlowDown();
            }
            else if(powerUp != null && !powerUp.IsSlowdown)
            {
                ballSpawner.Split(powerUp);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            paddle.Draw();
            boxSpawner.Draw();
            powerUpsSpawner.Draw();
            ballSpawner.Draw();
            spriteBatch.End();
           
            base.Draw(gameTime);
        }
    }
}