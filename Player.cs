using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProgProdAvanz_Semana2
{
    internal class Player : Character
    {
        public Player(string name, int damage, int health) : base(name, damage, health)
        {
        }

        public void Rest()
        {
            int healthToRestore = (int)(MaxHealth * 0.3); //30% de la vida
            Health += healthToRestore;
            if (Health > MaxHealth) Health = MaxHealth;
            Console.WriteLine($"{Name} descansa y recupera {healthToRestore} puntos de vida. Vida actual: {Health}/{MaxHealth}");
        }
    }
}
