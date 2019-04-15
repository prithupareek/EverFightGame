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
        Vector2 position;
        Game1.ButtonType buttonType;
        Texture2D buttonTexture;
        Texture2D activeButtonTexture;

        public Button(Vector2 pos, Game1.ButtonType type)
        {
            position = pos;
            buttonType = type;

        }

        public void LoadContent(ContentManager cm)
        {
            buttonTexture = cm.Load<Texture2D>('');
            activeButtonTexture = cm.Load<Texture2D>('');
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch sb)
        {

        }
    }
}
