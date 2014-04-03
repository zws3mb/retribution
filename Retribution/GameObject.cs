﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Retribution
{
    //  The overarching "Grandpa" class of all objects in the game (projectiles, towers, walls, boulders, etc.)
    abstract class GameObject
    {
        public Vector2 position;
        public Texture2D texture;
        public int health;
        public int damage;
        public int attackRange;
        public bool alive;
        public bool selected;
        public string type;

        public GameObject(int health, Vector2 position, int damage, int attackRange)
        {
            this.health = health;
            this.position = position;
            this.damage = damage;
            this.attackRange = attackRange;
            this.alive = true;
            this.selected = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)this.position.X, (int)this.position.Y, 50, 50), Color.White);
        }
        //  A rectangle to represent the object
        public Rectangle Bounds
        {
            get { return new Rectangle((int)position.X, (int)position.Y, this.texture.Width, this.texture.Height); }
        }

        //  Issue attack. Alpha method that damages target. No other skills or actions are implemented in the Alpha Version
        public void Attack(GameObject target)
        {
            target.health -= this.damage;
        }

        public Boolean IsInRange(GameObject target)
        {
           double distance;
           distance = (int) Math.Sqrt(Math.Pow((this.position.X - target.position.X), 2) + Math.Pow((this.position.Y - target.position.Y), 2));
           if (distance <= this.attackRange)
           {
               return true;
           }
           else return false;
        }

        //public abstract void Die();

        //  Return true if this object collides with target object
        public bool collidesWith(GameObject target)
        {
            if (this.Bounds.Intersects(target.Bounds))
                return true;
            return false;
        }

        //  Returns true if object is alive
        public bool isAlive()
        {
            if (health > 0)
                return true;
            else
            {
                this.alive = false;
                return false;
            }
        }

        //  Getters and Setters
        public Vector2 getPosition()
        {
            return this.position;
        }

        public Boolean isSelectable(MouseState mouse)
        {
            if ((mouse.X >= this.Bounds.Left && mouse.X <= this.Bounds.Right)
                && (mouse.Y >= this.Bounds.Top && mouse.Y <= this.Bounds.Bottom))
                return true;
            else
            {
                return false;
            }
        }

        public abstract void LoadContent(ContentManager content);
    }
}
