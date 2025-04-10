using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgProdAvanz_Semana2
{
    internal class AdvanceAction : INodeAction
    {
        public void Execute(GameManager gameManager)
        {
            Console.WriteLine("\nEstás en una zona tranquila. Puedes avanzar sin problemas.");
            Console.WriteLine("Presiona Enter para continuar...");
            Console.ReadLine();
            gameManager.MoveToNextNode();
        }
    }
}
