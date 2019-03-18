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


        public LevelManager()
        {
            levels = new List<Level>();
            activeLevel = 1;

        }

        public void LoadLevel(ContentManager cm)
        {
            Texture2D platformTexture = cm.Load<Texture2D>("black");
            levels.Add(new Level(0, platformTexture));
            levels.Add(new Level(1, platformTexture));
            levels.Add(new Level(2, platformTexture));
        }

       

        public void DrawLevel(SpriteBatch sb)
        {
            levels[activeLevel].Draw(sb);
        }
    }
}
