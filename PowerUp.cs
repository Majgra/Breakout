using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout
{
    public class PowerUp
    {
        private Rectangle powerUp;

        private int x_speed = 0;
        private int y_speed = 2;
        private SpriteBatch spriteBatch;
        private Texture2D texture;

        public string PowerUpType;

        public PowerUp(SpriteBatch spriteBatch, Texture2D texture, Box removed, string powerUpType)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;
            this.PowerUpType = powerUpType;

            powerUp = new Rectangle(removed.GetRectangle().Center.X, removed.GetRectangle().Center.Y, 15, 15);
        }

        public Rectangle GetRectangle()
        {
            return powerUp;
        }

        public void Update()
        {
            powerUp.X += x_speed;
            powerUp.Y += y_speed;
        }

        //Ritar powerupen i regnbågens färger
        int colors = 0;
        int ColorSpeed = 10;
        public void Draw()
        {
            if (colors >= 0 && colors < ColorSpeed)
            {
                spriteBatch.Draw(texture, powerUp, Color.Red);
            }
            else if (colors >= ColorSpeed && colors < 2 * ColorSpeed)
            {
                spriteBatch.Draw(texture, powerUp, Color.Orange);
            }
            else if (colors >= 2 * ColorSpeed && colors < 3 * ColorSpeed)
            {
                spriteBatch.Draw(texture, powerUp, Color.Yellow);
            }
            else if (colors >= 3 * ColorSpeed && colors < 4 * ColorSpeed)
            {
                spriteBatch.Draw(texture, powerUp, Color.Green);
            }
            else if (colors >= 4 * ColorSpeed && colors < 5 * ColorSpeed)
            {
                spriteBatch.Draw(texture, powerUp, Color.Blue);
            }
            else if (colors >= 5 * ColorSpeed && colors < 6 * ColorSpeed)
            {
                spriteBatch.Draw(texture, powerUp, Color.Indigo);
            }
            else if (colors >= 6 * ColorSpeed && colors < 7 * ColorSpeed)
            {
                spriteBatch.Draw(texture, powerUp, Color.Violet);
            }
            else
            {
                colors = 0;
            }
            colors++;
        }
    }
}