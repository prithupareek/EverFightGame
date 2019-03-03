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
        Texture2D spriteTexture; //the image for our sprite
        public Vector2 position; //the position for our sprite
        int playerNum; //stores if p1 or p2
        Vector2 windowSize; //not sure if needed
        float rotation;

        //Constructor
        public Weapon(Vector2 pos, int player) {

            playerNum = player;
            position = pos;

            if (playerNum == 1)
            {
                rotation = 0f;
            }
            else
            {
                rotation = 90f; //TODO: Figure out conversion to degrees
            }
        }

        //Load Content
        public void LoadContent(ContentManager cm)
        {
            //load the image for the sprite
            spriteTexture = cm.Load<Texture2D>("sword");
        }

        //Update
        public void Update()
        {


        }

        //Draw
        public void Draw(SpriteBatch sb)
        {

            sb.Begin();

            //TODO: Ask Darby how to vary scale with screen size
            sb.Draw(spriteTexture, position, null, Color.White, rotation, Vector2.Zero, 0.1f, SpriteEffects.None, 0f);
            sb.End();
        }

    }
}
