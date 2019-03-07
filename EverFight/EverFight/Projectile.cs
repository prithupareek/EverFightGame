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

        //Constructor
        public Projectile(Vector2 pos, int player, float rot)
        {
            playerNum = player;
            position = pos;
            rotation = rot;
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
           

        }

        //Draw
        public void Draw(SpriteBatch sb)
        {
            sb.Begin();

            //TODO: Ask Darby how to vary scale with screen size



            sb.End();
        }
    }
}
