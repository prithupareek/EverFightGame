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
    class Projectile
    {
        //Properties
        Texture2D spriteTexture;   // the image for our sprite
        public Vector2 position;  // the position for our sprite
        int playerNum; //stores if p1 or p2
        float rotation;
        Vector2 velocity;
        Vector2 windowSize;

        //Constructor
        public Projectile(Vector2 pos, int player, float rot, Vector2 ws)
        {
            playerNum = player;
            position = pos;
            rotation = rot;
            windowSize = ws;

            if (playerNum == 1)
            {
                velocity = new Vector2(5f, 2f);
            }
            if (playerNum == 2)
            {
                velocity = new Vector2(-5f, 2f);
            }
            
        }

        //LoadContent
        public void LoadContent(ContentManager cm)
        {
            //load the image for the sprite
            spriteTexture = cm.Load<Texture2D>("projectile");
        }

        //Update
        public void Update()
        {
            position += velocity;

            //gravity for y direction
            //if the bullet hits the ground
            if (position.Y >= windowSize.Y - (windowSize.Y / 3))
            {
                position.Y = windowSize.Y - (windowSize.Y / 3);
                velocity.X = 0f;
                velocity.Y = 0f;
            }
            //if the bullet is in the air
            if (position.Y < windowSize.Y - (windowSize.Y /3))
            {
                //float i = 1;
                //velocity.Y += 0.3f * i;
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
