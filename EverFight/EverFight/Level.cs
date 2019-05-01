using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace EverFight
{
    class Level
    {
        int level;
        public List<Platform> platforms;
        Texture2D platform1, platform11, platform16, platform17, platformTexture;
        Vector2 windowSize;

        public Level(int lev, Texture2D pTex, Vector2 ws, ContentManager cm)
        {
            level = lev;
            platforms = new List<Platform>();
            platformTexture = pTex;
            windowSize = ws;

            //Load all the Images
            //platform1 = cm.Load<Texture2D>()

            //parse the input tilemap file
            String input = File.ReadAllText(@"Content\level1.txt");
            int i = 0, j = 0;

            int[,] result = new int[20, 32];
            foreach (var row in input.Split('\n'))
            {
                j = 0;
                foreach (var col in row.Trim().Split(' '))
                {
                    result[i, j] = int.Parse(col.Trim());
                    j++;
                }
                i++;
            }

            for (int y=0; i<20; i++)
            {
                for (int x=0; j<32; j++)
                {
                    if (result[y,x] == 1)
                    {

                    }
                    else if (result[y, x] == 11)
                    {

                    }
                    else if (result[y, x] == 16)
                    {

                    }
                    else if (result[y, x] == 17)
                    {

                    }
                    else
                    {

                    }

                }
            }

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
