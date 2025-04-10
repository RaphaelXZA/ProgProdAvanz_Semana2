using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgProdAvanz_Semana2
{
    internal class DecisionAction : INodeAction
    {
        private List<string> options;
        private List<string> consequences;

        public DecisionAction(List<string> options, List<string> consequences)
        {
            this.options = options;
            this.consequences = consequences;
        }

        public void Execute(GameManager gameManager)
        {
            Console.WriteLine("\nHas llegado a un punto donde debes tomar una decisión:");

            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }

            int choice;
            do
            {
                Console.Write("Elige una opción: ");
            } while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > options.Count);

            //Muestra consecuencia de la decisión
            Console.WriteLine($"\n{consequences[choice - 1]}");
            Console.WriteLine("\nPresiona Enter para continuar...");
            Console.ReadLine();

            gameManager.MoveToNextNode();
        }
    }
}
