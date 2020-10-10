using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astroid
{   
    //Just defined a Class as Abstract.Beacuse There is no methods to Use Just Properties.
   abstract  class GlobalVar 
    {
        //it defines the size of screen to play
        public static int wiwth = 1800;
        public static int height =1000;

        //Define the PlayGround Area So space can be replaced in rectange area
        public static Rectangle GPlayground
        {
            get
            {
                return new Rectangle(-80, -80, wiwth + 160, height + 160);
            }
        }

        //Define new static Ractangle which re spawn the ractangle.
        public static Rectangle RSpawnArea
        {
            get
            {
                return new Rectangle((int)centreScreen.X - 200, (int)centreScreen.Y - 200,400,400);
            }
        }

        public static Vector2 centreScreen
        {
            get { return new Vector2(height / 2, wiwth / 2); }
        } 

    }
}
