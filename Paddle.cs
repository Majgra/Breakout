using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class Paddle
    {
        private Rectangle paddle = new Rectangle(325, 440, 150, 10);
        
        private SpriteBatch spriteBatch; 
        private Texture2D texture;

        public Paddle(SpriteBatch spriteBatch, Texture2D texture)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;
        }

        public Rectangle GetRectangle()
        {
            return paddle;
        }

        //Flyttar paddeln
        public void Update(KeyboardState kstate, int windowWidth)
        {
            if (kstate.IsKeyDown(Keys.D) || kstate.IsKeyDown(Keys.Right))
            {
                paddle.X += 6;
            }
            else if (kstate.IsKeyDown(Keys.A) || kstate.IsKeyDown(Keys.Left))
            {
                paddle.X -= 6;
            }
            else
            {
                return;
            }

            //Gör så att paddeln inte åker utanför 
            if (paddle.X > windowWidth - paddle.Width)
            {
                paddle.X = windowWidth - paddle.Width;
            }
            if (paddle.X < 0)
            {
                paddle.X = 0;
            }
        }

        public void Draw()
        {
            spriteBatch.Draw(texture, paddle, Color.White);
        }
    }
}
