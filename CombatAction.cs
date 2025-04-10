using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgProdAvanz_Semana2
{
    internal class CombatAction : INodeAction
    {
        private List<Enemy> enemies;
        private bool combatEnded = false;

        public CombatAction(List<Enemy> enemies)
        {
            this.enemies = enemies;
        }

        public void Execute(GameManager gameManager)
        {
            if (enemies.Count == 0 || combatEnded)
            {
                Console.WriteLine("\nHas derrotado a todos los enemigos. Puedes continuar tu camino.");
                Console.WriteLine("Presiona Enter para avanzar...");
                Console.ReadLine();
                gameManager.MoveToNextNode();
                return;
            }

            Console.WriteLine("\n¡Te has encontrado con enemigos!");

            foreach (var enemy in enemies)
            {
                Console.WriteLine($" - {enemy.Name} (Vida: {enemy.Health}, Daño: {enemy.Damage})");
            }

            Console.WriteLine("\n¿Qué quieres hacer?");
            Console.WriteLine("1. Atacar");
            Console.WriteLine("2. Huir (perderás algo de vida)");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                //Selecciona enemigo a atacar
                if (enemies.Count > 1)
                {
                    Console.WriteLine("\n¿A qué enemigo quieres atacar?");
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {enemies[i].Name} (Vida: {enemies[i].Health})");
                    }

                    int targetIndex;
                    while (!int.TryParse(Console.ReadLine(), out targetIndex) || targetIndex < 1 || targetIndex > enemies.Count)
                    {
                        Console.WriteLine("Selección inválida. Intenta de nuevo:");
                    }

                    Enemy target = enemies[targetIndex - 1];
                    Player player = gameManager.Player;

                    //Ataque del jugador
                    int playerDamage = player.PerformAttack();
                    target.TakeDamage(playerDamage);
                    Console.WriteLine($"\n{player.Name} ataca a {target.Name} causando {playerDamage} puntos de daño.");
                    Console.WriteLine($"{target.Name} tiene {target.Health} puntos de vida restantes.");

                    //Verifica si el enemigo murió
                    if (!target.IsAlive())
                    {
                        Console.WriteLine($"{target.Name} ha sido derrotado!");
                        enemies.Remove(target);
                    }
                }
                else if (enemies.Count == 1)
                {
                    //Si solo hay un enemigo ataca directamente
                    Enemy target = enemies[0];
                    Player player = gameManager.Player;

                    int playerDamage = player.PerformAttack();
                    target.TakeDamage(playerDamage);
                    Console.WriteLine($"\n{player.Name} ataca a {target.Name} causando {playerDamage} puntos de daño.");
                    Console.WriteLine($"{target.Name} tiene {target.Health} puntos de vida restantes.");

                    if (!target.IsAlive())
                    {
                        Console.WriteLine($"{target.Name} ha sido derrotado!");
                        enemies.Remove(target);
                    }
                }

                //Los enemigos atacan al jugador
                foreach (var enemy in enemies.ToList())
                {
                    int enemyDamage = enemy.PerformAttack();
                    gameManager.Player.TakeDamage(enemyDamage);
                    Console.WriteLine($"{enemy.Name} ataca a {gameManager.Player.Name} causando {enemyDamage} puntos de daño.");
                }

                Console.WriteLine($"\n{gameManager.Player.Name} tiene {gameManager.Player.Health}/{gameManager.Player.MaxHealth} puntos de vida.");

                //Verifica si el jugador murió
                if (!gameManager.Player.IsAlive())
                {
                    Console.WriteLine("\n¡Has sido derrotado! Fin del juego.");
                    gameManager.GameOver();
                    return;
                }

                //Si todos los enemigos murieron
                if (enemies.Count == 0)
                {
                    Console.WriteLine("\n¡Has derrotado a todos los enemigos!");
                    Console.WriteLine("Presiona Enter para continuar...");
                    Console.ReadLine();

                    //Dar la opción de descansar para recuperar vida
                    Console.WriteLine("\n¿Quieres descansar para recuperar algo de vida?");
                    Console.WriteLine("1. Sí");
                    Console.WriteLine("2. No");

                    string restChoice = Console.ReadLine();
                    if (restChoice == "1")
                    {
                        gameManager.Player.Rest();
                    }

                    combatEnded = true;
                }
            }
            else if (choice == "2")
            {
                //El jugador huye y recibe daño
                Console.WriteLine("\nDecides huir del combate. Recibes daño al escapar.");
                int escapeDamage = 10;
                gameManager.Player.TakeDamage(escapeDamage);
                Console.WriteLine($"Has perdido {escapeDamage} puntos de vida. Vida actual: {gameManager.Player.Health}/{gameManager.Player.MaxHealth}");

                //Verificar si el jugador murió al huir
                if (!gameManager.Player.IsAlive())
                {
                    Console.WriteLine("\n¡Has muerto al intentar huir! Fin del juego.");
                    gameManager.GameOver();
                    return;
                }

                combatEnded = true;
            }
            else
            {
                Console.WriteLine("Opción inválida. Intenta de nuevo.");
            }
        }
    }
}
