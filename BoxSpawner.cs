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
        private const int indent = 17;
        private const int spacing = 5;

        private SpriteBatch spriteBatch;
        private Texture2D texture;

        public BoxSpawner(SpriteBatch spriteBatch, Texture2D texture)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;

            for(int i = 0; i < 5; i++)
            {
                AddRow(i);
            } 
        }

        private void AddRow(int index)
        {
            for(int i = 0; i < 14; i++)
            {
                boxes.Add(new Box(spriteBatch, texture, new Vector2(indent + i * (Box.Width + spacing), height + index * (spacing + Box.Height))));
            }
        }

        public void Draw()
        {
            foreach(Box box in boxes)
            {
                box.Draw();
            }
        }

        public void CheckBall(Ball ball)
        {
            foreach(Box box in boxes)
            {
                if(ball.GetRectangle().Intersects(box.GetRectangle()))
                {
                    boxes.Remove(box);
                    box.Remove();

                    if(box.IntersectsEdge(ball))
                    {
                        ball.BounceHorizontal();
                    }   
                    else
                    {
                        ball.BounceVertical();
                    }
                    break;
                }
            }
        }
    }
}