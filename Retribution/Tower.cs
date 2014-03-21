﻿using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Retribution
{
    
    class Tower : GameObject
    {
        public string state;
        public Texture2D image;
        public Texture2D image2;

        public Tower(Vector2 position, int health = 2, int damage = 0, int attackRange = 0)
            : base (health, position, damage, attackRange)
        {
            this.position = position;
            this.state = "Wall";
        }

        public void Morph(string mobiletype = "Wall")
        {
            if (mobiletype == "archer")
            {
                this.state = "RangedTower";
                this.damage = 2;
                this.attackRange = 5;
            }
            else if (mobiletype == "warrior")
            {
                this.state = "EarthquakeTower";
                this.damage = 3;
                this.attackRange = 1;
            }
        }

<<<<<<< HEAD
<<<<<<< HEAD
        //public override void Die()
        //{
        //    this.alive = false;
        //}
=======
>>>>>>> 3d0754205313a76004001e81ca806258029c275e
=======
>>>>>>> 3d0754205313a76004001e81ca806258029c275e

        public void LoadContent(ContentManager content)
        {
            this.image = content.Load<Texture2D>("tower.png");
            this.image2 = content.Load<Texture2D>("dead.png");
        }

<<<<<<< HEAD
<<<<<<< HEAD
        public void Update(GameTime gameTime)
        {
            if (this.alive)
            {
                // towers can't move so no movement option
                // TODO: attack option
                // TODO: Morph option

                if (this.health <= 0)
                {
                    //this.Die();
                    Console.Write("hello there");
                    this.position = new Vector2(0, 400);
                }
            }
        }
=======
>>>>>>> 3d0754205313a76004001e81ca806258029c275e
=======
>>>>>>> 3d0754205313a76004001e81ca806258029c275e

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Begin();
            spriteBatch.Draw(image, new Rectangle((int)this.position.X,(int)this.position.Y, 50, 50), Color.White);
            //spriteBatch.End();
            // draw the sprite here
        }
        
        /*
        public void GetValues()
        {
            string output = string.Format("{0}:, {1}:, {2}:", this.health, this.damage, this.position);
            Console.WriteLine(output);
        }

        static void Main()
        {
            Tower tower = new Tower(new Vector2(20, 20));
            Tower tower2 = new Tower(new Vector2(22, 20));
            tower.GetValues();
            tower.Morph("Wall");
            Console.WriteLine(tower.state);
            tower2.Morph("archer");
            Console.WriteLine(tower.state);
            Console.WriteLine(string.Format("The tower's damage is now: {0}", tower2.damage));
            //tower.Morph("warrior");
            //Console.WriteLine(tower.state);
            Console.WriteLine(string.Format("The tower is alive: {0}",tower.alive));
            tower2.Attack(tower);
            Console.WriteLine(string.Format("The tower's health is: {0}", tower.health));
            List<Vector2> myranges = tower2.tiles_in_range;
            for (int i = 0; i < tower2.tiles_in_range.Count; i++)
            {
                Console.WriteLine(myranges[i]);
            }
        }
         * */
    }
}
