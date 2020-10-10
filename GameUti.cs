using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astroid
{
    //just define as abstract class  Because there is Only Mehtod declarations and Properties defination.
   abstract class GameUti:IGameUti
    {
      public  bool isDead { get; set; }
        public Vector2 positin { get; set; }
        public Vector2 speed { get; set; }
        public float Rotation { get; set; }
        public float Radius { get; set; }


        public bool collideWith(IGameUti other)
        {
            return (this.positin - other.positin).LengthSquared() < (Radius + other.Radius*Radius);
        }
    }
}
