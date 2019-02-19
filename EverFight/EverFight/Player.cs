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
    class Player : Game1
    {

        // Properties
        Texture2D spriteTexture;   // the image for our sprite
        Vector2 position;  // the position for our sprite
        int playerNumber; //stores if p1 or p2
        
        //Constructor
        public Player(int num) {

            playerNumber = num;

            if (playerNumber == 1)
            {
                position = new Vector2(200, 1200); //initial player position
            }
            else if (playerNumber == 2)
            {
                position = new Vector2(200, 1200); //initial player position
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
