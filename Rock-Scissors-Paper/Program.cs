using System.Text;
using Rock_Scissors_Paper.Enums;

namespace Rock_Scissors_Paper
{
    public static class Program
    {
        private static readonly Random Random = new();
        private static int _totalRounds;
        private static int _totalVictories;
        private static string? _name = string.Empty;
        private static int _age;
        
        public static void Main()
        {
            Console.ResetColor();
            
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine(GameAssets.WelcomeText);
            Thread.Sleep(2000);
            
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
            Thread.Sleep(1000);
            Console.WriteLine("\u2552\u2550\u2550\u2550\u2550\u2550\u2550\u2555");
            
            Console.WriteLine($"\u2570\u2508\u27a4 Ім'я: {_name}");
            Console.WriteLine($"\u2570\u2508\u27a4 Вік: {_age}");
            Console.WriteLine($"\u2570\u2508\u27a4 Кількість зіграних раундів: {_totalRounds}");
            Console.WriteLine($"\u2570\u2508\u27a4 Кількість перемог: {_totalVictories}");

            Console.WriteLine("\u2558\u2550\u2550\u2550\u2550\u2550\u2550\u255b");
            Thread.Sleep(1000);
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
            Thread.Sleep(2000);
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
                ProcessResult(ConsoleColor.DarkGreen, GameAssets.WinText, GetRandomPraiseMessage());
            }
            else
                ProcessResult(ConsoleColor.DarkRed, GameAssets.FailText, GetRandomEncouragementMessage());
            
            ChangeGameState();
        }

        private static void ProcessResult(ConsoleColor color, string text, string message)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
            Console.WriteLine(message);
            Thread.Sleep(2000);
            ShowStatistics();
        }

        private static int Battle()
        {
            int victories = 0;
            for (int round = 0; round < 3; round++)
            {
                Weapons playerWeapon = ChooseWeapon();
                Weapons enemyWeapon = GetEnemyRandomWeapon();
                
                ShowBattle(playerWeapon, enemyWeapon);
                BattleState battleState = DetermineWinner(playerWeapon, enemyWeapon);
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

        private static void ShowBattle(Weapons playerWeapon, Weapons enemyWeapon)
        {
            Thread.Sleep(2000);
            Console.WriteLine($"{GameAssets.PlayerWeapons[playerWeapon]}  \nVS  {GameAssets.AiWeapons[enemyWeapon]}");
            Thread.Sleep(2000);
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
            Thread.Sleep(2000);
        }

        private static Weapons ChooseWeapon()
        {
            Console.WriteLine("\nОберіть свою зброю!" +
                              $"\n{(int)Weapons.Paper}) Папір" +
                              $"\n{(int)Weapons.Rock}) Каміння" +
                              $"\n{(int)Weapons.Scissors}) Ножиці");

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