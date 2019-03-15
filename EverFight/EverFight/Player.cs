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
        public Texture2D spriteTexture;   // the image for our sprite
        public Vector2 position;  // the position for our sprite
        int playerNumber; //stores if p1 or p2
        Vector2 windowSize;
        public Vector2 velocity;
        public Boolean hasJumped;
        public BoundingBox boundingBox;
        public Boolean hasDied;
        public Boolean hasWon;
        public Weapon weapon;
        //public BoundingBox playerWeaponBox;

        //Constructor
        public Player(int num, Vector2 ws) {

            playerNumber = num;
            windowSize = ws;
            hasJumped = true;
            hasDied = false;
            

            if (playerNumber == 1)
            {
                position = new Vector2((windowSize.X / 4) - 50, windowSize.Y - (windowSize.Y / 3)); //initial player position
            }
            else if (playerNumber == 2)
            {
                position = new Vector2(windowSize.X - (windowSize.X / 4), windowSize.Y - (windowSize.Y/3)); //initial player position
            }

            weapon = new Weapon(position, playerNumber);
        }

        //Load Content
        public void LoadContent(ContentManager cm)
        {
            //load the image for the sprite
            spriteTexture = cm.Load<Texture2D>("rectSprite");

            weapon.LoadContent(cm);
        }

        //Update
        public void Update()
        {
            weapon.Update();
            
            KeyboardState keys = Keyboard.GetState();   // get current state of keyboard

            position += velocity;   //used mostly for gravity

            boundingBox = new BoundingBox(new Vector3(position, 0), new Vector3(position.X + (spriteTexture.Width), position.Y + (spriteTexture.Height), 0));

            if (playerNumber == 1)
            {
                if (keys.IsKeyDown(Keys.D)) //right
                {
                    weapon.movingRight = true;
                    weapon.position.X = position.X + 50;
                    position.X+= 3f;
                }
                if (keys.IsKeyDown(Keys.A)) //left
                {
                    weapon.movingRight = false;
                    weapon.position.X = position.X - 25;
                    position.X-= 3f;
                }
                if (keys.IsKeyDown(Keys.B) && hasJumped == false)   //jump
                {
                    position.Y -= 10f;
                    velocity.Y = -10f;
                    hasJumped = true;
                }


            }
            else if (playerNumber == 2)
            {
                if (keys.IsKeyDown(Keys.Right)) //right
                {
                    weapon.movingRight = true;
                    weapon.position.X = position.X + 50;
                    position.X+= 3f;
                }
                if (keys.IsKeyDown(Keys.Left)) //left
                {
                    weapon.movingRight = false;
                    weapon.position.X = position.X - 25;
                    position.X-= 3f;
                }
                if (keys.IsKeyDown(Keys.L) && hasJumped == false)   //jump
                {
                    position.Y -= 10f;
                    velocity.Y = -10f;
                    hasJumped = true;
                }
            }

            //TODO: Prevent players from crossing each other
            //player weapon bounding box stuff
            //if (weapon.movingRight)
            //{
            //    playerWeaponBox = new BoundingBox(new Vector3(weapon.position.X + weapon.spriteTexture.Width * 0.15f, position.Y, 0), new Vector3(position.X + (spriteTexture.Width), position.Y + (spriteTexture.Height), 0));
            //}
            //if (!weapon.movingRight)
            //{
            //    playerWeaponBox = new BoundingBox(new Vector3(weapon.position.X + weapon.spriteTexture.Width * 0.15f, position.Y, 0), new Vector3(position.X + (spriteTexture.Width), position.Y + (spriteTexture.Height), 0));
            //}

            weapon.position.Y = position.Y + 50;

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
            }
            if (hasJumped == false)
            {
                velocity.Y = 0f;
            }
            
        }

        public void Respawn()
        {
            position = new Vector2(windowSize.X - (windowSize.X / 4), -200);
            weapon.position = position + new Vector2(-25, 50);
            weapon.movingRight = false;
            hasDied = true;

        }

        //Draw
        public void Draw(SpriteBatch sb)
        {


            sb.Begin();
            sb.Draw(spriteTexture, position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f); //used for smaller machine scale
            sb.End();

            weapon.Draw(sb);

        }
    }
}
