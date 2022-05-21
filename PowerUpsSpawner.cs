using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace Breakout
{
    //Har hand om powerupsen
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

        //Spawnar en powerup där boxen försvann
        public void Spawn(Box removed)
        {
            if(removed != null)
            {
                //En tredjedels chans att en powerup spawnas
                Random rand = new Random();
                int chanse = rand.Next(1, 6);
                if(chanse == 2)
                {
                    powerUps.Add(new PowerUp(spriteBatch, texture, removed, "Slow"));
                }
                else if(chanse == 3)
                {
                    powerUps.Add(new PowerUp(spriteBatch, texture, removed, "Extra"));
                }
            }
        }

        public void Reset()
        {
            powerUps.Clear();
        }

        //Kollar om powerupen träffar paddeln
        public PowerUp CheckPowerUp(Paddle paddle)
        {
            foreach(PowerUp powerUp in powerUps)
            {
                if(powerUp.GetRectangle().Intersects(paddle.GetRectangle()))
                {
                    powerUps.Remove(powerUp);

                    BreakoutGame.AddPoints(10);

                    return powerUp;
                }
            }

            return null;
        }

        public void Update()
        {
            foreach(PowerUp powerUp in powerUps)
            {
                powerUp.Update();
            }
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