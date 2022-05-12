using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Breakout
{
    public class BallSpawner
    {
        private List<Ball> balls = new List<Ball>();

        private SpriteBatch spriteBatch;
        private Texture2D circle;

        public BallSpawner(SpriteBatch spriteBatch, Texture2D circle)
        {
            this.spriteBatch = spriteBatch;
            this.circle = circle;
            balls.Add(new Ball(spriteBatch, circle));
        }

        public void Reset()
        {
            for(int i = balls.Count - 1; i > 0; i--)
            {
                balls.RemoveAt(i);
            }

            balls[0].Reset();
        }

        public void Update(GameTime gameTime, int windowWidth)
        {
            foreach(Ball ball in balls)
            {
                ball.Update(gameTime, windowWidth);
            }
        }

        public void SlowDown()
        {
            foreach(Ball ball in balls)
            {
                ball.SlowDown();
            }
        }

        public void Split(PowerUp powerUp)
        {
            Ball ball = new Ball(spriteBatch, circle, powerUp.GetRectangle().X, powerUp.GetRectangle().Y);

            ball.BounceUp();
            balls.Add(ball);
        }

        //Kollar om bollen träffar något
        //returnerar false om alla bollar trillat ut
        public bool CheckBalls(Paddle paddle, int windowHeight, BoxSpawner boxSpawner, PowerUpsSpawner powerUpsSpawner)
        {
            for(int i = balls.Count - 1; i >= 0; i--)
            {
                Ball ball = balls[i];

                if(ball.GetRectangle().Intersects(paddle.GetRectangle()))
                {
                    ball.BounceOnPaddle(paddle);

                    // ball.BounceUp();

                    // if(ball.GetRectangle().Center.X < paddle.GetRectangle().Center.X)
                    // {
                    //     ball.BounceLeft();
                    // }
                    // else
                    // {
                    //     ball.BounceRigth();
                    // }
                }
            
                if(ball.OutOfBounds(windowHeight))
                {
                    //en boll ska alltid finnas
                    if(balls.Count == 1)
                    {
                        return false;
                    }
                    else
                    {
                        balls.Remove(ball);
                    }
                }
            
                Box removed = boxSpawner.CheckBall(ball);
            
                powerUpsSpawner.Spawn(removed);
            }

            return true;
        }

        public void Draw()
        {
            foreach(Ball ball in balls)
            {
                ball.Draw();
            }
        }
    }
}