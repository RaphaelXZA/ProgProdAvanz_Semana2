using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgProdAvanz_Semana2
{
    internal class Enemy : Character
    {
        private static string[] enemyTypes = { "Goblin", "Orco", "Lobo", "Esqueleto", "Troll", "Murciélago", "Araña", "Bruja", "Zombi" };
        private static Random random = new Random();

        public Enemy(string name, int damage, int health) : base(name, damage, health)
        {
        }

        public static Enemy CreateRandomEnemy(int difficulty)
        {
            string name = enemyTypes[random.Next(enemyTypes.Length)];

            int baseHealth = 20 + (difficulty * 5);
            int baseDamage = 5 + (difficulty * 2);

            int health = baseHealth + random.Next(-5, 6);
            int damage = baseDamage + random.Next(-2, 3);

            return new Enemy($"{name} Lvl.{difficulty}", damage, health);
        }
    }
}
