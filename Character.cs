using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgProdAvanz_Semana2
{
    internal abstract class Character
    {
        public string Name { get; protected set; }
        public int Damage { get; protected set; }
        public int Health { get; protected set; }
        public int MaxHealth { get; protected set; }

        protected IAttackBehavior attackBehavior;

        public Character(string name, int damage, int health)
        {
            Name = name;
            Damage = damage;
            Health = health;
            MaxHealth = health;
            attackBehavior = new NormalAttack(damage);
        }

        public bool IsAlive()
        {
            return Health > 0;
        }

        public virtual int PerformAttack()
        {
            return attackBehavior.Attack();
        }

        public virtual void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health < 0) Health = 0;
        }

        public virtual void SetAttackBehavior(IAttackBehavior attackBehavior)
        {
            this.attackBehavior = attackBehavior;
        }
    }
}
