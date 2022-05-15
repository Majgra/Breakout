using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout
{
    //ideer lånade från https://www.informit.com/articles/article.aspx?p=2180417&seqNum=2
    public class Ball
    {
        const int radius = 15;
        private int startPositionX = 390;
        private int startPositionY = 420;
        private Rectangle ball;

        public float speed = 400;
        public Vector2 direction;
        private Vector2 startDirection = new Vector2(0.707f, -0.707f);

        private SpriteBatch spriteBatch;
        private Texture2D texture;

        public Ball(SpriteBatch spriteBatch, Texture2D texture)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;

            ball = new Rectangle(startPositionX, startPositionY, radius, radius);
            direction = startDirection;
        }

        //Extra bollen spawnas på powerUpsens position
        public Ball(SpriteBatch spriteBatch, Texture2D texture, int startPositionX, int startPositionY)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;

            ball = new Rectangle(startPositionX, startPositionY, radius, radius);
            direction = startDirection;
        }

        public void Reset()
        {
            ball = new Rectangle(startPositionX, startPositionY, radius, radius);

            speed = 400;
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

            Slowedtime();

            if(ball.Y <= 0)
            {
                direction.Y *= -1;
            }
            if(ball.X <= 0 || ball.X >= windowWidth - radius)
            {
                direction.X *= -1;
            }
        }

        //Tagit från länken ovan
        public void BounceOnPaddle(Paddle paddle)
        {
            // Reflect based on which part of the paddle is hit
                   
            // Distance from the leftmost to rightmost part of the paddle
            float dist = paddle.GetRectangle().Width + radius * 2;
            // Where within this distance the ball is at
            float ballLocation = ball.X - (paddle.GetRectangle().X - radius - paddle.GetRectangle().Width / 2);
            // Percent between leftmost and rightmost part of paddle
            float pct = ballLocation / dist;
            
            // By default, set the normal to "up"
            Vector2 normal = -1.0f * Vector2.UnitY;
            if (pct < 0.33f)
            {
                normal = new Vector2(-0.196f, -0.981f);
            }
            else if (pct > 0.66f)
            {
                normal = new Vector2(0.196f, -0.981f);
            }

            direction = Vector2.Reflect(direction, normal);
            
            //Tvinga den att studsa uppåt
            BounceUp();
            
            //Studsa åt höger om träff på högersidan av paddeln och tvärtom
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
            if(direction.Y > 0)
            {
                direction.Y *= -1;
            } 
        }

        private void BounceLeft()
        {
            if(direction.X > 0)
            {
                direction.X *= -1;
            } 
        }

        private void BounceRigth()
        {
            if(direction.X < 0)
            {
                direction.X *= -1;
            } 
        }
        
        public void BounceVertical()
        {            
            direction.Y *= -1;
        }

        public void BounceHorizontal()
        {
            direction.X *= -1;
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
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 position = direction * speed * deltaTime;

            ball.X += (int)position.X;
            ball.Y += (int)position.Y;
        }

        public void SlowDown()
        {
            speed = 250;
            counter = 0;
        }

        int counter = 0;
        private void Slowedtime()
        {
            counter++;

            if(counter >= 500)
            {
                speed = 400;
            }
        }

        public void Draw()
        {
            spriteBatch.Draw(texture, ball, Color.Gray);
        }
    }
}