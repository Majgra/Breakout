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
        private SpriteFont gameOverFont;

        private Paddle paddle;

        private BallSpawner ballSpawner;
        private BoxSpawner boxSpawner;
        private PowerUpsSpawner powerUpsSpawner;

        private bool gameStarted = false;
        private bool gameOver = false;
        private int gameLevel = 1;

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

            gameOverFont = Content.Load<SpriteFont>("gameOver");

            ballSpawner = new BallSpawner(spriteBatch, circle);
            boxSpawner = new BoxSpawner(spriteBatch, pixel);
            boxSpawner.Spawn(gameLevel);
            powerUpsSpawner = new PowerUpsSpawner(spriteBatch, pixel);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState kstate = Keyboard.GetState();
            
            if(kstate.IsKeyDown(Keys.Space))
            {
                if(gameOver)
                {
                    gameOver = false;
                }
                else
                {
                    gameStarted = true;
                }
            }
            
            if(gameStarted)
            {
                ballSpawner.Update(gameTime, Window.ClientBounds.Width);

                gameStarted = ballSpawner.CheckBalls(paddle, Window.ClientBounds.Height, boxSpawner, powerUpsSpawner);
                gameOver = !gameStarted;

                if(boxSpawner.AllBoxesDestroyed())
                {
                   gameStarted = false;
                   gameLevel++;
                   boxSpawner.Spawn(gameLevel); 
                   powerUpsSpawner.Reset();
                   ballSpawner.Reset();
                }
                else if(gameOver)
                {
                    gameLevel = 1;
                    boxSpawner.Spawn(gameLevel);
                    powerUpsSpawner.Reset();
                    ballSpawner.Reset();
                }
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
            if(!gameOver)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);

                spriteBatch.Begin();
                paddle.Draw();

                boxSpawner.Draw();
                powerUpsSpawner.Draw();
                ballSpawner.Draw();
                
                spriteBatch.End();
            }
            else
            {
                GraphicsDevice.Clear(Color.Black);

                spriteBatch.Begin();

                spriteBatch.DrawString(gameOverFont, "GAME OVER", new Vector2(40, 160), Color.Red);
            
                spriteBatch.End();
            }
           
           
            base.Draw(gameTime);
        }
    }
}