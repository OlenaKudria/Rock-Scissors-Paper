using System.Text;
namespace Rock_Scissors_Paper
{
    public static class Program
    {
        private static readonly Random Random = new Random();
        private static int _totalRounds = 0;
        private static int _totalVictories = 0;
        private static string? _name = string.Empty;
        private static int _age = 0;
        
        public static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine(GameAssets.WelcomeText);
            Console.ReadKey();
            
            _name = GetName();
            _age = GetAge();
            
            ShowStatistics();
            ChangeGameState();
        }

        private static string GetName()
        {
            Console.WriteLine(new string('-', Console.WindowWidth));
            Console.Write("Ім'я: ");
            
            string? name = Console.ReadLine();

            if (!string.IsNullOrEmpty(name)) 
                return name;
            
            Console.WriteLine("\nТи маєш ввести своє ім'я для продовження :(");
            return GetName();
        }
        
        private static int GetAge()
        {
            Console.WriteLine(new string('-', Console.WindowWidth));
            Console.Write("Вік: ");

            if (int.TryParse(Console.ReadLine(), out int age))
            {
                CheckAge(age);
                return age;
            }
            
            Console.WriteLine("\nВік це числа!");
            return GetAge();
        }

        private static void CheckAge(int age)
        {
            int restrictionAge = 12;

            if (age >= restrictionAge) return;

            Console.WriteLine($"Вибач, герою, але тобі має бути принаймі {restrictionAge} років!");
            EndGame();
        }
        
        private static void ShowStatistics()
        {
            Console.WriteLine("\u2552\u2550\u2550\u2550\u2550\u2550\u2550\u2555");
            
            Console.WriteLine($"\u2570\u2508\u27a4 Ім'я: {_name}");
            Console.WriteLine($"\u2570\u2508\u27a4 Вік: {_age}");
            Console.WriteLine($"\u2570\u2508\u27a4 Кількість зіграних раундів: {_totalRounds}");
            Console.WriteLine($"\u2570\u2508\u27a4 Кількість перемог: {_totalVictories}");

            Console.WriteLine("\u2558\u2550\u2550\u2550\u2550\u2550\u2550\u255b");
        }

        private static void ChangeGameState()
        {
            int answer = AskForStart();
            
            if (answer == (int)GameState.Start)
            {
                ShowGameRules();
                StartGame();
            }
            else
                EndGame();
        }

        private static int AskForStart()
        {
            Console.WriteLine(new string('-', Console.WindowWidth));
            
            Console.WriteLine($"Готовий, герою, розпочати свій шлях і випробування, що чекають на тебе?" +
                              $"\nТвоя відвага та винахідливість будуть ключем до твоєї перемоги! " +
                              $"\n{(int)GameState.Start}) Готовий!!! " +
                              $"\n{(int)GameState.End}) Краще я піду...");
            
            
            if (int.TryParse(Console.ReadLine(), out int answer))
                if (Enum.IsDefined(typeof(GameState), answer))
                    return answer;
            
            Console.WriteLine("\nОберіть один з варіантів!");
            return AskForStart();
        }

        private static void StartGame()
        {
            int result = Battle();
            _totalRounds += 3;

            if (result >= 2)
            {
                _totalVictories++;
                Console.WriteLine(GameAssets.WinText);
                Console.WriteLine(GetRandomPraiseMessage());
                ShowStatistics();
            }
            else
            {
                Console.WriteLine(GameAssets.FailText);
                Console.WriteLine(GetRandomEncouragementMessage());
                ShowStatistics();
            }

            ChangeGameState();
        }

        private static int Battle()
        {
            int victories = 0;
            for (int round = 0; round < 3; round++)
            {
                Weapons playerWeapon = ChooseWeapon();
                Weapons enemyWeapon = GetEnemyRandomWeapon();
                
                BattleState battleState = DetermineWinner(playerWeapon, enemyWeapon);
                //todo show battle
                Console.WriteLine(battleState);

                if (battleState == BattleState.PlayerWin)
                    victories++;
            }

            return victories;
        }

        private static BattleState DetermineWinner(Weapons playerWeapon, Weapons enemyWeapon)
        {
            if (playerWeapon == enemyWeapon) return BattleState.Draw;

            bool userWins = (playerWeapon == Weapons.Paper && enemyWeapon == Weapons.Rock) ||
                            (playerWeapon == Weapons.Rock && enemyWeapon == Weapons.Scissors) ||
                            (playerWeapon == Weapons.Scissors && enemyWeapon == Weapons.Paper);

            return userWins ? BattleState.PlayerWin : BattleState.EnemyWin;
        }

        private static void EndGame()
        {
            Console.WriteLine($"До зустрічі, {_name}!");
            Environment.Exit(0);
        }

        private static void ShowGameRules()
        {
            Console.WriteLine(new string('-', Console.WindowWidth));
            Console.WriteLine(GameAssets.RulesText);
            Console.ReadKey();
        }

        private static Weapons ChooseWeapon()
        {
            Console.WriteLine("\nОберіть свою зброю!" +
                              $"\n{(int)Weapons.Paper}) Папір" +
                              $"\n{(int)Weapons.Rock}) Каміння" +
                              $"\n{(int)Weapons.Scissors}) Ножиці\"");

            if (int.TryParse(Console.ReadLine(), out int weapon))
                if (Enum.IsDefined(typeof(Weapons), weapon))
                    return (Weapons)weapon;
            
            Console.WriteLine("\nОберіть один з варіантів!");
            return ChooseWeapon();
        }
        
        private static Weapons GetEnemyRandomWeapon() => 
            (Weapons)Random.Next(1, Enum.GetNames(typeof(Weapons)).Length + 1);

        private static string GetRandomEncouragementMessage() =>
            GameAssets.EncouragementMessages[Random.Next(GameAssets.EncouragementMessages.Length)];
        
        private static string GetRandomPraiseMessage() => 
            GameAssets.PraiseMessages[Random.Next(GameAssets.PraiseMessages.Length)];
    }
}