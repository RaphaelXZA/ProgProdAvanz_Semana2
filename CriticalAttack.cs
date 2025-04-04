using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgProdAvanz_Semana2
{
    internal class CriticalAttack : IAttackBehavior
    {
        private int criticalDamage;
        private int criticalChance = 20;
        private Random random = new Random();

        public CriticalAttack(int damage)
        {
            criticalDamage = damage;
        }

        public int Attack()
        {
            return random.Next(1, 101) <= criticalChance ? criticalDamage * 2 : criticalDamage;
        }
    }
}
