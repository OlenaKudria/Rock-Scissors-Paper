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
            Console.WriteLine(GameAssets.Logo);
            Console.WriteLine(GameAssets.WelcomeText);
            
            _name = GetName();
            _age = GetAge();
            
            ShowStatistics();
            ChangeGameState();
        }

        private static string GetName()
        {
            Console.Write("Name: ");
            
            string? name = Console.ReadLine();

            if (!string.IsNullOrEmpty(name)) 
                return name;
            
            Console.WriteLine("\nYou need to enter your name to continue :(");
            return GetName();
        }
        
        private static int GetAge()
        {
            Console.Write("Age: ");

            if (int.TryParse(Console.ReadLine(), out int age))
            {
                CheckAge(age);
                return age;
            }
            
            Console.WriteLine("\nAge is number!");
            return GetAge();
        }

        private static void CheckAge(int age)
        {
            int restrictionAge = 12;

            if (age >= restrictionAge) return;

            Console.WriteLine($"Sorry, hero, but you must be at least {restrictionAge} years old!");
            EndGame();
        }
        
        private static void ShowStatistics()
        {
            Console.ReadKey();
            Console.WriteLine("\u2552\u2550\u2550\u2550\u2550\u2550\u2550\u2555");
            
            Console.WriteLine($"Name: {_name}");
            Console.WriteLine($"Age: {_age}");
            Console.WriteLine($"Total rounds played: {_totalRounds}");
            Console.WriteLine($"Total victories: {_totalVictories}");

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
            Console.ReadKey();
    
            Console.WriteLine($"Are you ready, hero, to embark on your journey and face the trials ahead?" +
                              $"\nYour courage and ingenuity will be the key to your victory! " +
                              $"\n{(int)GameState.Start}) Ready!!! " +
                              $"\n{(int)GameState.End}) I'd rather not...");
    
    
            if (int.TryParse(Console.ReadLine(), out int answer))
                if (Enum.IsDefined(typeof(GameState), answer))
                    return answer;
    
            Console.WriteLine("\nChoose one of the options!");
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
            Console.ReadKey();
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
            Console.WriteLine($"{GameAssets.PlayerWeapons[playerWeapon]}  \n  {GameAssets.AiWeapons[enemyWeapon]}");
            Thread.Sleep(2000);
        }

        private static void EndGame()
        {
            Console.WriteLine($"Goodbye, {_name}!");
            Environment.Exit(0);
        }

        private static void ShowGameRules()
        {
            Console.WriteLine(GameAssets.RulesText);
            Console.ReadKey();
        }

        private static Weapons ChooseWeapon()
        {
            Console.WriteLine("\nChoose your weapon!" +
                              $"\n{(int)Weapons.Paper}) Paper" +
                              $"\n{(int)Weapons.Rock}) Rock" +
                              $"\n{(int)Weapons.Scissors}) Scissors");

            if (int.TryParse(Console.ReadLine(), out int weapon))
                if (Enum.IsDefined(typeof(Weapons), weapon))
                    return (Weapons)weapon;
            
            Console.WriteLine("\nChoose one of the options!");
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