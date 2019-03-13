using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverFight
{
    class Delay
    {
        //Properties
        TimeSpan delayRate;
        TimeSpan previousCallTime;

        //Constructor
        public Delay(float rate)
        {
            delayRate = TimeSpan.FromSeconds(rate);
            previousCallTime = TimeSpan.FromSeconds(0f);
        }

        public bool timerDone(GameTime gt)
        {

            if (gt.TotalGameTime - previousCallTime > delayRate)
            {
                previousCallTime = gt.TotalGameTime;
                return true;
            }

            return false;
        }

    }
}
