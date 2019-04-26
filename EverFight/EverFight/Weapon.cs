using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

//for debugging and printing to the console
using System.Diagnostics;


namespace EverFight
{
    class Weapon
    {

        //Properties
        public Texture2D spriteTexture; //the image for our sprite
        public Vector2 position; //the position for our sprite
        int playerNum; //stores if p1 or p2
        Vector2 windowSize; //not sure if needed
        public float rotation = 0f;
        float rotationSpeed = 0f;
        public Boolean movingRight;
        public List<Projectile> projectiles;

        //Constructor
        public Weapon(Vector2 pos, int player) {

            playerNum = player;

            if (playerNum == 1)
            {
                position = pos + new Vector2(50, 50);
                movingRight = true;
            }
            else if (playerNum == 2)
            {
                position = pos + new Vector2(-25, 50);
                movingRight = false;
            }

            projectiles = new List<Projectile>();
        }

        //Load Content
        public void LoadContent(ContentManager cm)
        {
            //load the image for the sprite
            spriteTexture = cm.Load<Texture2D>("watergun");
        }

        //Update
        public void Update()
        {

            KeyboardState keys = Keyboard.GetState();   // get current state of keyboard

            //aim weapon
            if (playerNum == 1)
            {
                if (keys.IsKeyDown(Keys.W) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y > 0)
                {
                    if (movingRight)
                    {
                        rotationSpeed = MathHelper.ToRadians(-1f);
                    }
                    else
                    {
                        rotationSpeed = MathHelper.ToRadians(1f);

                    }
                }
                if (keys.IsKeyDown(Keys.S) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y < 0)
                {
                    if (movingRight)
                    {
                        rotationSpeed = MathHelper.ToRadians(1f);
                    }
                    else
                    {
                        rotationSpeed = MathHelper.ToRadians(-1f);

                    }
                }
                if (rotation <= MathHelper.ToRadians(-25))
                {
                    rotationSpeed = 0;
                }
                if (rotation >= MathHelper.ToRadians(25))
                {
                    rotationSpeed = 0;
                }
                if (keys.IsKeyUp(Keys.W) && keys.IsKeyUp(Keys.S) && Math.Abs(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y) < 0.1)
                {
                    if (rotation > 0)
                    {
                        rotationSpeed = MathHelper.ToRadians(-1f);
                    }
                    if (rotation < 0)
                    {
                        rotationSpeed = MathHelper.ToRadians(1f);
                    }
                    if (rotation == 0)
                    {
                        rotationSpeed = 0;
                    }
                }
                

            }
            else if (playerNum == 2)
            {
                if (keys.IsKeyDown(Keys.Up) || GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.Y > 0)
                {
                    if (movingRight)
                    {
                        rotationSpeed = MathHelper.ToRadians(-1f);
                    }
                    else
                    {
                        rotationSpeed = MathHelper.ToRadians(1f);

                    }
                }
                if (keys.IsKeyDown(Keys.Down) || GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.Y < 0)
                {
                    if (movingRight)
                    {
                        rotationSpeed = MathHelper.ToRadians(1f);
                    }
                    else
                    {
                        rotationSpeed = MathHelper.ToRadians(-1f);

                    }
                }
                if (rotation <= MathHelper.ToRadians(-25))
                {
                    rotationSpeed = 0;
                }
                if (rotation >= MathHelper.ToRadians(25))
                {
                    rotationSpeed = 0;
                }
                if (keys.IsKeyUp(Keys.Up) && keys.IsKeyUp(Keys.Down) && Math.Abs(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y) < 0.1)
                {
                    if (rotation > 0)
                    {
                        rotationSpeed = MathHelper.ToRadians(-1f);
                    }
                    if (rotation < 0)
                    {
                        rotationSpeed = MathHelper.ToRadians(1f);
                    }
                    if (rotation == 0)
                    {
                        rotationSpeed = 0;
                    }
                }

            }

            rotation += rotationSpeed;
        }

        //Draw
        public void Draw(SpriteBatch sb)
        {

            sb.Begin();            

            //used to draw the sprite flipped for players
            if (movingRight)
            {
                sb.Draw(spriteTexture, position, null, Color.White, rotation, new Vector2(spriteTexture.Width / 2, spriteTexture.Height / 2), 0.15f, SpriteEffects.None, 0f);
            }
            else if (!movingRight)
            {
                sb.Draw(spriteTexture, position, null, Color.White, rotation, new Vector2(spriteTexture.Width/2, spriteTexture.Height/2) , 0.15f, SpriteEffects.FlipHorizontally, 0f);
            }


            sb.End();
        }

    }
}
