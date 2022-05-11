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
                    powerUps.Add(new PowerUp(spriteBatch, texture, removed, true));
                }
                else if(chanse == 3)
                {
                    powerUps.Add(new PowerUp(spriteBatch, texture, removed, false));
                }
            }
        }

        public void Update()
        {
            foreach(PowerUp powerUp in powerUps)
            {
                powerUp.Update();
            }
        }

        public PowerUp CheckPowerUp(Paddle paddle)
        {
            foreach(PowerUp powerUp in powerUps)
            {
                if(powerUp.GetRectangle().Intersects(paddle.GetRectangle()))
                {
                    powerUps.Remove(powerUp);

                    return powerUp;
                }
            }

            return null;
        }

         public void Draw()
        {
            foreach(PowerUp powerUp in powerUps)
            {
                powerUp.Draw();
            }
        }
    }
}