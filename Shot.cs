using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astroid
{
    class Shot : GameUti
    {
        public Shot()
        {
            Radius = 16;
        }

        public void Update(GameTime gameTime)
        {
            positin += speed;

           
        
            Rotation += 0.04f;
            if (Rotation > MathHelper.TwoPi)
                Rotation = 0;
        }
    }
}
