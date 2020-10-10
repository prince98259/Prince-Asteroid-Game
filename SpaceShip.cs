using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Astroid
{
    class SpaceShip :DrawableGameComponent, IGameUti
    {
        //Inhites the proprties from Interfaces
        public  bool isDead { get; set; }
        public Vector2 positin { get; set; }
        public Vector2 speed { get; set; }
        public float Rotation { get; set; }
        private int relodTime = 0;
        private Random random = new Random();
        public bool shooted { get { return relodTime == 0; } }
        public float Radius { get; set; }
        private Texture2D spaceshipTexture;
      

        //A constructer to define the starting position of spaceship
        public SpaceShip(Game game):base(game)
        {
            positin = new Vector2(GlobalVar.wiwth / 4, GlobalVar.height / 4);

        }

        protected override void LoadContent()
        {
            spaceshipTexture = Game.Content.Load<Texture2D>("spaceship");
            base.LoadContent();
        }

        //Update Mehtod Which Override the base Meyhod In game Class 
        public override void Update(GameTime gameTime)
        {
            positin += speed;
            if (relodTime > 0)
                relodTime--;
            //Uses the Property defined in the GlobalVar and make a Bountry for Spaceship.
            if (positin.X < GlobalVar.GPlayground.Left)
                positin = new Vector2(GlobalVar.GPlayground.Right, positin.Y);
            if (positin.X > GlobalVar.GPlayground.Right)
                positin = new Vector2(GlobalVar.GPlayground.Left, positin.Y);
            if (positin.Y < GlobalVar.GPlayground.Top)
                positin = new Vector2( positin.X , GlobalVar.GPlayground.Bottom);
            if (positin.Y > GlobalVar.GPlayground.Bottom)
                positin = new Vector2(positin.X, GlobalVar.GPlayground.Top);

            base.Update(gameTime);
        }

        /// <summary>
        /// Mehtod to draw the SpaceShip 
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            //I took algorithm to move My spaceship angle
            spriteBatch.Draw(spaceshipTexture, positin,null, Color.White,Rotation + MathHelper.PiOver2,new Vector2(spaceshipTexture.Width/2,spaceshipTexture.Height/2), 1.0f , SpriteEffects.None, 0f);
        }

        //I used the sinex and coex Mehtods to Move the Angle Of my spaceship (***As you Shown In Lecture****)
        public void Accelerate()
        {
            speed += new Vector2((float)Math.Cos(Rotation),(float)Math.Sin(Rotation)) * 0.08f;

            //O used the Accelerate Mehtod so the function for handle the speed
            if (speed.LengthSquared() > 60)
            {
                speed = Vector2.Negate(speed) * 5;
            }
        }

        //Shoot Mthod to add new Laser Image Object
        public Shot Shoot()
        {
            if (!shooted)
                return null;
            relodTime = 20;


            //Returns a New Shot With Prpperties Like Rotaiona and speed
            return new Shot()
            {
                positin = positin,
                speed = speed + 10f * new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation)),
                Rotation = random.Next()*MathHelper.TwoPi
           
            };
        }
    }
}
