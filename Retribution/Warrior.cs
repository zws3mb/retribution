﻿using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Retribution
{
    //  A tanky, high healthed, unit. Has armor that takes extra damage from magic
    class Warrior : Mobile
    {
        public static int cost = 5;
        public Warrior(Vector2 position, int health = 30, int damage = 15, int attackRange = 50)
            : base(health, position, damage, attackRange)
        {
            //this.health = 40;
            //this.damage = 6;
            //this.attackSpeed = 2;
            this.moveSpeed = 1;
            attackSpeed = 320;
            type = "WARRIOR";
            //this.animationState        //  The actual animation the object is performing (moving left, moving right, attacking, etc.)
            //this.animationFrame   //  Keeps track of the animation frame the object is on
            //this.animationTime    //  Calculates how much time has passed since animation began
            //this.attackReady = false;
            //this.isArmored = true;
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("warrior.png");
        }
        public override void attackSound(ContentManager content)
        {
            SoundEffect soundEffect = content.Load<SoundEffect>("clang.wav");
            soundEffect.Play();
        }

    }
}