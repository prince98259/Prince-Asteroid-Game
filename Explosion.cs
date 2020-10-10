﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astroid
{
    class Explosion:GameUti
    {
        public float Scale { get; set; }

        private int timer = 30;

        public Color Color
        {
            get { return new Color(timer * 8, timer * 8, timer * 8, timer * 8); }
        }

        public void Update(GameTime gameTime)
        {
            if (timer > 0)
                timer--;
            else isDead = true;

            Rotation += 0.02f;

            if (Rotation > MathHelper.TwoPi)
                Rotation = 0;
        }
    }
}
