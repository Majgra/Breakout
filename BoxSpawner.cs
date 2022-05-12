using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Breakout
{
    public class BoxSpawner
    {
        private List<Box> boxes = new List<Box>();

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

        public void Spawn(int level)
        {
            boxes.Clear();
            
            for(int i = 0; i < level; i++)
            {
                for(int j = 0; j < 10; j++)
          	    {
		            boxes.Add(new Box(spriteBatch, texture, new Vector2(indent + j * (Box.Width + spacing), height + i * (spacing + Box.Height))));
                }  
            }
        }

        public void Draw()
        {
            foreach(Box box in boxes)
            {
                box.Draw();
            }
        }

        public Box CheckBall(Ball ball)
        {
            foreach(Box box in boxes)
            {
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

                    return box;
                }
            }
            
            return null;
        }
    }
}