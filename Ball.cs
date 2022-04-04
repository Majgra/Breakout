using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class Ball
    {
        const int radius = 15;
        private const int startPositionX = 390;
        private const int startPositionY = 300;
        private Rectangle ball = new Rectangle(startPositionX, startPositionY, radius, radius);
        private int x_speed = 4;
        private int y_speed = 4;

        private SpriteBatch spriteBatch;
        private Texture2D texture;

        public Ball(SpriteBatch spriteBatch, Texture2D texture)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;
        }

        public Rectangle GetRectangle()
        {
            return ball;
        }

        public void Update(int windowWidth)
        {
            Move();

            if(ball.Y <= 0)
            {
                y_speed *= -1;
            }
            if(ball.X <= 0 || ball.X >= windowWidth - radius)
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

        public void Draw()
        {
            spriteBatch.Draw(texture, ball, Color.Gray);
        }
    }
}