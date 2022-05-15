using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Breakout
{
    public class BoxSpawner
    {
        public List<Box> boxes = new List<Box>();

        private const int height = 100;
        private const int indent = 7;
        private const int spacing = 5;

        private SpriteBatch spriteBatch;
        private Texture2D texture;

        public bool AllBoxesDestroyed()
        {
            return boxes.Count == 0;
        }

        public BoxSpawner(SpriteBatch spriteBatch, Texture2D texture)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;
        }

        //Spawnar boxar, en rad per level upp till 9. Flyttar upp raderna för level 3 och 6
        public void Spawn(int level)
        {
            boxes.Clear();

            int newHeight  = height;
            if(level >= 3)
            {
                newHeight = height - 35 ;
            }
            if(level >= 6)
            {
                newHeight = height - 35 * 2;
            }

            if(level > 9)
            {
                //max 9 rader
                level = 9;
            }                

            //Skapar en rad
            for(int i = 0; i < level; i++)
            {
                //Skapar boxar för raden
                for(int j = 0; j < 10; j++)
          	    {
		            boxes.Add(new Box(spriteBatch, texture, new Vector2(indent + j * (Box.Width + spacing), newHeight + i * (spacing + Box.Height))));
                }  
            }
        }

        public Box CheckBall(Ball ball)
        {
            foreach(Box box in boxes)
            {
                //kolla om bollen har träffat en box
                if(ball.GetRectangle().Intersects(box.GetRectangle()))
                {
                    boxes.Remove(box);

                    if(box.IntersectsEdge(ball))
                    {
                        ball.BounceHorizontal();
                    }   
                    else
                    {
                        ball.BounceVertical();
                    }

                    BreakoutGame.AddPoints(5);
                    
                    return box;
                }
            }
            
            return null;
        }

        public void Draw()
        {
            foreach(Box box in boxes)
            {
                box.Draw();
            }
        }
    }
}