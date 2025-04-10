using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgProdAvanz_Semana2
{
    internal class GameNode
    {
        public NodeType Type { get; private set; }
        public INodeAction Action { get; private set; }
        public GameNode? Next { get; set; }

        public GameNode(NodeType type, INodeAction action)
        {
            Type = type;
            Action = action;
            Next = null;
        }

        public void Execute(GameManager gameManager)
        {
            Action.Execute(gameManager);
        }

        public char GetMapChar()
        {
            switch (Type)
            {
                case NodeType.Advance:
                    return 'A';
                case NodeType.Combat:
                    return 'C';
                case NodeType.Decision:
                    return 'D';
                default:
                    return '?';
            }
        }
    }
}
