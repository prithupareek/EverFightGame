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
        Texture2D playerSpriteTexture; //used to get the dimensions of texture
        Vector2 launchPos;
        public BoundingBox boundingBox;

        //Constructor
        public Projectile(Vector2 pos, int player, float rot, Vector2 ws, Texture2D bt, Texture2D pt)
        {
            playerNum = player;
            position = pos;
            rotation = rot;
            windowSize = ws;
            spriteTexture = bt;
            playerSpriteTexture = pt;
            launchPos = pos;


            if (playerNum == 1)
            {
                velocity = new Vector2(20*(float)Math.Cos(rotation), 20*(float)Math.Sin(rotation));
            }
            if (playerNum == 2)
            {
                velocity = new Vector2(-20 * (float)Math.Cos(rotation), -20 * (float)Math.Sin(rotation));
            }
            
        }

        //Content is loaded in Game1 Class

        //Update
        public void Update()
        {
            position += velocity;

            boundingBox = new BoundingBox(new Vector3(position, 0), new Vector3(position.X + (spriteTexture.Width), position.Y + (spriteTexture.Height), 0));

            //gravity for y direction
            if (position.Y < windowSize.Y - (windowSize.Y /3) + playerSpriteTexture.Height)
            {
                float i = 1;
                velocity.Y += 0.3f * i;
            }
            //have the bullet rotate as a projectile
            rotation = (float)Math.Atan2(velocity.Y, velocity.X); 

        }

        //Draw
        public void Draw(SpriteBatch sb)
        {
            sb.Begin();
            sb.Draw(spriteTexture, position, null, Color.Black, rotation, new Vector2(spriteTexture.Width / 2, spriteTexture.Height / 2), 0.1f, SpriteEffects.None, 0f);
            sb.End();
        }
    }
}
