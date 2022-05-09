using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace Breakout
{
    public class PowerUpsSpawner
    {
        private List<PowerUp> powerUps = new List<PowerUp>();

        private SpriteBatch spriteBatch;
        private Texture2D texture;

        public PowerUpsSpawner(SpriteBatch spriteBatch, Texture2D texture)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;
        }

        public void Spawn(Box removed)
        {
            if(removed != null)
            {
                Random rand = new Random();
                int chanse = rand.Next(1, 6);
                if(chanse == 2)
                {
                    powerUps.Add(new PowerUp(spriteBatch, texture, removed));
                }
            }
        }

        public void Draw()
        {
            foreach(PowerUp powerUp in powerUps)
            {
                powerUp.Draw();
            }
        }

        public void Update()
        {
            foreach(PowerUp powerUp in powerUps)
            {
                powerUp.Update();
            }
        }

        public void CheckBall(Ball ball)
        {
            // foreach(Box box in boxes)
            // {
            //     if(ball.GetRectangle().Intersects(box.GetRectangle()))
            //     {
            //         boxes.Remove(box);
            //         box.Remove();

            //         if(box.IntersectsEdge(ball))
            //         {
            //             ball.BounceHorizontal();
            //         }   
            //         else
            //         {
            //             ball.BounceVertical();
            //         }

            //         break;
            //     }
            // }
        }
    }
}