using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Diagnostics;

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

            // TODO: use this.Content to load your game content here
            p1.LoadContent(Content);
            p2.LoadContent(Content);

            p1Weapon.LoadContent(Content);
            p2Weapon.LoadContent(Content);
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
            p1Weapon.position = p1.position + new Vector2(75, 150);
            p2Weapon.position = p2.position + new Vector2(-25, 150);

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

            base.Draw(gameTime);
        }
    }
}
