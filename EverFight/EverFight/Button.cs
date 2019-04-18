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
        Game1.ButtonType buttonType;
        public Texture2D buttonTexture;
        Texture2D activeButtonTexture;

        public Button(Vector2 pos, Game1.ButtonType type)
        {
            position = pos;
            buttonType = type;

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


            buttonTexture = cm.Load<Texture2D>(buttonName);
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch sb)
        {
            sb.Begin();
            sb.Draw(buttonTexture, position);
            sb.End();
        }
    }
}
