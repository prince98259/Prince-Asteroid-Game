using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astroid
{
    interface IGameUti
    {
        //Interface which defines the main properties of game 
        bool isDead { get; set; }
        Vector2 positin { get; set; }
        Vector2 speed { get; set; }
        float Rotation { get; set; }
        float Radius { get; set; }

       
    }
}
