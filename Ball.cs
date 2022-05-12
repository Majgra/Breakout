using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class Ball
    {
        const int radius = 15;
        private Vector2 startDirection = new Vector2(0.707f, -0.707f);
        private int startPositionX = 390;
        private int startPositionY = 300;
        private Rectangle ball;

        public float speed = 500;
        public Vector2 direction = new Vector2(0.707f, -0.707f);


        // private int x_speed = 4;
        // private int y_speed = 4;

        private SpriteBatch spriteBatch;
        private Texture2D texture;

        public Ball(SpriteBatch spriteBatch, Texture2D texture)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;

            ball = new Rectangle(startPositionX, startPositionY, radius, radius);
        }

        public Ball(SpriteBatch spriteBatch, Texture2D texture, int startPositionX, int startPositionY)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;

            ball = new Rectangle(startPositionX, startPositionY, radius, radius);
        }

        public void Reset()
        {
            ball = new Rectangle(startPositionX, startPositionY, radius, radius);
            //bollen ska inte vara slowed
            counter = 500;
            //bollen ska åka åt rätt håll
            direction = startDirection;
        }

        public Rectangle GetRectangle()
        {
            return ball;
        }

        public void Update(GameTime gameTime, int windowWidth)
        {    
            Move(gameTime);

            OriginalSpeed();

            if(ball.Y <= 0)
            {
                direction.Y *= -1;
               // y_speed *= -1;
            }
            if(ball.X <= 0 || ball.X >= windowWidth - radius)
            {
                direction.X *= -1;
                //x_speed *= -1;
            }
        }

        public void BounceOnPaddle(Paddle paddle){
            //här måste vi fixa hur bollen ska studsa
            BounceUp();

            if(GetRectangle().Center.X < paddle.GetRectangle().Center.X)
            {
                BounceLeft();
            }
            else
            {
                BounceRigth();
            }
        }

        public void BounceUp()
        {
            // if(y_speed > 0)
            // {
            //     y_speed *= -1;
            // } 

            if(direction.Y > 0)
            {
                direction.Y *= -1;
            } 
        }

        public void BounceDown()
        {
            // if(y_speed < 0)
            // {
            //     y_speed *= -1;
            // } 

            if(direction.Y < 0)
            {
                direction.Y *= -1;
            } 
        }

        public void BounceLeft()
        {
            // if(x_speed > 0)
            // {
            //     x_speed *= -1;
            // } 

            if(direction.X > 0)
            {
                direction.X *= -1;
            } 
        }

        public void BounceRigth()
        {
            // if(x_speed < 0)
            // {
            //     x_speed *= -1;
            // } 

             if(direction.X < 0)
            {
                direction.X *= -1;
            } 
        }

        public void BounceVertical()
        {
           //y_speed *= -1;
            
            direction.Y *= -1;
        }

        public void BounceHorizontal()
        {
            direction.X *= -1;
          //  x_speed *= -1;
        }

        public bool OutOfBounds(int windowHeight)
        {
            if(ball.Y > windowHeight)
            {
                ball.X = startPositionX;
                ball.Y = startPositionY;
               
                return true;
            }

            return false;
        }

        private void Move(GameTime gameTime)
        {
        //     ball.X += x_speed;
        //     ball.Y += y_speed;

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 position = direction * speed * deltaTime;

            ball.X += (int)position.X;
            ball.Y += (int)position.Y;
        }

        public void SlowDown()
        {
          // ChangeSpeed(1);
            speed = 250;
            counter = 0;
        }

        int counter = 0;
        private void OriginalSpeed()
        {
            counter++;

            if(counter > 500)
            {
                speed = 500;
                //ChangeSpeed(3);
            }
        }

        // private void ChangeSpeed(int speed)
        // {
        //     if(x_speed < 0)
        //     {
        //         x_speed = -speed;
        //     }
        //     else
        //     {
        //         x_speed = speed;
        //     }

        //     if(y_speed < 0)
        //     {
        //         y_speed = -speed;
        //     }
        //     else
        //     {
        //         y_speed = speed;
        //     }
        // }

        public void Draw()
        {
            spriteBatch.Draw(texture, ball, Color.Gray);
        }
    }
}