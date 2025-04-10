using ProgProdAvanz_Semana2;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.Title = "Aventura RPG con nombre generico!";
        ShowBanner();
        
        Player player = CreatePlayer();
       
        GameManager gameManager = GameManager.CreateRandomGame(player, 15);

        gameManager.Start();
    }

    private static void ShowBanner() //NOTA: Jugar con la ventana maximizada.
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(@"
 ██████╗ ███████╗███╗   ██╗███████╗██████╗ ██╗ ██████╗    ██████╗ ██████╗  ██████╗     █████╗ ██████╗ ██╗   ██╗███████╗███╗   ██╗████████╗██╗   ██╗██████╗ ███████╗
██╔════╝ ██╔════╝████╗  ██║██╔════╝██╔══██╗██║██╔════╝    ██╔══██╗██╔══██╗██╔════╝    ██╔══██╗██╔══██╗██║   ██║██╔════╝████╗  ██║╚══██╔══╝██║   ██║██╔══██╗██╔════╝
██║  ███╗█████╗  ██╔██╗ ██║█████╗  ██████╔╝██║██║         ██████╔╝██████╔╝██║  ███╗   ███████║██║  ██║██║   ██║█████╗  ██╔██╗ ██║   ██║   ██║   ██║██████╔╝█████╗  
██║   ██║██╔══╝  ██║╚██╗██║██╔══╝  ██╔══██╗██║██║         ██╔══██╗██╔═══╝ ██║   ██║   ██╔══██║██║  ██║╚██╗ ██╔╝██╔══╝  ██║╚██╗██║   ██║   ██║   ██║██╔══██╗██╔══╝  
╚██████╔╝███████╗██║ ╚████║███████╗██║  ██║██║╚██████╗    ██║  ██║██║     ╚██████╔╝   ██║  ██║██████╔╝ ╚████╔╝ ███████╗██║ ╚████║   ██║   ╚██████╔╝██║  ██║███████╗
 ╚═════╝ ╚══════╝╚═╝  ╚═══╝╚══════╝╚═╝  ╚═╝╚═╝ ╚═════╝    ╚═╝  ╚═╝╚═╝      ╚═════╝    ╚═╝  ╚═╝╚═════╝   ╚═══╝  ╚══════╝╚═╝  ╚═══╝   ╚═╝    ╚═════╝ ╚═╝  ╚═╝╚══════╝
");
        Console.ResetColor();
        Console.WriteLine("Bienvenido a Aventura RPG con nombre generico!");
        Console.WriteLine("Una odisea llena de peligros y decisiones (que no importan para nada al final) te espera...");
        Console.WriteLine("\nPresiona cualquier tecla para comenzar...");
        Console.ReadKey();
    }

    private static Player CreatePlayer()
    {
        Console.Clear();
        Console.WriteLine("== Creación de Personaje ==");
        Console.WriteLine("Tienes 100 puntos para distribuir entre Vida y Daño");

        Console.Write("\nIngresa el nombre de tu personaje: ");
        string name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name))
        {
            name = "Aventurero";
            Console.WriteLine($"Usaremos '{name}' como tu nombre.");
        }

        int health = 0;
        int damage = 0;
        int pointsLeft = 100;

        //Asignar vida
        while (true)
        {
            Console.Write($"\nPuntos disponibles: {pointsLeft}");
            Console.Write("\nPuntos de Vida (mínimo 1): ");

            if (int.TryParse(Console.ReadLine(), out health) && health >= 1 && health <= pointsLeft)
            {
                pointsLeft -= health;
                break;
            }
            else
            {
                Console.WriteLine("Valor inválido. Debes asignar al menos 1 punto y no exceder los puntos disponibles.");
            }
        }

        //Asignar daño
        while (true)
        {
            Console.Write($"\nPuntos disponibles: {pointsLeft}");
            Console.Write("\nPuntos de Daño (mínimo 1): ");

            if (int.TryParse(Console.ReadLine(), out damage) && damage >= 1 && damage <= pointsLeft)
            {
                pointsLeft -= damage;
                break;
            }
            else
            {
                Console.WriteLine("Valor inválido. Debes asignar al menos 1 punto y no exceder los puntos disponibles.");
            }
        }

        if (pointsLeft > 0)
        {
            Console.WriteLine($"\nQuedan {pointsLeft} puntos sin asignar. Se agregarán a tu Vida.");
            health += pointsLeft;
        }

        Console.WriteLine($"\nPersonaje creado:");
        Console.WriteLine($"Nombre: {name}");
        Console.WriteLine($"Vida: {health}");
        Console.WriteLine($"Daño: {damage}");
        Console.WriteLine("\nPresiona Enter para comenzar la aventura...");
        Console.ReadLine();

        return new Player(name, damage, health);
    }
}