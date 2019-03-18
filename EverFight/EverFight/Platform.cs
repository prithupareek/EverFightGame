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
    class Platform
    {

        Texture2D platformTexture;
        public Vector2 position;
        Vector2 dimensions;
        public BoundingBox boundingBox;

        public Platform(Vector2 pos, Vector2 dim, Texture2D texture2D)
        {
            position = pos;
            dimensions = dim;
            platformTexture = texture2D;
            boundingBox = new BoundingBox(new Vector3(position, 0), new Vector3(position.X + dimensions.X*40, position.Y + dimensions.Y*40, 0));

        }

    

        public void Draw(SpriteBatch sb)
        {
            sb.Begin();
            for (int x = 0; x<dimensions.X; x++)
            {
                for (int y=0; y<dimensions.Y; y++)
                {
                    sb.Draw(platformTexture, new Vector2(position.X + x * 40, position.Y + y * 40));
                }
            }
            sb.End();
        }

    }


}
