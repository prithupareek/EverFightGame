using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Diagnostics;

using System.Collections.Generic;

namespace EverFight
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        SpriteBatch spriteBatch;
        GraphicsDeviceManager graphics;

        ///initialize Players
        Player p1;
        Player p2;

        Vector2 windowSize;

        //bullets texture.. don't know if there is a better way to do this
        Texture2D bulletTexture;

        //pastkey for bullets firing
        KeyboardState pastKey;

        int p1DelayCounter;
        int p2DelayCounter;

        int splashDelayCounter;

        Texture2D arrow;

        LevelManager levelManager;

        enum GameMode
        {
            splashScreen, menu, playing, paused, p1Win, p2Win
        }

        GameMode mode;

        public enum ButtonType
        {
            START, RESTART, LEFT_ARROW, RIGHT_ARROW, UP_ARROW, QUIT, DOWN_ARROW
        }

        List<Button> menuButtons;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //makes sure the screen is always the same size
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 1280;

            //Debug.WriteLine(graphics.PreferredBackBufferHeight + ", " + graphics.PreferredBackBufferWidth);

            if (graphics.IsFullScreen)
            {
                graphics.ToggleFullScreen();
            }
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here


            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            windowSize = new Vector2(graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);

            /// Debug.WriteLine(height);

            // Constructors for Player class
            p1 = new Player(1, windowSize);
            p2 = new Player(2, windowSize);


            // TODO: use this.Content to load your game content here
            p1.LoadContent(Content);
            p2.LoadContent(Content);

            //load content for the projectiles
            bulletTexture = Content.Load<Texture2D>("projectile");


            //arrow sprite
            arrow = Content.Load<Texture2D>("arrow");

            levelManager = new LevelManager(windowSize);
            levelManager.LoadLevel(Content);

            mode = GameMode.splashScreen;

            splashDelayCounter = 0;

            startButton = new Button(new Vector2(windowSize.X/2 - 125, 400), ButtonType.START);
            startButton.LoadContent(Content);

            p1.pointer.LoadContent(Content);
            p2.pointer.LoadContent(Content);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here

            switch (mode)
            {
                case GameMode.splashScreen:
                    if (splashDelayCounter < 200)
                    {
                        splashDelayCounter++;
                    }
                    else
                    {
                        mode = GameMode.menu;
                    }
                    break;

                case GameMode.menu:
                    //Do something here


                    //testing code
                    if (Keyboard.GetState().IsKeyDown(Keys.T) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A))
                    {
                        mode = GameMode.playing;
                    }

                    p1.pointer.Update();
                    p2.pointer.Update();
                    break;

                case GameMode.paused:
                    //Do something here
                    break;

                case GameMode.playing:
                    //platform update
                    if (levelManager.activeLevel < 3 && levelManager.activeLevel > -1)
                    {
                        p1.Update(levelManager.levels[levelManager.activeLevel].platforms);
                        p2.Update(levelManager.levels[levelManager.activeLevel].platforms);
                    }

                    //on keypress for bullets
                    if ((Keyboard.GetState().IsKeyDown(Keys.V) && pastKey.IsKeyUp(Keys.V)) || (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.Y) && p1.pastButton.IsButtonUp(Buttons.Y)))   //p1
                    {

                        p1.weapon.projectiles.Add(new Projectile(p1.weapon.position, 1, p1.weapon.rotation, windowSize, bulletTexture, p1.spriteTexture, p1.weapon.movingRight));
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.K) && pastKey.IsKeyUp(Keys.K) || (GamePad.GetState(PlayerIndex.Two).IsButtonDown(Buttons.Y) && p2.pastButton.IsButtonUp(Buttons.Y)))   //p2
                    {
                        p2.weapon.projectiles.Add(new Projectile(p2.weapon.position, 2, p2.weapon.rotation, windowSize, bulletTexture, p2.spriteTexture, p2.weapon.movingRight));
                    }

                    pastKey = Keyboard.GetState();
                    p1.pastButton = GamePad.GetState(PlayerIndex.One);
                    p2.pastButton = GamePad.GetState(PlayerIndex.Two);


                    //delete the bullets if they hit the ground
                    for (int i = 0; i < p1.weapon.projectiles.Count; i++)
                    {
                        if (p1.weapon.projectiles[i].position.Y >= windowSize.Y - bulletTexture.Height * 0.1)
                        {
                            p1.weapon.projectiles.RemoveAt(i);
                        }
                    }
                    for (int i = 0; i < p2.weapon.projectiles.Count; i++)
                    {
                        if (p2.weapon.projectiles[i].position.Y >= windowSize.Y - bulletTexture.Height * 0.1)
                        {
                            p2.weapon.projectiles.RemoveAt(i);
                        }
                    }

                    //update the bullets
                    foreach (Projectile projectile in p1.weapon.projectiles) projectile.Update();
                    foreach (Projectile projectile in p2.weapon.projectiles) projectile.Update();



                    //projectile - player collision detection
                    foreach (Projectile projectile in p1.weapon.projectiles)
                    {
                        if (projectile.boundingBox.Intersects(p2.boundingBox))
                        {
                            p1.weapon.projectiles.Remove(projectile);

                            p2.Respawn();

                            p2DelayCounter = 0;

                            p1.hasDied = false;


                            break;
                        }
                    }
                    foreach (Projectile projectile in p2.weapon.projectiles)
                    {
                        if (projectile.boundingBox.Intersects(p1.boundingBox))
                        {
                            p2.weapon.projectiles.Remove(projectile);


                            p1.Respawn();

                            p1DelayCounter = 0;

                            p2.hasDied = false;

                            break;
                        }
                    }


                    //respawn timer
                    if (p2.position.Y < 0 && p2.hasDied)
                    {
                        p2DelayCounter++;
                    }
                    if (p1.position.Y < 0 && p1.hasDied)
                    {
                        p1DelayCounter++;
                    }

                    if (p1DelayCounter == 200)
                    {
                        p1DelayCounter = 0;
                        p1.velocity.Y = 0;
                        p1.hasJumped = true;
                    }
                    if (p2DelayCounter == 200)
                    {
                        p2DelayCounter = 0;
                        p2.velocity.Y = 0;
                        p2.hasJumped = true;
                    }

                    //player reaches endzone of current level
                    if (p1.position.X >= windowSize.X - p1.spriteTexture.Width)
                    {
                        if (p2.hasDied)
                        {
                            p1.position.X = 0;
                            p2.Respawn();
                            levelManager.activeLevel++;
                        }
                        else
                        {
                            p1.position.X = windowSize.X - p1.spriteTexture.Width;
                        }
                    }
                    if (p1.position.X <= 0)
                    {
                        p1.position.X = 0;
                    }
                    if (p2.position.X <= 0)
                    {
                        if (p1.hasDied)
                        {
                            p2.position.X = windowSize.X - p2.spriteTexture.Width;
                            p1.Respawn();
                            levelManager.activeLevel--;
                        }
                        else
                        {
                            p2.position.X = 0;
                        }
                    }
                    if (p2.position.X >= windowSize.X - p2.spriteTexture.Width)
                    {
                        p2.position.X = windowSize.X - p2.spriteTexture.Width;
                    }

                    //Game Over Scenario
                    if (p2.hasDied && levelManager.activeLevel < 3)
                    {
                        mode = GameMode.p1Win;
                    }
                    else if (p1.hasDied && levelManager.activeLevel > -1)
                    {
                        mode = GameMode.p2Win;
                    }

                    //pause game
                    if (Keyboard.GetState().IsKeyDown(Keys.Space) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.Start))
                    {
                        mode = GameMode.paused;
                    }
                    break;

                case GameMode.p1Win:
                    //Do something here
                    break;

                case GameMode.p2Win:
                    //Do something here
                    break;

                default:
                    break;
            }

            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Q) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.Back))
            {
                this.Exit();
            }


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            switch (mode)
            {
                case GameMode.splashScreen:
                    GraphicsDevice.Clear(Color.Azure);
                    break;

                case GameMode.menu:
                    GraphicsDevice.Clear(Color.Crimson);
                    startButton.Draw(spriteBatch);
                    p1.pointer.Draw(spriteBatch);
                    p2.pointer.Draw(spriteBatch);
                    break;

                case GameMode.playing:

                    GraphicsDevice.Clear(Color.CornflowerBlue);

                    if (levelManager.activeLevel > -1 && levelManager.activeLevel < 3)
                    {
                        // TODO: Add your drawing code here
                        p1.Draw(spriteBatch);
                        p2.Draw(spriteBatch);
                    }

                    foreach (Projectile projectile in p1.weapon.projectiles) projectile.Draw(spriteBatch);
                    foreach (Projectile projectile in p2.weapon.projectiles) projectile.Draw(spriteBatch);

                    levelManager.DrawLevel(spriteBatch);

                    break;

                case GameMode.paused:
                    GraphicsDevice.Clear(Color.ForestGreen);
                    
                    break;

                case GameMode.p1Win:

                    spriteBatch.Begin();
                    spriteBatch.Draw(arrow, new Vector2(windowSize.X - 100 - arrow.Width, 100), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, 0f);
                    spriteBatch.End();
                    break;

                case GameMode.p2Win:

                    spriteBatch.Begin();
                    spriteBatch.Draw(arrow, new Vector2(100, 100), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    spriteBatch.End();
                    break;

                default:
                    break;
            }

            base.Draw(gameTime);
        }
    }
}
