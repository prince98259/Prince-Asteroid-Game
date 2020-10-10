using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Astroid
{
 
    public class Astoroid : Game
    {
        public int numberofShots = 1;

        public float speedint = 1.0f;
        public SpriteBatch spriteBatch;

        //declare all scenes here
       
       
        //creditscene
        GraphicsDeviceManager graphics;
       
        Texture2D backgroundTexture;
        SpaceShip Spaceship;
        Texture2D laserTexture;
        KeyboardState KeyboardState;
        SoundEffect laserS;
        SoundEffect meteorS;
        Texture2D explosionTxt;
        SpriteFont Scorefont;
        List<Explosion> Lstexplosions = new List<Explosion>();
        //A lsit of Shots Class
        List<Shot> shots = new List<Shot>();
        Random random = new Random();
        List<Meteor> meteors = new List<Meteor>();
        Vector2 position;
        Texture2D meteorTxt;

        public int NumberOfMeteorKilled = 0;

        public Astoroid()
        {
           
            graphics = new GraphicsDeviceManager(this);
            //set the screen size when game initiliazed.
            graphics.PreferredBackBufferHeight = GlobalVar.height;
            graphics.PreferredBackBufferWidth = GlobalVar.wiwth;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            position = new Vector2(100, 100);
            Spaceship = new SpaceShip(this);
            //Adds the spaceship to the game components which is generic coollection
            Components.Add(Spaceship);
            ResetME();
            base.Initialize();
        }

        public void ResetME()
        {
            while (meteors.Count < 10)
            {
                var angle = random.Next() * MathHelper.TwoPi;
                var m = new Meteor()
                {
                    positin = new Vector2(GlobalVar.GPlayground.Left + (float)random.NextDouble() * GlobalVar.GPlayground.Width,
                    GlobalVar.GPlayground.Top + (float)random.NextDouble() * GlobalVar.GPlayground.Height),
                    Rotation = angle,
                    speed = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * random.Next(20, 60) / 30.0f
                };

                if (!GlobalVar.RSpawnArea.Contains(m.positin))
                    meteors.Add(m);
            }
        }


        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            




            backgroundTexture = Content.Load<Texture2D>("background");
            explosionTxt= Content.Load<Texture2D>("explosion");
            laserTexture = Content.Load<Texture2D>("laser");
            //load meteors to the content
            meteorTxt = Content.Load<Texture2D>("meteorBrown");

            //Load sound to content.
            laserS = Content.Load<SoundEffect>("laserSound");
            Scorefont = this.Content.Load<SpriteFont>("Score");
            meteorS = Content.Load<SoundEffect>("explosionSound");

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {


            float f = 1f;
            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.W))
            {
                f = f++;

                foreach (Meteor m in meteors)
                {
                    m.speed = m.speed + m.speed*f/10;

                }
               

            }

            


            if (ks.IsKeyDown(Keys.Escape))
            {
                using(var game = new Game1())
                {
                    game.Run();
                }
            }

          



            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Up))
                Spaceship.Accelerate();
            //Change the Direction Accordong to the is pressed
            if (state.IsKeyDown(Keys.Left))
                Spaceship.Rotation -= 0.05f;
            else if (state.IsKeyDown(Keys.Right))
                Spaceship.Rotation += 0.05f;
            if (state.IsKeyDown(Keys.Space))
            {
                //Count the number of shots shooed.
               
                //Add a new shot class when space is Pressed
                Shot s = Spaceship.Shoot();
                //Checks the position of shots and draw to pretend overpositions
                if (s != null)
                {

                    laserS.Play();
                    shots.Add(s);
                }
            }



                                    
            
            //Update a time of shots so the shot dont overridden
            foreach (Shot sh in shots)
            {
                sh.Update(gameTime);
                Meteor m = meteors.FirstOrDefault(x => x.collideWith(sh));

                if(m != null)
                {
                    meteors.Remove(m);
                    Lstexplosions.Add(new Explosion()
                    {
                       
                        positin = m.positin
                    }); 
                    sh.isDead = true;
                    NumberOfMeteorKilled++;
                    meteorS.Play();
                }
            }

            foreach (Explosion explosion in Lstexplosions)
                explosion.Update(gameTime);

            foreach (Meteor meteor in meteors)
                meteor.Update(gameTime);

            shots.RemoveAll(s => s.isDead || !GlobalVar.GPlayground.Contains(s.positin));
            Lstexplosions.RemoveAll(e => e.isDead);

                Spaceship.Update(gameTime);
            KeyboardState = state;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            GraphicsDevice.Clear(Color.CornflowerBlue);

            //An array to set the screen size so the game Looks compitable  
            for (int i = 0; i < GlobalVar.height; i+= backgroundTexture.Width)
            {
                for (int j = 0; j < GlobalVar.wiwth; j+= backgroundTexture.Height)
                {
                    spriteBatch.Draw(backgroundTexture, new Vector2(i,j), Color.White);
                }
            }


        

          

            //Draws the spaceship
            Spaceship.Draw(spriteBatch);
           
            foreach(Shot shot in shots)
            {
                spriteBatch.Draw(laserTexture, shot.positin, null, Color.White, shot.Rotation , new Vector2(laserTexture.Width / 2, laserTexture.Height / 2), 1.0f, SpriteEffects.None, 0f);
            }

            //Draw the Meteor and Add to the List with Properties;
            foreach (Meteor meteor in meteors)
            {
                spriteBatch.Draw(meteorTxt, meteor.positin, null, Color.White, meteor.Rotation, new Vector2(meteorTxt.Width / 2, meteorTxt.Height / 2), 1.0f, SpriteEffects.None, 0f);
            }

            foreach(Explosion explosion in Lstexplosions)
            {
                spriteBatch.Draw(explosionTxt, explosion.positin, null,explosion.Color, explosion.Rotation, new Vector2(explosionTxt.Width / 2, explosionTxt.Height / 2), 1.0f, SpriteEffects.None, 0f);

            }
            if (NumberOfMeteorKilled == 10)
            {
                spriteBatch.DrawString(Scorefont, $"You Win , Aliens Are scared Now", new Vector2(100, 100), Color.Green);
            }
            else
            {
                spriteBatch.DrawString(Scorefont, $"Your Score is : {(NumberOfMeteorKilled * 100).ToString()}", new Vector2(100, 100), Color.White);
            }
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
