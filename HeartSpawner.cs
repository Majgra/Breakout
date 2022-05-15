using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Breakout
{
    public class HeartSpawner
    {
        private SpriteBatch spriteBatch;
        private Texture2D texture;
        private Vector2 heartPosition;
        private Rectangle heartRectangle;

        private bool heartSpawned = false;

        public HeartSpawner(SpriteBatch spriteBatch, Texture2D texture)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;
        }

        //Spawnas bakom en random box för level 4 och uppåt
        public void Spawn(BoxSpawner boxSpawner, int gameLevel)
        {
            if(gameLevel > 3)
            {
                Random rand = new Random();
                int number = rand.Next(0, boxSpawner.boxes.Count);
                Box hiddenBehindBox = boxSpawner.boxes[number];

                heartPosition = new Vector2(hiddenBehindBox.GetRectangle().Center.X - texture.Width / 2 , hiddenBehindBox.GetRectangle().Center.Y - texture.Height / 2);
                //hjärtats träffyta måste vara mindre än boxen
                heartRectangle = new Rectangle(hiddenBehindBox.GetRectangle().Center.X -5, hiddenBehindBox.GetRectangle().Center.Y - 5, 10, 10);

                heartSpawned = true;
            }
        }

        //Kollar om bollen träffar hjärtat
        public void CheckHeart(Ball ball)
        {
            if(heartSpawned && ball.GetRectangle().Intersects(heartRectangle))
            {
                BreakoutGame.AddLife();
                heartSpawned = false;
            }
        }

        public void Draw()
        {
            if(heartSpawned)
            {
                spriteBatch.Draw(texture, heartPosition, Color.Red);
            }
        }
    }
}