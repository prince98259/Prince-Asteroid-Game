using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astroid
{
    class Meteor:GameUti
    {
        //Intialize the Radius when meteoe class used; 
        //That why Constructer used for right ,,,,???? hahahahahahaha
        public Meteor()
        {
            Radius = 46;
        }

        public void Update(GameTime gameTime)
        {
            positin += speed;

            if (positin.X < GlobalVar.GPlayground.Left)
                positin = new Vector2(GlobalVar.GPlayground.Right, positin.Y);
            if (positin.X > GlobalVar.GPlayground.Right)
                positin = new Vector2(GlobalVar.GPlayground.Left, positin.Y);
            if (positin.Y < GlobalVar.GPlayground.Top)
                positin = new Vector2(positin.X, GlobalVar.GPlayground.Bottom);
            if (positin.Y > GlobalVar.GPlayground.Bottom)
                positin = new Vector2(positin.X, GlobalVar.GPlayground.Top);


            Rotation += 0.04f;
            if (Rotation > MathHelper.TwoPi)
                Rotation = 0;
        }
    }

}
