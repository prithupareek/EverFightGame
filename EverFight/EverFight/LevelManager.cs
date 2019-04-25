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
        Texture2D p1Win;
        Texture2D p2Win;


        public LevelManager(Vector2 ws)
        {
            levels = new List<Level>();
            activeLevel = 1;    //active level is set to the middle of the map
            windowSize = ws;

        }

        //create the levels, load the win screen images
        public void LoadLevel(ContentManager cm)
        {
            Texture2D platformTexture = cm.Load<Texture2D>("black");
            levels.Add(new Level(0, platformTexture, windowSize));
            levels.Add(new Level(1, platformTexture, windowSize));
            levels.Add(new Level(2, platformTexture, windowSize));

            


        }

       

        public void DrawLevel(SpriteBatch sb)
        {

            //draw win screen images if ative level is beyond created levels
            if (activeLevel > 2)
            {
                
            }
            else if (activeLevel < 0)
            {
                
            }
            else
            {
                levels[activeLevel].Draw(sb);
            }
        }
    }
}
