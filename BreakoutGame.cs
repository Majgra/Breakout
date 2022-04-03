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
        private BoxSpawner boxSpawner;
        private Ball ball;

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

            circle = new CircleCreator().Create(GraphicsDevice, 15);

            paddle = new Paddle(spriteBatch, pixel);
            boxSpawner = new BoxSpawner(spriteBatch, pixel);
            ball = new Ball(spriteBatch, circle);
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
                 ball.Update(Window.ClientBounds.Width);
            }

            paddle.Update(kstate, Window.ClientBounds.Width);

            CheckBall();

            base.Update(gameTime);
        }

        private void CheckBall()
        {
            //kolla om ball rör paddle och om ball är utanför skärmen.
            if(ball.GetRectangle().Intersects(paddle.GetRectangle()))
            {
                if(paddle.IntersectsEdge(ball))
                {
                    ball.BounceHorizontal();
                }
                else
                {
                    ball.BounceVertical();
                }
            }
            
            if(ball.OutOfBounds(Window.ClientBounds.Height))
            {
                gameStarted = false;
            }
            
            boxSpawner.CheckBall(ball);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            paddle.Draw();
            boxSpawner.Draw();
            ball.Draw();
            spriteBatch.End();
           
            base.Draw(gameTime);
        }
    }
}
