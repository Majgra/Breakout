// using Microsoft.Xna.Framework;
// using Microsoft.Xna.Framework.Graphics;
// using Microsoft.Xna.Framework.Input;

// namespace Breakout
// {
//     public class PowerUps
//     {
//         private Rectangle powerUps = new Rectangle(100,100,15,15)
// ;
//         private int x_speed = 0;
//         private int y_speed = 2;

//         private SpriteBatch spriteBatch;
//         private Texture2D texture;

//         public PowerUps(SpriteBatch spriteBatch, Texture2D texture)
//         {
//             this.spriteBatch = spriteBatch;
//             this.texture = texture;
//         }

//         public Rectangle GetRectangle()
//         {
//             return powerUps;
//         }
//         public void ChangeRectangle()
//         {
//             powerUps.X += x_speed;
//             powerUps.Y += y_speed;
//         }

//         public void Draw()
//         {
//             spriteBatch.Draw(texture, powerUps, Color.Gray);
//         }
//     }
// }