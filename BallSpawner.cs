using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Breakout
{
    //Har hand om bollarna
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

        //Återställer bollen/bollarna om man dött eller klarat banan
        public void Reset()
        {
            //Tar bort alla utom en boll
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

        //powerup sakna ner
        public void SlowDown()
        {
            foreach(Ball ball in balls)
            {
                ball.SlowDown();
            }
        }

        //powerup extra boll
        public void ExtraBall(PowerUp powerUp)
        {
            Ball ball = new Ball(spriteBatch, circle, powerUp.GetRectangle().X, powerUp.GetRectangle().Y);

            ball.BounceUp();
            balls.Add(ball);
        }

        //Kollar om bollen träffar något
        //Returnerar false om alla bollar trillat ut
        public bool CheckBalls(Paddle paddle, int windowHeight, BoxSpawner boxSpawner, PowerUpsSpawner powerUpsSpawner, HeartSpawner heartSpawner)
        {
            //Loppar baklängelse för att vi ska ta bort bollar
            for(int i = balls.Count - 1; i >= 0; i--)
            {
                Ball ball = balls[i];

                if(ball.GetRectangle().Intersects(paddle.GetRectangle()))
                {
                    ball.BounceOnPaddle(paddle);
                }
            
                if(ball.OutOfBounds(windowHeight))
                {
                    //Tar inte bort sista bollen för nästa startning
                    if(balls.Count == 1)
                    {
                        return false;
                    }
                    else
                    {
                        balls.Remove(ball);
                    }
                }

                //kontrollerar om bollen träffar en box
                Box removedBox = boxSpawner.CheckBall(ball);
            
                powerUpsSpawner.Spawn(removedBox);

                heartSpawner.CheckHeart(ball);
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