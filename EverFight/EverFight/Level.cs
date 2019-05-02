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

        public Level(int lev, Vector2 ws, ContentManager cm)
        {
            level = lev;
            platforms = new List<Platform>();
            windowSize = ws;

            //Load all the Images
            platform1 = cm.Load<Texture2D>("1");
            platform11 = cm.Load<Texture2D>("11");
            platform16 = cm.Load<Texture2D>("16");
            platform17 = cm.Load<Texture2D>("17");

            //parse the input tilemap file
            String input = File.ReadAllText(@"Content\level"+level+".txt");
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

            for (int y=0; y<20; y++)
            {
                for (int x=0; x<32; x++)
                {
                    if (result[y,x] == 1)
                    {
                        platforms.Add(new Platform(new Vector2(x * 40, y * 40), new Vector2(1, 1), platform1));
                    }
                    else if (result[y, x] == 11)
                    {
                        platforms.Add(new Platform(new Vector2(x * 40, y * 40), new Vector2(1, 1), platform11));
                    }
                    else if (result[y, x] == 16)
                    {
                        platforms.Add(new Platform(new Vector2(x * 40, y * 40), new Vector2(1, 1), platform16));
                    }
                    else if (result[y, x] == 17)
                    {
                        platforms.Add(new Platform(new Vector2(x * 40, y * 40), new Vector2(1, 1), platform17));
                    }
                    else
                    {

                    }

                }
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
