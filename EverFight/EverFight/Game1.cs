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

        //arraylist to store bullets
        List<Projectile> p1Projectiles;
        List<Projectile> p2Projectiles;

        //bullets texture.. don't know if there is a better way to do this
        Texture2D bulletTexture;

        //pastkey for bullets firing
        KeyboardState pastKey;

        Delay respawnDelay1;
        Delay respawnDelay2;

        Texture2D arrow;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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

            // Constructors for the projectile arraylists
            p1Projectiles = new List<Projectile>();
            p2Projectiles = new List<Projectile>();

            // TODO: use this.Content to load your game content here
            p1.LoadContent(Content);
            p2.LoadContent(Content);

            //load content for the projectiles
            bulletTexture = Content.Load<Texture2D>("projectile");

            //respawn delay
            respawnDelay1 = new Delay(3f);
            respawnDelay2 = new Delay(3f);

            //arrow sprite
            arrow = Content.Load<Texture2D>("arrow");

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
            
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                this.Exit();
            }

            p1.Update();
            p2.Update();

            //on keypress for bullets
            if (Keyboard.GetState().IsKeyDown(Keys.V) && pastKey.IsKeyUp(Keys.V))   //p1
            {

                p1Projectiles.Add(new Projectile(p1.weapon.position, 1, p1.weapon.rotation, windowSize, bulletTexture, p1.spriteTexture, p1.weapon.movingRight));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.K) && pastKey.IsKeyUp(Keys.K))   //p2
            { 
                p2Projectiles.Add(new Projectile(p2.weapon.position, 2, p2.weapon.rotation, windowSize, bulletTexture, p2.spriteTexture, p2.weapon.movingRight));
            }

            pastKey = Keyboard.GetState();

            //delete the bullets if they hit the ground
            for (int i=0; i<p1Projectiles.Count; i++)
            {
                if (p1Projectiles[i].position.Y >= windowSize.Y - bulletTexture.Height*0.1)
                {
                    p1Projectiles.RemoveAt(i);
                }
            }
            for (int i = 0; i < p2Projectiles.Count; i++)
            {
                if (p2Projectiles[i].position.Y >= windowSize.Y - bulletTexture.Height * 0.1)
                {
                    p2Projectiles.RemoveAt(i);
                }
            }

            //update the bullets
            foreach (Projectile projectile in p1Projectiles) projectile.Update();
            foreach (Projectile projectile in p2Projectiles) projectile.Update();



            //projectile - player collision detection
            foreach (Projectile projectile in p1Projectiles)
            {
                if (projectile.boundingBox.Intersects(p2.boundingBox))
                {
                    p1Projectiles.Remove(projectile);

                    p2.Respawn();
                    p1.hasDied = false;


                    break;
                }
            }
            foreach (Projectile projectile in p2Projectiles)
            {
                if (projectile.boundingBox.Intersects(p1.boundingBox))
                {
                    p2Projectiles.Remove(projectile);


                    p1.Respawn();

                    p2.hasDied = false;

                    break;
                }
            }


            //respawn timer
            if (p2.position.Y < 0 && respawnDelay1.timerDone(gameTime))
            {
                p2.velocity.Y = 5;
                p2.hasJumped = true;
            }
            if (p1.position.Y < 0 && respawnDelay2.timerDone(gameTime))
            {
                p1.velocity.Y = 5;
                p1.hasJumped = true;
            }

            //player reaches endzone
            if (p1.position.X>= windowSize.X - p1.spriteTexture.Width)
            {
                if (p2.hasDied)
                {
                    p1.position.X = 0;
                    p2.Respawn();
                }
                else
                {
                    p1.position.X = windowSize.X - p1.spriteTexture.Width;
                }
            }
            if (p2.position.X <= 0)
            {
                if (p1.hasDied)
                {
                    p2.position.X = windowSize.X - p2.spriteTexture.Width;
                    p1.Respawn();
                }
                else
                {
                    p2.position.X = 0;
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Azure);

            // TODO: Add your drawing code here
            p1.Draw(spriteBatch);
            p2.Draw(spriteBatch);

            foreach (Projectile projectile in p1Projectiles) projectile.Draw(spriteBatch);
            foreach (Projectile projectile in p2Projectiles) projectile.Draw(spriteBatch);

            //Game Over Scenario
            if (p2.hasDied)
            { 
                spriteBatch.Begin();
                spriteBatch.Draw(arrow, new Vector2(windowSize.X - 100 - arrow.Width, 100), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, 0f);
                spriteBatch.End();
            }
            else if (p1.hasDied)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(arrow, new Vector2(100, 100), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}
