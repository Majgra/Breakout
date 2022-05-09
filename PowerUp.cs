using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class PowerUp
    {
        private Rectangle powerUp;

        private int x_speed = 0;
        private int y_speed = 2;

        private SpriteBatch spriteBatch;
        private Texture2D texture;

        public PowerUp(SpriteBatch spriteBatch, Texture2D texture, Box removed)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;
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

        public void Draw()
        {
            spriteBatch.Draw(texture, powerUp, Color.Gray);
        }
    }
}