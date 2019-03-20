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
        Vector2 windowSize;

        public Level(int lev, Texture2D pTex, Vector2 ws)
        {
            level = lev;
            platforms = new List<Platform>();
            platformTexture = pTex;
            windowSize = ws;

            //add the platforms to the level based on level
            if (level == 0)
            {
                platforms.Add(new Platform(new Vector2(windowSize.X - 640, windowSize.Y - 150), new Vector2(16, 1), platformTexture));
               
            }
            if (level == 1)
            {
                platforms.Add(new Platform(new Vector2(0, windowSize.Y - 150), new Vector2(5,1), platformTexture));
                platforms.Add(new Platform(new Vector2(320, windowSize.Y - 300), new Vector2(7, 1), platformTexture));
                platforms.Add(new Platform(new Vector2(680, windowSize.Y - 300), new Vector2(7, 1), platformTexture));
                platforms.Add(new Platform(new Vector2(windowSize.X - 200, windowSize.Y - 150), new Vector2(5, 1), platformTexture));

            }
            if (level == 2)
            {
                platforms.Add(new Platform(new Vector2(0, windowSize.Y - 150), new Vector2(16, 1), platformTexture));
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
