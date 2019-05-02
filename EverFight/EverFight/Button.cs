using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverFight
{
    class Button
    {

        //Properties
        public Vector2 position;
        public Game1.ButtonType buttonType;
        public Texture2D buttonTexture;
        public BoundingBox boundingBox;
        public int playerNumber;

        public Button(Vector2 pos, Game1.ButtonType type)
        {
            position = pos;
            buttonType = type;

        }

        public Button(Vector2 pos, Game1.ButtonType type, int playerNum)
        {
            position = pos;
            buttonType = type;
            playerNumber = playerNum;
        }

        public void LoadContent(ContentManager cm)
        {
            string buttonName = null;

            if (buttonType == Game1.ButtonType.START)
            {
                buttonName = "start-button";
            }
            else if (buttonType == Game1.ButtonType.UP_ARROW)
            {
                buttonName = "up-arrow-button";
            }
            else if (buttonType == Game1.ButtonType.LEFT_ARROW)
            {
                buttonName = "left-arrow-button";
            }
            else if (buttonType == Game1.ButtonType.RIGHT_ARROW)
            {
                buttonName = "right-arrow-button";
            }
            else if (buttonType == Game1.ButtonType.DOWN_ARROW)
            {
                buttonName = "down-arrow-button";
            }
            else if (buttonType == Game1.ButtonType.MENU)
            {
                buttonName = "menu-button";
            }
            else if (buttonType == Game1.ButtonType.PLAY)
            {
                buttonName = "play-button";
            }



            buttonTexture = cm.Load<Texture2D>(buttonName);

            position.X -= buttonTexture.Width/2;

            boundingBox = new BoundingBox(new Vector3(position, 0), new Vector3(position.X + (buttonTexture.Width), position.Y + (buttonTexture.Height), 0));

        }

        public void Draw(SpriteBatch sb)
        {
            sb.Begin();
            sb.Draw(buttonTexture, position);
            sb.End();
        }
    }
}
