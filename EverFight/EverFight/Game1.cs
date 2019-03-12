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

        //initialize weapon
        Weapon p1Weapon;
        Weapon p2Weapon;

        Vector2 windowSize;

        //arraylist to store bullets
        List<Projectile> p1Projectiles;
        List<Projectile> p2Projectiles;

        //bullets texture.. don't know if there is a better way to do this
        Texture2D bulletTexture;

        //pastkey for bullets firing
        KeyboardState pastKey;


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

            // Constructors for the Weapon class
            p1Weapon = new Weapon(p1.position, 1);
            p2Weapon = new Weapon(p2.position, 2);

            // Constructors for the projectile arraylists
            p1Projectiles = new List<Projectile>();
            p2Projectiles = new List<Projectile>();

            // TODO: use this.Content to load your game content here
            p1.LoadContent(Content);
            p2.LoadContent(Content);

            p1Weapon.LoadContent(Content);
            p2Weapon.LoadContent(Content);

            //load content for the projectiles
            bulletTexture = Content.Load<Texture2D>("projectile");

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

            p1Weapon.Update();
            p2Weapon.Update();

            //keep the positions in sync between the weapon and player
            p1Weapon.position = p1.position + new Vector2(50, 50);
            p2Weapon.position = p2.position + new Vector2(-25, 50);

            //on keypress for bullets
            if (Keyboard.GetState().IsKeyDown(Keys.V) && pastKey.IsKeyUp(Keys.V))
            {

                p1Projectiles.Add(new Projectile(p1Weapon.position, 1, p1Weapon.rotation, windowSize, bulletTexture, p1.spriteTexture));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.K) && pastKey.IsKeyUp(Keys.K))
            { 
                p2Projectiles.Add(new Projectile(p2Weapon.position, 2, p2Weapon.rotation, windowSize, bulletTexture, p2.spriteTexture));
            }

            pastKey = Keyboard.GetState();

            //delete the bullets if they hit the ground
            for (int i=0; i<p1Projectiles.Count; i++)
            {
                if (p1Projectiles[i].position.Y >= windowSize.Y - (windowSize.Y / 3) + p1.spriteTexture.Height*0.5 - bulletTexture.Height*0.1)
                {
                    p1Projectiles.RemoveAt(i);
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
                    p2.Respawn(gameTime);
                }
            }
            foreach (Projectile projectile in p2Projectiles)
            {
                if (projectile.boundingBox.Intersects(p1.boundingBox))
                {
                    Debug.WriteLine("Test");
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

            p1Weapon.Draw(spriteBatch);
            p2Weapon.Draw(spriteBatch);

            foreach (Projectile projectile in p1Projectiles) projectile.Draw(spriteBatch);
            foreach (Projectile projectile in p2Projectiles) projectile.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
