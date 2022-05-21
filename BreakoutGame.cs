using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    //Huvudprogrammet
    public class BreakoutGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Texture2D pixel;
        private Texture2D heart;
        private Texture2D leftRightButtons;
        private Texture2D spaceButton;
        private Texture2D circle;

        private SpriteFont bigFont;
        private SpriteFont smallFont;
        private SpriteFont miniFont;
        private SpriteFont microFont;

        private Rectangle startButton = new Rectangle(340, 251, 125, 55);
        private Rectangle menuButton = new Rectangle(370, 350, 69, 30);

        private Paddle paddle;
        private BallSpawner ballSpawner;
        private BoxSpawner boxSpawner;
        private PowerUpsSpawner powerUpsSpawner;
        private HeartSpawner heartSpawner;

        //Skärmar som ska visas
        private bool gameMenuScreen = true;
        private bool gameScreen = false;
        private bool gameOverScreen = false;

        //Variabler som styr spelet
        private bool gameStarted = false;
        private int gameLevel = 1;
        private static int gameLives = 3;
        private static int gamePoints = 0;

        public static void AddPoints(int points)
        {
            gamePoints += points;
        }

        public static void AddLife()
        {
            if(gameLives < 3)
            {
               gameLives++;
            } 
        }

        public BreakoutGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Bilder
            pixel = Content.Load<Texture2D>("pixel");
            heart = Content.Load<Texture2D>("Heart");
            leftRightButtons = Content.Load<Texture2D>("LeftRightButtons");
            spaceButton = Content.Load<Texture2D>("SpaceButton");

            //Fonter
            bigFont = Content.Load<SpriteFont>("bigFont");
            smallFont = Content.Load<SpriteFont>("smallFont");
            miniFont = Content.Load<SpriteFont>("miniFont");
            microFont = Content.Load<SpriteFont>("microFont");
            
            //Spelplanen
            circle = CircleCreator.Create(GraphicsDevice, 15);
            paddle = new Paddle(spriteBatch, pixel);
            ballSpawner = new BallSpawner(spriteBatch, circle);
            boxSpawner = new BoxSpawner(spriteBatch, pixel);
            boxSpawner.Spawn(gameLevel);
            powerUpsSpawner = new PowerUpsSpawner(spriteBatch, pixel);
            heartSpawner = new HeartSpawner(spriteBatch, heart);
            heartSpawner.Spawn(boxSpawner, gameLevel);
        }

        bool mousePressed;
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            mousePressed = mousePressed || Mouse.GetState().LeftButton == ButtonState.Pressed;

            if(gameMenuScreen)
            {
                //Ger en fade effekt på Breakout texten
                Fade(gameTime);

                //Används för att det ska bli ett klick med musen på knappen
                if(mousePressed && Mouse.GetState().LeftButton == ButtonState.Released)
                {
                    if(startButton.Contains(Mouse.GetState().Position))
                    {
                        gameMenuScreen = false;
                        gameScreen = true;

                        gamePoints = 0;
                    }

                    mousePressed = false;
                }

                return;
            }

            if(gameOverScreen)
            {
                //Används för att det ska bli ett klick med musen på knappen
                if(mousePressed && Mouse.GetState().LeftButton == ButtonState.Released)
                {
                    if(menuButton.Contains(Mouse.GetState().Position))
                    {
                        gameOverScreen = false;
                        gameMenuScreen = true;
                    }

                    mousePressed = false;
                }

                return;
            }
            mousePressed = false;

            //Om mellanslag tryckts ner startars bollen/spelet
            KeyboardState kstate = Keyboard.GetState();
            if(kstate.IsKeyDown(Keys.Space))
            {
                gameStarted = true;
            }
            
            if(gameStarted)
            {
                //Flyttar bollarna
                ballSpawner.Update(gameTime, Window.ClientBounds.Width);

                //kollar om bollen träffar nåt eller utanför spelplanen
                bool lifeLost = !ballSpawner.CheckBalls(paddle, Window.ClientBounds.Height, boxSpawner, powerUpsSpawner, heartSpawner);
                //Alla bollar utanför, förlorar ett liv
                if(lifeLost)
                {
                    gameLives--;
                    
                    gameStarted = false;
                    ballSpawner.Reset();
                    powerUpsSpawner.Reset();
                    
                    //Liven slut, spelet är över
                    if(gameLives == 0)
                    {
                        gameScreen = false;
                        gameOverScreen = true;                        
                    }
                }
                
                if(boxSpawner.AllBoxesDestroyed())
                {
                    //ladda nya leveln
                    gameStarted = false;
                    AddPoints(25);
                    gameLevel++;
                    boxSpawner.Spawn(gameLevel); 
                    powerUpsSpawner.Reset();
                    ballSpawner.Reset();
                    heartSpawner.Spawn(boxSpawner, gameLevel);
                }
                //Visar gameover skärmen och nollställer spelet. 
                else if(gameOverScreen)
                {
                    gameLevel = 1;
                    gameLives = 3;
                    boxSpawner.Spawn(gameLevel);
                    powerUpsSpawner.Reset();
                    ballSpawner.Reset();
                }
            }

            paddle.Update(kstate, Window.ClientBounds.Width);

            powerUpsSpawner.Update();
            
            //kontrollerar om powerupen har träffat paddeln
            PowerUp powerUp = powerUpsSpawner.CheckPowerUp(paddle);

            if(powerUp != null && powerUp.PowerUpType == "Slow")
            {
                ballSpawner.SlowDown();
            }
            else if(powerUp != null && powerUp.PowerUpType == "Extra")
            {
                ballSpawner.ExtraBall(powerUp);
            }

            base.Update(gameTime);
        }

        //Taget från http://www.xnadevelopment.com/tutorials/fadeinfadeout/FadeInFadeOut.shtml
        int mAlphaValue = 1;
        int mFadeIncrement = 3;
        double mFadeDelay = .01;
        private void Fade(GameTime gameTime)
        {
             mFadeDelay -= gameTime.ElapsedGameTime.TotalSeconds;
 
            //If the Fade delays has dropped below zero, then it is time to 
            //fade in/fade out the image a little bit more.
            if (mFadeDelay <= 0)
            {
                //Reset the Fade delay
                mFadeDelay = .01;
 
                //Increment/Decrement the fade value for the image
                mAlphaValue += mFadeIncrement;
 
                //If the AlphaValue is equal or above the max Alpha value or
                //has dropped below or equal to the min Alpha value, then 
                //reverse the fade
                if (mAlphaValue >= 255 || mAlphaValue <= 0)
                {
                    mFadeIncrement *= -1;
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            if(gameMenuScreen)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
                spriteBatch.Begin();

                spriteBatch.DrawString(bigFont, "Breakout", new Vector2(160, 100), new Color((byte)MathHelper.Clamp(mAlphaValue, 0, 0), (byte)MathHelper.Clamp(mAlphaValue, 35, 0), (byte)MathHelper.Clamp(mAlphaValue, 100, 255)));
                spriteBatch.Draw(pixel, startButton, Color.CornflowerBlue);
                spriteBatch.DrawString(smallFont, "Start", new Vector2(345, 250), Color.RoyalBlue);
                
                spriteBatch.Draw(leftRightButtons, new Vector2(190, 395), Color.Black);
                spriteBatch.Draw(spaceButton, new Vector2(439, 395), Color.Black);
                spriteBatch.DrawString(microFont, "Press space to start ball", new Vector2(453, 385), Color.Black);
                spriteBatch.DrawString(microFont, "Move left or right with", new Vector2(215, 385), Color.Black);

                spriteBatch.End();
            }
            else if(gameScreen)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);

                spriteBatch.Begin();
                paddle.Draw();
                if(gameLives == 3)
                {
                    spriteBatch.Draw(heart, new Vector2(5, 450), Color.Red);
                    spriteBatch.Draw(heart, new Vector2(35, 450), Color.Red);
                    spriteBatch.Draw(heart, new Vector2(65, 450), Color.Red);
                }
                else if(gameLives == 2)
                {
                    spriteBatch.Draw(heart, new Vector2(5, 450), Color.Red);
                    spriteBatch.Draw(heart, new Vector2(35, 450), Color.Red);
                    spriteBatch.Draw(heart, new Vector2(65, 450), Color.Gray);
                }
                else
                {
                    spriteBatch.Draw(heart, new Vector2(5, 450), Color.Red);
                    spriteBatch.Draw(heart, new Vector2(35, 450), Color.Gray);
                    spriteBatch.Draw(heart, new Vector2(65, 450), Color.Gray);
                }

                spriteBatch.DrawString(miniFont, gamePoints.ToString(), new Vector2(100, 450), Color.Black);

                //flyttar texten när tiopotensen ökar
                int numberCount = gameLevel.ToString().Length;
                spriteBatch.DrawString(miniFont, "Level " + gameLevel.ToString(), new Vector2(721 - 14 * numberCount, 450), Color.Black);

                heartSpawner.Draw();
                boxSpawner.Draw();
                powerUpsSpawner.Draw();
                ballSpawner.Draw();
                
                spriteBatch.End();
            }
            else if(gameOverScreen)
            {
                GraphicsDevice.Clear(Color.Black);

                spriteBatch.Begin();

                spriteBatch.DrawString(bigFont, "GAME OVER", new Vector2(40, 100), Color.Red);
                spriteBatch.Draw(pixel, menuButton, Color.Black);
                spriteBatch.DrawString(smallFont, "Your score: " + gamePoints, new Vector2(225, 253), Color.Red);
                spriteBatch.DrawString(miniFont, "Menu", new Vector2(370, 350), Color.Red);
                
                spriteBatch.End();
            }
            base.Draw(gameTime);
        }
    }
}