using Rock_Scissors_Paper.Enums;

namespace Rock_Scissors_Paper;

public static class GameAssets
{
   public const string Logo = """
                               ╦═╗┌─┐┌─┐┬┌─  ╔═╗┌─┐┬┌─┐┌─┐┌─┐┬─┐┌─┐  ╔═╗┌─┐┌─┐┌─┐┬─┐
                              ╠╦╝│ ││  ├┴┐  ╚═╗│  │└─┐└─┐│ │├┬┘└─┐  ╠═╝├─┤├─┘├┤ ├┬┘
                              ╩╚═└─┘└─┘┴ ┴  ╚═╝└─┘┴└─┘└─┘└─┘┴└─└─┘  ╩  ┴ ┴┴  └─┘┴└─ 
                              """;

   public const string RulesText = """
                                      ____________________________________________
                                    / \                                            \.
                                    |   |                                           |.
                                     \_ |                                           |.
                                       |   Paper beats rock, but loses to scissors  |.
                                       |                                            |.
                                       |   Scissors beat paper, but lose to rock    |.
                                       |                                            |.
                                       |   Rock beats scissors, but loses to paper  |.
                                       |                                            |.
                                       |                                            |.
                                       |   _________________________________________|___
                                       |  /                                            /.
                                       \_/____________________________________________/.
                                   """;

   public const string WelcomeText = "Welcome to the game, hero! Defeat Artificial Intelligence. Use enter to continue.";
   
   public const string WinText = "\n\n[Triumphant music erupts in the hall as the hero enters the arena, having won the final battle. The defeated and bewildered Artificial Intelligence stands before him.]" +
                                 "\n Hero: Finally, my goal is achieved. You were a formidable opponent, Artificial Intelligence, but true strength always comes from courage and self-belief." +
                                 "\n\n[The Artificial Intelligence, showing weakness, responds with a bow.]" +
                                 "\n Artificial Intelligence: You have defeated me, hero. Your strength and ingenuity were unbeatable. Forgive me for my villainous deeds." +
                                 "\n\n[The hero approaches the Artificial Intelligence, extends a hand in reconciliation, and the Artificial Intelligence accepts it, performing a gesture of reconciliation.]" +
                                 "\n Hero: Let us try to put the past behind us. Our shared goal now is to build the future together." +
                                 "\n\n[The hall erupts in applause, marking the triumph of the hero and the birth of a new era of peace and reconciliation.]";

   public const string FailText = "\n\n[Anxious music fills the hall as the hero lowers their head, defeated after the final battle. The Artificial Intelligence looks upon them with pity.]" +
                                  "\n Artificial Intelligence: Hero, it was a worthy contest. You proved to be brave, but today is not your day. Rest, and we will meet again." +
                                  "\n\n[The hero bows their head, acknowledging their defeat, and walks away from the arena, leaving behind the Artificial Intelligence, who won the battle but lost the human they fought against.]" +
                                  "\n Hero: Nothing remains but hope for a better future. Perhaps this defeat is only our first great trial." +
                                  "\n\n[The Artificial Intelligence watches the hero, feeling a certain sense of doubt and sympathy.]" +
                                  "\n Artificial Intelligence: Do not be disheartened, hero. Every defeat is an opportunity to learn and grow stronger. I will await your return." +
                                  "\n\n[The hall fills with silence, signaling the victory of the Artificial Intelligence, but also the hope for the future and the resilience of the hero's spirit.]";
 
    public static readonly string[] EncouragementMessages = new[] {
     "\nDon't be discouraged, hero! Even the strongest heroes face defeats. You will undoubtedly grow and overcome this challenge!\n",
     "\nYour courage is beyond doubt, hero. Remember that every defeat is just inspiration for future victories!\n",
     "\nDon't let defeat break your spirit, hero. All great deeds began with mistakes. You can overcome this!\n"
    };

    public static readonly string[] PraiseMessages = new[] {
     "\nYour victory is a shining example of strength and courage. Keep on your path, hero, you are capable of great things!\n",
     "\nGlory to you, hero! Your victory is not only yours but also the victory of all our people. You are a true hero!\n",
     "\nYour victory is a bright testament to your courage and determination. Keep fighting, hero, the world needs your strength!\n"
    };

    public static readonly Dictionary<Weapons, string> PlayerWeapons = new()
    {
     { Weapons.Paper,
      """
                                 _______
                            ---'    ________)
          Player                       ______)
          Weapon                      _______)
                                     _______)
                            ---.__________)
      """ },
     { Weapons.Rock,
      """
                                 _______
                             ---'   ____)
          Player                   (_____)
          Weapon                   (_____)
                                   (____)
                             ---.__(___)
      """ },
     { Weapons.Scissors,
      """
                                _______
                            ---'   ____)
          Player                      ______)
          Weapon                   __________)
                                  (____)
                            ---.__(___)
      """ }
    };
    
    public static readonly Dictionary<Weapons, string> AiWeapons = new()
    {
     { Weapons.Paper,
      """
                                  _______
                             (________     '---
           Ai               (______
          Weapon            (_______
                             (_______
                               (_________.---
      """ },
     { Weapons.Rock,
      """
                                 _______
                                (____   '---
           Ai                  (_____)
          Weapon               (_____)
                                (____)
                                 (___)__.---
      """ },
     { Weapons.Scissors,
      """
                                      _______
                                     (____   '---
            Ai                  (______
           Weapon              (__________
                                     (____)
                                      (___)__.---
      """ }
    };
}