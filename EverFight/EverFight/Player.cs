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
        Texture2D spriteTexture;   // the image for our sprite
        Vector2 position;  // the position for our sprite
        int playerNumber; //stores if p1 or p2
        Boolean jumping;
        int jumpSpeed;
        Vector2 windowSize; 

        //Constructor
        public Player(int num, Vector2 ws) {

            playerNumber = num;
            windowSize = ws;
            jumping = false;
            jumpSpeed = 0;

            if (playerNumber == 1)
            {
                position = new Vector2((windowSize.X / 4) - 50, windowSize.Y - (windowSize.Y / 3)); //initial player position
            }
            else if (playerNumber == 2)
            {
                position = new Vector2(windowSize.X - (windowSize.X / 4), windowSize.Y - (windowSize.Y/3)); //initial player position
            }
        }

        //Load Content
        public void LoadContent(ContentManager cm)
        {
            //load the image for the sprite
            spriteTexture = cm.Load<Texture2D>("rectSprite");
        }

        //Update
        public void Update()
        { 

            KeyboardState keys = Keyboard.GetState();   // get current state of keyboard

            position.Y += jumpSpeed;

            if (playerNumber == 1)
            {
                if (keys.IsKeyDown(Keys.D)) //right
                {
                    position = position + new Vector2(1, 0);
                }
                if (keys.IsKeyDown(Keys.A)) //left
                {
                    position = position + new Vector2(-1, 0);
                }
                if (keys.IsKeyDown(Keys.B) && jumping == false) //jump
                {

                    jumpSpeed = -5;
                    jumping = true;
                }
                if (position.Y == windowSize.Y - (windowSize.Y / 3) && keys.IsKeyUp(Keys.B))
                {
                    jumping = false;
                    jumpSpeed = 0;
                }
            }
            else if (playerNumber == 2)
            {
                if (keys.IsKeyDown(Keys.Right)) //right
                {
                    position = position + new Vector2(1, 0);
                }
                if (keys.IsKeyDown(Keys.Left)) //left
                {
                    position = position + new Vector2(-1, 0);
                }
                if (keys.IsKeyDown(Keys.L) && jumping == false) //jump
                {

                    jumpSpeed = -5;
                    jumping = true;
                }
                if (position.Y == windowSize.Y - (windowSize.Y / 3) && keys.IsKeyUp(Keys.L))
                {
                    jumping = false;
                    jumpSpeed = 0;
                }
            }

            if (position.Y < windowSize.Y - (windowSize.Y / 2))
            {
                jumpSpeed = 5;
            }
            if (position.Y > windowSize.Y - (windowSize.Y / 3))
            {
                position.Y = windowSize.Y - (windowSize.Y / 3);
                jumping = false;
            }
            
        }

        //Draw
        public void Draw(SpriteBatch sb)
        {

            sb.Begin();
            sb.Draw(spriteTexture, position);
            sb.End();
        }
    }
}
