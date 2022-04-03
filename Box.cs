using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class Box
    {
        public static int Height = 20;
        public static int Width = 50;

        private Rectangle box;
        private Rectangle boxRightEdge;
        private Rectangle boxLeftEdge;

        private SpriteBatch spriteBatch;
        private Texture2D texture;

        public Box(SpriteBatch spriteBatch, Texture2D texture, Vector2 position)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;

            box = new Rectangle((int)position.X, (int)position.Y, Width, Height);
            boxRightEdge = new Rectangle((int)box.X + box.Width, (int)box.Y + 3, 0, box.Height - 6);
            boxLeftEdge = new Rectangle((int)box.X, (int)box.Y + 3, 0, box.Height - 6);
        } 

        public bool IntersectsEdge(Ball ball)
        {
            return ball.GetRectangle().Intersects(boxRightEdge) || ball.GetRectangle().Intersects(boxLeftEdge);
        }

        public Rectangle GetRectangle()
        {
            return box;
        }

        public void Draw()
        {
            spriteBatch.Draw(texture, box, Color.PaleGreen);
        }

         public void Remove()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, box, Color.CornflowerBlue);
            spriteBatch.End();
        }
    }
}