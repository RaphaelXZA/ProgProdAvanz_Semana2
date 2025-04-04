using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgProdAvanz_Semana2
{
    internal class NormalAttack : IAttackBehavior
    {
        private int normalDamage;

        public NormalAttack(int damage)
        {
            normalDamage = damage;
        }

        public int Attack()
        {
            return normalDamage;
        }
    }
}
