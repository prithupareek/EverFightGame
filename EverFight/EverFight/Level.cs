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
    class Level
    {
        int level;
        public List<Platform> platforms;
        Texture2D platformTexture;

        public Level(int lev, Texture2D pTex)
        {
            level = lev;
            platforms = new List<Platform>();
            platformTexture = pTex;

            if (level == 0)
            {
                platforms.Add(new Platform(new Vector2(100, 400), new Vector2(19, 1), platformTexture));
               
            }
            if (level == 1)
            {
                platforms.Add(new Platform(new Vector2(0, 600), new Vector2(10,2), platformTexture));
            }
            if (level == 2)
            {
                platforms.Add(new Platform(new Vector2(200, 200), new Vector2(3, 3), platformTexture));
            }


           
        }

        public void Draw(SpriteBatch sb)
        {
           
            for (int i = 0; i<platforms.Count; i++)
            {
                platforms[i].Draw(sb);
            }
        }
    }
}
