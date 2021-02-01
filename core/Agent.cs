using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoEngine.core
{
    public class Agent  : SpriteObject, IDamagable
    {

        public float Speed = 100;
        private float maxHealth;
        private float curHealth;
        private GameObject baseClass;
        public float CurMeleeAttackCoolDown = 1;
        public float MaxMeleeAttackCoolDown = 1;
        public int Team = 0;
        public Agent TargetAgent;

        public bool Dead = false;
        public Agent(Vector2 position, float  health) 
        {
            maxHealth = health;
            curHealth = maxHealth;
            baseClass = this;
        }

        public float MaxHealth { get { return maxHealth; } set { maxHealth = value; } }
        public float CurHealth { get { return curHealth; } set { curHealth = value; } }

        public GameObject BaseClass { get {return baseClass; } set { baseClass = value; } }

        public void Die()
        {
            Dead = true;
            ShouldDraw = false;
           // flagForRemoval = true;
        }

        public void TakeDamage(float dmg)
        {
            CurHealth -= dmg;
            if(CurHealth <= 0)
            {
                Die();
            }
        }

        public override void Update(float dt)
        {
            CurMeleeAttackCoolDown -= dt;
            base.Update(dt);
        }



    }
}
