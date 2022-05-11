using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class Ball
    {
        const int radius = 15;
        private int startPositionX = 390;
        private int startPositionY = 300;
        private Rectangle ball;

        private int x_speed = 3;
        private int y_speed = 3;

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

        public Rectangle GetRectangle()
        {
            return ball;
        }

        public void Update(int windowWidth)
        {
            Move();

            OriginalSpeed();

            if(ball.Y <= 0)
            {
                y_speed *= -1;
            }
            if(ball.X <= 0 || ball.X >= windowWidth - radius)
            {
                x_speed *= -1;
            }
        }

         public void BounceUp()
        {
            if(y_speed > 0)
            {
                y_speed *= -1;
            } 
        }

        public void BounceLeft()
        {
            if(x_speed > 0)
            {
                x_speed *= -1;
            } 
        }

        public void BounceRigth()
        {
            if(x_speed < 0)
            {
                x_speed *= -1;
            } 
        }

        public void BounceVertical()
        {
            y_speed *= -1;
        }

        public void BounceHorizontal()
        {
            x_speed *= -1;
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

        private void Move()
        {
            ball.X += x_speed;
            ball.Y += y_speed;
        }

        public void SlowDown()
        {
           ChangeSpeed(1);

           counter = 0;
        }

        int counter = 0;
        private void OriginalSpeed()
        {
            counter++;

            if(counter > 500)
            {
                ChangeSpeed(3);
            }
        }

        private void ChangeSpeed(int speed)
        {
            if(x_speed < 0)
            {
                x_speed = -speed;
            }
            else
            {
                x_speed = speed;
            }

            if(y_speed < 0)
            {
                y_speed = -speed;
            }
            else
            {
                y_speed = speed;
            }
        }

        public void Draw()
        {
            spriteBatch.Draw(texture, ball, Color.Gray);
        }
    }
}