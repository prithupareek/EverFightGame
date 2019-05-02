using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Diagnostics;

using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;

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

        Texture2D bulletTexture;

        KeyboardState pastKey;

        int p1DelayCounter;
        int p2DelayCounter;

        int splashDelayCounter;

        Texture2D arrow;

        LevelManager levelManager;

        enum GameMode
        {
            splashScreen, menu, playing, paused, p1Win, p2Win, instructions
        }

        GameMode mode;

        public enum ButtonType
        {
            START, RESTART, LEFT_ARROW, RIGHT_ARROW, UP_ARROW, QUIT, DOWN_ARROW, MENU, PLAY
        }

        List<Button> menuButtons;
        List<Button> pauseButtons;
        Button winScreenRestartButton;

        Texture2D p1Win;
        Texture2D p2Win;
        Texture2D splashImage;
        Texture2D backgroundImage;
        Texture2D instructionsImage;

        Song menuBackgroundMusic;
        Song gameplayBackgroundMusic;

        enum MusicState
        {
            PLAYING, NOTPLAYING
        }

        MusicState musicState;

        //list of charecter colors
        List<string> playerColors;

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
            arrow = Content.Load<Texture2D>("arrow-indicator");

            //win images
            p1Win = Content.Load<Texture2D>("p1Win");
            p2Win = Content.Load<Texture2D>("p2Win");

            //splash image
            splashImage = Content.Load<Texture2D>("splash");

            //background image
            backgroundImage = Content.Load<Texture2D>("cave-background");

            //instructions image
            instructionsImage = Content.Load<Texture2D>("instructions");

            levelManager = new LevelManager(windowSize, backgroundImage);
            levelManager.LoadLevel(Content);

            mode = GameMode.splashScreen;

            splashDelayCounter = 0;

            //startButton = new Button(new Vector2(windowSize.X/2 - 125, 400), ButtonType.START);
            //startButton.LoadContent(Content);

            p1.pointer.LoadContent(Content);
            p2.pointer.LoadContent(Content);

            menuButtons = new List<Button>();
            menuButtons.Add(new Button(new Vector2 (windowSize.X/2, 300), ButtonType.START));
            //arrows for changing charecter skins
            menuButtons.Add(new Button(new Vector2(100, 600), ButtonType.LEFT_ARROW, 1));
            menuButtons.Add(new Button(new Vector2(300, 600), ButtonType.RIGHT_ARROW, 1));
            menuButtons.Add(new Button(new Vector2(windowSize.X - 300, 600), ButtonType.LEFT_ARROW, 2));
            menuButtons.Add(new Button(new Vector2(windowSize.X - 100, 600), ButtonType.RIGHT_ARROW, 2));
        
            foreach (Button button in menuButtons) {
                button.LoadContent(Content);
            }

            pauseButtons = new List<Button>();
            pauseButtons.Add(new Button(new Vector2(windowSize.X / 2, 300), ButtonType.MENU));
            pauseButtons.Add(new Button(new Vector2(windowSize.X / 2, 500), ButtonType.PLAY));

            foreach(Button button in pauseButtons)
            {
                button.LoadContent(Content);
            }

            winScreenRestartButton = new Button(new Vector2(windowSize.X / 2, 300), ButtonType.MENU);
            winScreenRestartButton.LoadContent(Content);

            //background music stuff
            menuBackgroundMusic = Content.Load<Song>("warrior-song");
            gameplayBackgroundMusic = Content.Load<Song>("gameplay-background-song");
            musicState = MusicState.NOTPLAYING;

            //player colors list
            playerColors = new List<string>() {
                "Biege",
                "Blue",
                "Green",
                "Pink",
                "Yellow"
            };
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

            if (mode == GameMode.splashScreen)
            {
                if (splashDelayCounter < 200)
                {
                    splashDelayCounter++;
                }
                else
                {
                    mode = GameMode.instructions;
                }
            }
            else if (mode == GameMode.instructions)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A) || GamePad.GetState(PlayerIndex.Two).IsButtonDown(Buttons.A))
                {
                    mode = GameMode.menu;
                }
            }
            else if (mode == GameMode.menu)
            {

                //background music
                if (musicState == MusicState.NOTPLAYING)
                {
                    MediaPlayer.Play(menuBackgroundMusic);
                    musicState = MusicState.PLAYING;
                    MediaPlayer.IsRepeating = true;
                }

                p1.pointer.Update();
                p2.pointer.Update();

                foreach (Button button in menuButtons)
                {
                    if (p1.pointer.boundingBox.Intersects(button.boundingBox))
                    {

                        if ((Keyboard.GetState().IsKeyDown(Keys.B) && pastKey.IsKeyUp(Keys.B))|| (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A) && p1.pastButton.IsButtonUp(Buttons.A)))
                        {
                            if (button.buttonType == ButtonType.START)
                            {
                                mode = GameMode.playing;
                                MediaPlayer.Stop();
                                musicState = MusicState.NOTPLAYING;

                            }
                            if (button.buttonType == ButtonType.RIGHT_ARROW && button.playerNumber == 1)
                            {
                                if (p1.playerColor == playerColors[4])
                                {
                                    p1.playerColor = playerColors[0];
                                }
                                else
                                {
                                    p1.playerColor = playerColors[playerColors.IndexOf(p1.playerColor) + 1];
                                }

                                p1.LoadContent(Content);
                            }
                            if (button.buttonType == ButtonType.LEFT_ARROW && button.playerNumber == 1)
                            {
                                if (p1.playerColor == playerColors[0])
                                {
                                    p1.playerColor = playerColors[4];
                                }
                                else
                                {
                                    p1.playerColor = playerColors[playerColors.IndexOf(p1.playerColor) - 1];
                                }

                                p1.LoadContent(Content);
                            }
                        }
                        
                    }
                    if (p2.pointer.boundingBox.Intersects(button.boundingBox))
                    {
                        

                        if ((Keyboard.GetState().IsKeyDown(Keys.L) && pastKey.IsKeyUp(Keys.L))|| (GamePad.GetState(PlayerIndex.Two).IsButtonDown(Buttons.A) && p2.pastButton.IsButtonUp(Buttons.A)))
                        {
                            if (button.buttonType == ButtonType.START)
                            {
                                mode = GameMode.playing;
                                MediaPlayer.Stop();
                                musicState = MusicState.NOTPLAYING;

                            }
                            if (button.buttonType == ButtonType.RIGHT_ARROW && button.playerNumber == 2)
                            {
                                if (p2.playerColor == playerColors[4])
                                {
                                    p2.playerColor = playerColors[0];
                                }
                                else
                                {
                                    p2.playerColor = playerColors[playerColors.IndexOf(p2.playerColor) + 1];
                                }

                                p2.LoadContent(Content);
                            }
                            if (button.buttonType == ButtonType.LEFT_ARROW && button.playerNumber == 2)
                            {
                                if (p2.playerColor == playerColors[0])
                                {
                                    p2.playerColor = playerColors[4];
                                }
                                else
                                {
                                    p2.playerColor = playerColors[playerColors.IndexOf(p2.playerColor) - 1];
                                }

                                p2.LoadContent(Content);
                            }
                        }
                    }
                }

                pastKey = Keyboard.GetState();
                p1.pastButton = GamePad.GetState(PlayerIndex.One);
                p2.pastButton = GamePad.GetState(PlayerIndex.Two);
            }

            else if (mode == GameMode.paused)
            {
                //Do something here
                p1.pointer.Update();
                p2.pointer.Update();

                foreach (Button button in pauseButtons)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.B) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A))
                    {
                        if (p1.pointer.boundingBox.Intersects(button.boundingBox))
                        {
                            if (button.buttonType == ButtonType.PLAY)
                            {
                                mode = GameMode.playing;
                            }
                            else if (button.buttonType == ButtonType.MENU)
                            {
                                mode = GameMode.menu;
                                p1.pointer.position = new Vector2(100, 10);
                                p2.pointer.position = new Vector2(windowSize.X - 100, 10);

                                //restart the game
                                levelManager = new LevelManager(windowSize, backgroundImage);
                                levelManager.LoadLevel(Content);
                                p1.hasDied = false;
                                p2.hasDied = false;
                                p1.position = new Vector2(100, 10);
                                p2.position = new Vector2(windowSize.X - 125-72, 10);
                                MediaPlayer.Stop();
                                musicState = MusicState.NOTPLAYING;

                            }
                        }

                    }
                    if (p2.pointer.boundingBox.Intersects(button.boundingBox))
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.L) || GamePad.GetState(PlayerIndex.Two).IsButtonDown(Buttons.A))
                        {
                            if (button.buttonType == ButtonType.PLAY)
                            {
                                mode = GameMode.playing;
                            }
                            else if (button.buttonType == ButtonType.MENU)
                            {
                                mode = GameMode.menu;
                                p1.pointer.position = new Vector2(100, 10);
                                p2.pointer.position = new Vector2(windowSize.X - 100, 10);

                                //restart the game
                                levelManager = new LevelManager(windowSize, backgroundImage);
                                levelManager.LoadLevel(Content);
                                p1.hasDied = false;
                                p2.hasDied = false;
                                p1.position = new Vector2(100, 10);
                                p2.position = new Vector2(windowSize.X - 125-72, 10);
                                MediaPlayer.Stop();
                                musicState = MusicState.NOTPLAYING;
                            }
                        }
                    }
                }
            }
            else if (mode == GameMode.playing)
            {

                //background music
                if (musicState == MusicState.NOTPLAYING)
                {
                    MediaPlayer.Play(gameplayBackgroundMusic);
                    musicState = MusicState.PLAYING;
                    MediaPlayer.IsRepeating = true;
                }


                //platform update
                if (levelManager.activeLevel < 5 && levelManager.activeLevel > -1)
                {
                    p1.Update(levelManager.levels[levelManager.activeLevel].platforms);
                    p2.Update(levelManager.levels[levelManager.activeLevel].platforms);
                }

                //on keypress for bullets
                if ((Keyboard.GetState().IsKeyDown(Keys.V) && pastKey.IsKeyUp(Keys.V)) || (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.RightTrigger) && p1.pastButton.IsButtonUp(Buttons.RightTrigger)))   //p1
                {

                    p1.weapon.projectiles.Add(new Projectile(p1.weapon.position, 1, p1.weapon.rotation, windowSize, bulletTexture, p1.spriteTexture, p1.weapon.movingRight));
                }
                if (Keyboard.GetState().IsKeyDown(Keys.K) && pastKey.IsKeyUp(Keys.K) || (GamePad.GetState(PlayerIndex.Two).IsButtonDown(Buttons.RightTrigger) && p2.pastButton.IsButtonUp(Buttons.RightTrigger)))   //p2
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
                        break;
                    }

                    foreach (Platform platform in levelManager.levels[levelManager.activeLevel].platforms)
                    {
                        if (p1.weapon.projectiles[i].boundingBox.Intersects(platform.boundingBox))
                        {
                            p1.weapon.projectiles.RemoveAt(i);
                            break;
                            
                        }
                    }
                }
                for (int i = 0; i < p2.weapon.projectiles.Count; i++)
                {
                    if (p2.weapon.projectiles[i].position.Y >= windowSize.Y - bulletTexture.Height * 0.1)
                    {
                        p2.weapon.projectiles.RemoveAt(i);
                        break;
                    }

                    foreach (Platform platform in levelManager.levels[levelManager.activeLevel].platforms)
                    {
                        if (p2.weapon.projectiles[i].boundingBox.Intersects(platform.boundingBox))
                        {
                            p2.weapon.projectiles.RemoveAt(i);
                            break;

                        }
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
                if (p2.hasDied && levelManager.activeLevel > 4)
                {
                    mode = GameMode.p1Win;
                }
                else if (p1.hasDied && levelManager.activeLevel < 0)
                {
                    mode = GameMode.p2Win;
                }

                //pause game
                if (Keyboard.GetState().IsKeyDown(Keys.Space) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.Start))
                {
                    mode = GameMode.paused;
                }
            }
            else if (mode == GameMode.p1Win || mode == GameMode.p2Win)
            {
                p1.pointer.Update();
                p2.pointer.Update();

                //Do something here
                if (Keyboard.GetState().IsKeyDown(Keys.B) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A))
                {
                    if (p1.pointer.boundingBox.Intersects(winScreenRestartButton.boundingBox))
                    {

                        if (winScreenRestartButton.buttonType == ButtonType.MENU)
                        {
                            mode = GameMode.menu;
                            p1.pointer.position = new Vector2(100, 10);
                            p2.pointer.position = new Vector2(windowSize.X - 100, 10);

                            //restart the game
                            levelManager = new LevelManager(windowSize, backgroundImage);
                            levelManager.LoadLevel(Content);
                            p1.hasDied = false;
                            p2.hasDied = false;
                            p1.position = new Vector2(100, 10);
                            p2.position = new Vector2(windowSize.X - 125 - 72, 10);
                            MediaPlayer.Stop();
                            musicState = MusicState.NOTPLAYING;

                        }
                    }

                }
                if (p2.pointer.boundingBox.Intersects(winScreenRestartButton.boundingBox))
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.L) || GamePad.GetState(PlayerIndex.Two).IsButtonDown(Buttons.A))
                    {

                        if (winScreenRestartButton.buttonType == ButtonType.MENU)
                        {
                            mode = GameMode.menu;
                            p1.pointer.position = new Vector2(100, 10);
                            p2.pointer.position = new Vector2(windowSize.X - 100, 10);

                            //restart the game
                            levelManager = new LevelManager(windowSize, backgroundImage);
                            levelManager.LoadLevel(Content);
                            p1.hasDied = false;
                            p2.hasDied = false;
                            p1.position = new Vector2(100, 10);
                            p2.position = new Vector2(windowSize.X - 125 - 72, 10);
                            MediaPlayer.Stop();
                            musicState = MusicState.NOTPLAYING;
                        }
                    }
                }
            }
 
            else
            {
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
                    GraphicsDevice.Clear(Color.Black);
                    spriteBatch.Begin();
                    spriteBatch.Draw(splashImage, new Vector2(windowSize.X / 2 - splashImage.Width / 2, windowSize.Y / 2 - splashImage.Height / 2));
                    spriteBatch.End();
                    break;

                case GameMode.instructions:
                    GraphicsDevice.Clear(Color.Black);
                    spriteBatch.Begin();
                    spriteBatch.Draw(instructionsImage, new Vector2(0,0));
                    spriteBatch.End();
                    break;

                case GameMode.menu:
                    GraphicsDevice.Clear(Color.Crimson);
                    spriteBatch.Begin();
                    spriteBatch.Draw(backgroundImage, new Vector2(0, 0));
                    spriteBatch.Draw(p1.spriteTexture, new Vector2(160, 590));
                    spriteBatch.Draw(p2.spriteTexture, new Vector2(windowSize.X - 160 - p2.spriteTexture.Width, 590));
                    spriteBatch.End();

                    foreach (Button button in menuButtons)
                    {
                        button.Draw(spriteBatch);
                    }

                    p1.pointer.Draw(spriteBatch);
                    p2.pointer.Draw(spriteBatch);
                    break;

                case GameMode.playing:

                    GraphicsDevice.Clear(Color.CornflowerBlue);

                    levelManager.DrawLevel(spriteBatch);

                    if (p1.hasDied)
                    {
                        spriteBatch.Begin();
                        spriteBatch.Draw(arrow, new Vector2(100, 100), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                        spriteBatch.End();
                    }
                    else if (p2.hasDied)
                    {
                        spriteBatch.Begin();
                        spriteBatch.Draw(arrow, new Vector2(windowSize.X - 100 - arrow.Width, 100), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, 0f);
                        spriteBatch.End();
                    }

                    if (levelManager.activeLevel > -1 && levelManager.activeLevel < 5)
                    {
                        // TODO: Add your drawing code here
                        p1.Draw(spriteBatch);
                        p2.Draw(spriteBatch);
                    }

                    foreach (Projectile projectile in p1.weapon.projectiles) projectile.Draw(spriteBatch);
                    foreach (Projectile projectile in p2.weapon.projectiles) projectile.Draw(spriteBatch);

                    break;

                case GameMode.paused:
                    GraphicsDevice.Clear(Color.ForestGreen);
                    spriteBatch.Begin();
                    spriteBatch.Draw(backgroundImage, new Vector2(0, 0));
                    spriteBatch.End();

                    foreach (Button button in pauseButtons) button.Draw(spriteBatch);
                    p1.pointer.Draw(spriteBatch);
                    p2.pointer.Draw(spriteBatch);

                    break;

                case GameMode.p1Win:
                    GraphicsDevice.Clear(Color.White);
                    spriteBatch.Begin();
                    spriteBatch.Draw(backgroundImage, new Vector2(0, 0));
                    spriteBatch.End();

                    spriteBatch.Begin();
                    spriteBatch.Draw(p1Win, new Vector2(windowSize.X/2 - p1Win.Width/2, 100));
                    spriteBatch.End();

                    winScreenRestartButton.Draw(spriteBatch);

                    p1.pointer.Draw(spriteBatch);
                    p2.pointer.Draw(spriteBatch);

                    break;

                case GameMode.p2Win:
                    GraphicsDevice.Clear(Color.White);
                    spriteBatch.Begin();
                    spriteBatch.Draw(backgroundImage, new Vector2(0, 0));
                    spriteBatch.End();

                    spriteBatch.Begin();
                    spriteBatch.Draw(p2Win, new Vector2(windowSize.X / 2 - p1Win.Width / 2, 100));
                    spriteBatch.End();

                    break;

                default:
                    break;
            }

            base.Draw(gameTime);
        }
    }
}
