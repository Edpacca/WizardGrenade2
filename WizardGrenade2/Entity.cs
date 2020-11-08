﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardGrenade2
{
    public class Entity
    {
        public int Health { get; private set; }
        public bool IsDead { get; private set; }

        public Entity(int startHealth)
        {
            Health = startHealth;
        }

        public void ApplyDamage(int damage)
        {
            Health -= damage;
            if (Health < 0)
            {
                Health = 0;
                IsDead = true;
            }
        }

        public void Kill()
        {
            IsDead = true;
        }
    }
}
