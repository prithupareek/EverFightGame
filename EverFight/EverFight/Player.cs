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
    class Player
    {

        // Properties
        public Texture2D spriteTexture, jumpTexture, walkTexture1, walkTexture2, temp;   // the image for our sprite
        public Vector2 position;  // the position for our sprite
        int playerNumber; //stores if p1 or p2
        Vector2 windowSize;
        public Vector2 velocity;
        public Boolean hasJumped;
        public BoundingBox boundingBox;
        public Boolean hasDied;
        public Weapon weapon;
        public GamePadState pastButton;
        public Pointer pointer;
        Boolean walking;
        public string playerColor;

        //Delay timer for walking animation
        int walkingAnimationDelay;



        //Constructor
        public Player(int num, Vector2 ws) {

            playerNumber = num;
            windowSize = ws;
            hasJumped = true;
            hasDied = false;
            walking = false;
            walkingAnimationDelay = 0;

            //used for walking animation
            temp = walkTexture1;



            if (playerNumber == 1)
            {
                position = new Vector2(125, 10); //initial player position
                playerColor = "Biege";
            }
            else if (playerNumber == 2)
            {
                position = new Vector2(windowSize.X - 125 - 72, 10); //initial player position
                playerColor = "Blue";
            }

            weapon = new Weapon(position, playerNumber);
            pointer = new Pointer(playerNumber, windowSize);
        }

        //Load Content
        public void LoadContent(ContentManager cm)
        {
            //load the image for the sprite
            spriteTexture = cm.Load<Texture2D>("alien"+playerColor+"_stand");
            jumpTexture = cm.Load<Texture2D>("alien" + playerColor + "_jump");
            walkTexture1 = cm.Load<Texture2D>("alien" + playerColor + "_walk1");
            walkTexture2 = cm.Load<Texture2D>("alien" + playerColor + "_walk2");

            weapon.LoadContent(cm);
        }

        //Update
        public void Update(List<Platform> platforms)
        {
            weapon.Update();
            
            KeyboardState keys = Keyboard.GetState();   // get current state of keyboard

            position += velocity;   //used mostly for gravity

            boundingBox = new BoundingBox(new Vector3(position, 0), new Vector3(position.X + (spriteTexture.Width), position.Y + (spriteTexture.Height), 0));

            if (playerNumber == 1)
            {
                if (keys.IsKeyDown(Keys.D) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X>0) //right
                {
                    weapon.movingRight = true;
                    weapon.position.X = position.X + 95;
                    position.X+= 3f;
                    walking = true;
                }
                if (keys.IsKeyDown(Keys.A) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < 0) //left
                {
                    weapon.movingRight = false;
                    weapon.position.X = position.X - 25;
                    position.X-= 3f;
                    walking = true;
                }
                if ((keys.IsKeyDown(Keys.B) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A)) && hasJumped == false)   //jump
                {
                    position.Y -= 10f;
                    velocity.Y = -10f;
                    hasJumped = true;
                    //touchingPlatTop = false;
                }

                if (keys.IsKeyUp(Keys.A) && keys.IsKeyUp(Keys.D))
                {
                    walking = false;
                    walkingAnimationDelay = 0;
                }


            }
            else if (playerNumber == 2)
            {
                if (keys.IsKeyDown(Keys.Right) || GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.X > 0) //right
                {
                    weapon.movingRight = true;
                    weapon.position.X = position.X + 95;
                    position.X+= 3f;
                    walking = true;
                }
                if (keys.IsKeyDown(Keys.Left) || GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.X < 0) //left
                {
                    weapon.movingRight = false;
                    weapon.position.X = position.X - 25;
                    position.X-= 3f;
                    walking = true;
                }
                if ((keys.IsKeyDown(Keys.L) || GamePad.GetState(PlayerIndex.Two).IsButtonDown(Buttons.A)) && hasJumped == false)   //jump
                {
                    position.Y -= 10f;
                    velocity.Y = -10f;
                    hasJumped = true;
                    //touchingPlatTop = false;
                }

                if (keys.IsKeyUp(Keys.Left) && keys.IsKeyUp(Keys.Right))
                {
                    walking = false;
                    walkingAnimationDelay = 0;
                }
            }


            weapon.position.Y = position.Y + 50;

            if (position.Y > 0)
            {
                hasJumped = true;
            }

            //stop jumping if intersecting platform
            foreach (Platform platform in platforms)
            {       

                if (boundingBox.Intersects(platform.boundingBox))
                {
                    //if on top of platform -- Works
                    if (position.Y + spriteTexture.Height < platform.position.Y + 20)
                    {
                        hasJumped = false;
                    }

                    //if on the bottom of the platform -- Works
                    else if (position.Y > platform.position.Y + platform.pixDimensions.Y - 20)
                    {
                        velocity.Y = -velocity.Y;
                    }

                    //if hitting the right side of the platform 
                    else if (position.X <= platform.position.X + platform.pixDimensions.X && position.X > platform.position.X + platform.pixDimensions.X / 2 && !(position.Y + spriteTexture.Height < platform.position.Y + 40) && !(position.Y > platform.position.Y + platform.pixDimensions.Y - 40))
                    {
                        position.X = platform.position.X + platform.pixDimensions.X;
                    }

                    //if hitting the left side of the platform
                    else if (position.X + spriteTexture.Width >= platform.position.X && position.X < platform.position.X + platform.pixDimensions.X / 2 && !(position.Y + spriteTexture.Height < platform.position.Y + 20) && !(position.Y > platform.position.Y + platform.pixDimensions.Y - 20))
                    {
                        position.X = platform.position.X - spriteTexture.Width;
                    }

                }

            }


            //jumping stuff
            if (hasJumped == true)
            {
                float i = 1;
                velocity.Y += 0.3f * i;
            }
            if (position.Y > windowSize.Y - spriteTexture.Height)
            {
                position.Y = windowSize.Y - spriteTexture.Height;
                hasJumped = false;

                //die if hit the ground
                Respawn();
            }

            if (hasJumped == false)
            {
                velocity.Y = 0f;
            }



        }

        public void Respawn()
        {
            //TODO: Change for p1 vs p2

            if (playerNumber == 1)
            {
                position = new Vector2(125, -200);
                weapon.position = position + new Vector2(95, 50);
                weapon.movingRight = true;
                hasDied = true;
                hasJumped = false;

            }
            else if (playerNumber == 2)
            {
                position = new Vector2(windowSize.X - 125 - spriteTexture.Width, -200);
                weapon.position = position + new Vector2(-25, 50);
                weapon.movingRight = false;
                hasDied = true;
                hasJumped = false;
            }
            

        }

        //Draw
        public void Draw(SpriteBatch sb)
        {

            //used for flipping the image based on the direction of motion
            SpriteEffects flip = SpriteEffects.None;
            if (weapon.movingRight == false)
            {
                flip = SpriteEffects.FlipHorizontally;
            }
            else
            {
                flip = SpriteEffects.None;
            }

            if (hasJumped == true)
            {
                sb.Begin();
                sb.Draw(jumpTexture, position, null, Color.White, 0f, Vector2.Zero, 1f, flip, 0f);
                sb.End();
            }
            else if (walking && !hasJumped)
            {

                if (walkingAnimationDelay % 5 == 0)
                {
                    temp = walkTexture1;
                }
                if (walkingAnimationDelay % 10 == 0)
                {
                    temp = walkTexture2;
                }

                walkingAnimationDelay++;

                sb.Begin();
                sb.Draw(temp, position, null, Color.White, 0f, Vector2.Zero, 1f, flip, 0f);
                sb.End();
            }
            else
            {
                sb.Begin();
                sb.Draw(spriteTexture, position, null, Color.White, 0f, Vector2.Zero, 1f, flip, 0f);
                sb.End();
            }
            

            weapon.Draw(sb);

        }

    }
}
