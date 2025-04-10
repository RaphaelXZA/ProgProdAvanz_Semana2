using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgProdAvanz_Semana2
{
    internal class GameManager
    {
        public Player Player { get; private set; }
        public GameNode? CurrentNode { get; private set; }
        public GameNode? FirstNode { get; private set; }
        private bool isGameOver = false;
        private Random random = new Random();

        public GameManager(Player player, GameNode firstNode)
        {
            Player = player;
            CurrentNode = firstNode;
            FirstNode = firstNode;
        }

        public void Start()
        {
            Console.Clear();
            Console.WriteLine($"¡Bienvenido a la aventura, {Player.Name}!");
            Console.WriteLine($"Vida: {Player.Health}, Daño: {Player.Damage}");
            Console.WriteLine("\nPresiona Enter para comenzar...");
            Console.ReadLine();

            GameLoop();
        }

        public void GameLoop()
        {
            while (CurrentNode != null && !isGameOver)
            {
                Console.Clear();
                DisplayMinimap();
                CurrentNode.Execute(this);
            }

            if (!isGameOver && CurrentNode == null)
            {
                Console.Clear();
                Console.WriteLine("\n¡Has llegado al final de tu aventura!");
                Console.WriteLine($"Felicidades, {Player.Name}. Has completado el juego con {Player.Health}/{Player.MaxHealth} puntos de vida restantes.");
            }
        }

        public void MoveToNextNode()
        {
            CurrentNode = CurrentNode?.Next;
        }

        public void GameOver()
        {
            isGameOver = true;
            Console.WriteLine("\nFin del juego. Presiona Enter para salir...");
            Console.ReadLine();
        }

        public void DisplayMinimap()
        {
            string minimap = "";
            GameNode? node = FirstNode;

            while (node != null)
            {
                if (node == CurrentNode)
                {
                    minimap += 'J';
                }
                else
                {
                    minimap += node.GetMapChar();
                }
                node = node.Next;
            }

            Console.WriteLine("Minimapa: " + minimap);
            Console.WriteLine(new string('-', 50));
        }

        public static GameManager CreateRandomGame(Player player, int nodeCount)
        {
            Random random = new Random();
            GameNode? firstNode = null;
            GameNode? currentNode = null;
            int difficulty = 1;

            for (int i = 0; i < nodeCount; i++)
            {
                //Elegir tipo de nodo
                NodeType nodeType;
                INodeAction nodeAction;

                //Aumentar gradualmente la dificultad
                if (i > nodeCount / 2) difficulty = 2;
                if (i > (nodeCount * 0.8)) difficulty = 3;

                //Asegura que el primer nodo sea de avance y el último sea de decisión
                if (i == 0)
                {
                    nodeType = NodeType.Advance;
                    nodeAction = new AdvanceAction();
                }
                else if (i == nodeCount - 1)
                {
                    nodeType = NodeType.Decision;
                    nodeAction = CreateRandomDecisionAction(true); //Final del juego
                }
                else
                {
                    //Distribución aleatoria de tipos de nodos
                    int typeRoll = random.Next(100);

                    if (typeRoll < 50) //50% chance de zona de avance
                    {
                        nodeType = NodeType.Advance;
                        nodeAction = new AdvanceAction();
                    }
                    else if (typeRoll < 80) //30% chance de combate
                    {
                        nodeType = NodeType.Combat;
                        nodeAction = CreateRandomCombatAction(difficulty);
                    }
                    else //20% chance de decisión
                    {
                        nodeType = NodeType.Decision;
                        nodeAction = CreateRandomDecisionAction(false);
                    }
                }

                GameNode newNode = new GameNode(nodeType, nodeAction);

                if (firstNode == null)
                {
                    firstNode = newNode;
                    currentNode = newNode;
                }
                else
                {
                    currentNode!.Next = newNode;
                    currentNode = newNode;
                }
            }

            return new GameManager(player, firstNode!);
        }

        private static CombatAction CreateRandomCombatAction(int difficulty)
        {
            Random random = new Random();
            int enemyCount = random.Next(1, 4); //De 1 a 3 enemigos
            List<Enemy> enemies = new List<Enemy>();

            for (int i = 0; i < enemyCount; i++)
            {
                int enemyDifficulty = Math.Max(1, difficulty + random.Next(-1, 2));
                enemies.Add(Enemy.CreateRandomEnemy(enemyDifficulty));
            }

            return new CombatAction(enemies);
        }

        private static DecisionAction CreateRandomDecisionAction(bool isFinal)
        {
            List<string> options = new List<string>();
            List<string> consequences = new List<string>();

            if (isFinal)
            {
                //Opciones para el final del juego
                options.Add("Regresar a tu pueblo con las riquezas obtenidas");
                options.Add("Continuar explorando nuevas tierras");
                options.Add("Quedarte en este lugar y construir un nuevo hogar");

                consequences.Add("Regresas a tu pueblo como un héroe. Todos te reciben con una gran celebración y vives cómodamente con tu reputación y riquezas.");
                consequences.Add("Decides que tu espíritu aventurero aún no está satisfecho. Te adentras en tierras desconocidas, listo para nuevos desafíos.");
                consequences.Add("Encuentras paz en este lugar. Con el tiempo, construyes un pequeño refugio que eventualmente se convierte en un próspero asentamiento.");
            }
            else
            {
                //Escenarios de toma de decisiones
                string[][] scenarios = {
                new string[] {
                    "Encuentras un río caudaloso bloqueando tu camino",
                    "Intentar cruzar nadando",
                    "Buscar un puente más adelante",
                    "Construir una balsa con troncos cercanos",
                    "Te lanzas al agua, pero la corriente es fuerte. Afortunadamente, logras cruzar con apenas unos rasguños.",
                    "Después de caminar por un rato, encuentras un viejo puente de piedra. Lo cruzas sin problemas.",
                    "Pasas tiempo reuniendo troncos y construyendo una balsa. Cruzas el río sin dificultad y continúas tu camino."
                },
                new string[] {
                    "En el camino te encuentras con un viajero herido",
                    "Ayudarlo y compartir tus provisiones",
                    "Ignorarlo y seguir tu camino",
                    "Revisar sus pertenencias mientras está débil",
                    "Curas sus heridas y compartes comida. Te agradece y te entrega un pequeño amuleto que según él, trae suerte.",
                    "Pasas de largo, priorizando tu propia seguridad. Desde lejos, lo escuchas maldecir tu nombre.",
                    "Mientras revisas sus cosas, el viajero despierta y te confronta. Avergonzado, te disculpas y sigues tu camino."
                },
                new string[] {
                    "Frente a ti hay una cueva oscura y una colina escarpada",
                    "Explorar la cueva",
                    "Subir por la colina",
                    "Acampar y esperar al amanecer",
                    "La cueva resulta ser el hogar de un ermitaño que te cuenta historias fascinantes sobre la región.",
                    "Desde la cima de la colina, ves un valle hermoso y logras orientarte mejor en tu viaje.",
                    "Descansas bien y al amanecer, el camino se ve más claro. Continúas con energías renovadas."
                },
                new string[] {
                    "Encuentras una antigua estatua con una inscripción indescifrable",
                    "Intentar descifrar la inscripción",
                    "Dejar una ofrenda ante la estatua",
                    "Ignorarla y seguir adelante",
                    "Después de mucho esfuerzo, comprendes que habla de un tesoro oculto en las montañas cercanas.",
                    "Dejas una pequeña ofrenda. Sientes una extraña sensación de bienestar al continuar tu camino.",
                    "Decides no perder tiempo con antiguallas. Sigues tu camino sin mirar atrás."
                }
            };

                //Elegir un escenario aleatorio
                Random random = new Random();
                string[] scenario = scenarios[random.Next(scenarios.Length)];

                Console.WriteLine(scenario[0]); //Descripción del escenario

                options.Add(scenario[1]);
                options.Add(scenario[2]);
                options.Add(scenario[3]);

                consequences.Add(scenario[4]);
                consequences.Add(scenario[5]);
                consequences.Add(scenario[6]);
            }

            return new DecisionAction(options, consequences);
        }
    }
}
