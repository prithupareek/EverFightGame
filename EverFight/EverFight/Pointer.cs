using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverFight
{
    class Pointer
    {
        public Vector2 position;
        public Texture2D pointerTexture;
        public int playerNum;
        Vector2 windowSize;
        public BoundingBox boundingBox;
        


        public Pointer(int num, Vector2 ws)
        {
            playerNum = num;
            windowSize = ws;

            if (playerNum == 1)
            {
                position = new Vector2(100, 10); //initial player position
            }
            else if (playerNum == 2)
            {
                position = new Vector2(windowSize.X - 100, 10); //initial player position
            }
        }

        //Load Content
        public void LoadContent(ContentManager cm)
        {
            //load the image for the sprite
            pointerTexture = cm.Load<Texture2D>("pointer");
        }

            public void Update() {

            KeyboardState keys = Keyboard.GetState();   // get current state of keyboard

            boundingBox = new BoundingBox(new Vector3(position, 0), new Vector3(position.X + (pointerTexture.Width), position.Y + (pointerTexture.Height), 0));

            if (playerNum == 1)
            {
                if (keys.IsKeyDown(Keys.D) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X > 0) //right
                {
                    position.X += 3.5f;
                }
                if (keys.IsKeyDown(Keys.A) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < 0) //left
                {

                    position.X -= 3.5f;
                }
                if (keys.IsKeyDown(Keys.W) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y > 0)   //up
                {
                    position.Y -= 3.5f;
                }
                if (keys.IsKeyDown(Keys.S) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y < 0)   //down
                {
                    position.Y += 3.5f;
                }
            }
            else if (playerNum == 2)
            {
                if (keys.IsKeyDown(Keys.Right) || GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.X > 0) //right
                {
                    position.X += 3.5f;
                }
                if (keys.IsKeyDown(Keys.Left) || GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.X < 0) //left
                {

                    position.X -= 3.5f;
                }
                if (keys.IsKeyDown(Keys.Up) || GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.Y > 0)   //up
                {
                    position.Y -= 3.5f;
                }
                if (keys.IsKeyDown(Keys.Down) || GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.Y < 0)   //down
                {
                    position.Y += 3.5f;
                }

            }




        }

        public void Draw(SpriteBatch sb)
        {

            sb.Begin();

            if (playerNum == 1)
            {
                sb.Draw(pointerTexture, position, null, Color.White, 0f, new Vector2(pointerTexture.Width / 2, pointerTexture.Height / 2), 1f, SpriteEffects.FlipHorizontally, 0f);

            }
            if (playerNum == 2)
            {
                sb.Draw(pointerTexture, position, null, Color.White, 0f, new Vector2(pointerTexture.Width / 2, pointerTexture.Height / 2), 1f, SpriteEffects.None, 0f);

            }
            sb.End();

        }
    }
}
