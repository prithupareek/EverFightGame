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
    class LevelManager
    {

        public List<Level> levels;
        public int activeLevel;
        Vector2 windowSize;
        Texture2D levelBackground;


        public LevelManager(Vector2 ws, Texture2D background)
        {
            levels = new List<Level>();
            activeLevel = 2;    //active level is set to the middle of the map
            windowSize = ws;
            levelBackground = background;

        }

        //create the levels, load the win screen images
        public void LoadLevel(ContentManager cm)
        {

                levels.Add(new Level(0, windowSize, cm));
                levels.Add(new Level(1, windowSize, cm));
                levels.Add(new Level(2, windowSize, cm));
                levels.Add(new Level(3, windowSize, cm));
                levels.Add(new Level(4, windowSize, cm)); 



        }

       

        public void DrawLevel(SpriteBatch sb)
        {

            //draw win screen images if ative level is beyond created levels
            if (activeLevel > 4)
            {
                
            }
            else if (activeLevel < 0)
            {
                
            }
            else
            {
                sb.Begin();
                sb.Draw(levelBackground, new Vector2(0, 0));
                sb.End();
                levels[activeLevel].Draw(sb);
            }
        }
    }
}
