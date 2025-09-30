using System.Diagnostics;
using System.Reflection;

namespace DucksVSGeese
{
    public abstract class Arena
    {
        private static readonly HashSet<string> FighterAliases = ["duck fighter", "fighter", "f", "0"];
        private static readonly HashSet<string> MageAliases = ["duck mage", "mage", "m", "1"];
        private static readonly HashSet<string> ThiefAliases = ["duck thief", "thief", "t", "2"];
        private static readonly HashSet<string> ClericAliases = ["duck cleric", "cleric", "cl", "3"];
        private static readonly HashSet<string> CursedAliases = ["duck cursed", "cursed", "cu", "a", "acu", "ac", "accursed duck", "accursed", "4"];

        private static readonly Random RNG = new Random();
        private const int DuckPartySize = 4;
        private const int GoosePartySize = 6;
        private const int GooseTypes = 5;
        private const int NumBattles = 5;
        static void Test()
        {
            try
            {
                Attack test = new Attack("Test Attack", [1, 2, 3, 4], Attribute.Elemental);
                for (int i = 0; i < test.Hits.Length; i++)
                {
                    Console.Write($"{test.Hits[i]} ");
                }

                Console.WriteLine("\n" + test);

                Combatant fighterDuck = new DuckFighter("Duc");
                Combatant mageDuck = new DuckMage("Uck");
                Combatant thiefDuck = new DuckThief("Dck");
                Combatant clericDuck = new DuckCleric("Uckd");
                Combatant cursedDuck = new DuckCursed("Kcud");

                Combatant fighterGoose = new GooseFighter("Goose");
                Combatant mageGoose = new GooseMage("Oose");
                Combatant thiefGoose = new GooseThief("Gose");
                Combatant clericGoose = new GooseCleric("Geese");
                Combatant cursedGoose = new GooseCursed("Esoog");

                // fighter.TakeDamage(mage.Attack());
                // mage.TakeDamage(fighter.Attack());
                // thief.TakeDamage(thief.Attack());
                // cleric.TakeDamage(cursed.Attack());
                // cursed.TakeDamage(thief.Attack());

                Combatant[] Combatants = [fighterDuck, mageDuck, thiefDuck, clericDuck, cursedDuck, fighterGoose, mageGoose, thiefGoose, clericGoose, cursedGoose];
                // test each Combatant with each other Combatant
                foreach (Combatant Combatant in Combatants)
                {
                    Combatant current = Combatant;
                    foreach (Combatant c in Combatants)
                    {
                        Console.WriteLine(Combatant);
                        Attack attack = c.Attack();
                        int damage = Combatant.TakeDamage(attack);
                        Console.WriteLine($"Attack: {attack}");
                        Console.WriteLine($"{Combatant} is attacked by {c.GetTitle()} for {damage} damage!");
                        Console.WriteLine();
                        Combatant.Heal(10000);
                    }
                    Console.WriteLine();
                }
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Whoops.");
            }
        }

        static void Battle(bool skip = false)
        {
            // ask the user for a team of four ducks
            // example input: fighter fighter fighter fighter
            // ... maybe offer quick creation and slow creation?
            // maybe pull names from a name pool
            // could also have the user enter numbers corresponding to the
            List<Duck> ducks = new List<Duck>();
            List<Combatant> felledDucks = new List<Combatant>();
            List<Combatant> felledGeese = new List<Combatant>();
            while (true)
            {
                Console.Write("Enter your team of 4 ducks: ");
                string? answer = Console.ReadLine();
                if (answer == null)
                {
                    Console.WriteLine("Invalid answer.");
                }
                else if (answer == "quit")
                {
                    break;
                }
                else
                {
                    string[] answerTokens = answer.Split(" ");
                    if (answerTokens.Length != DuckPartySize)
                    {
                        Console.WriteLine("Must have a party of four ducks.");
                        continue;
                    }
                    else
                    {
                        foreach (string s in answerTokens)
                        {
                            // add duck based on what alias the string maps into
                            List<HashSet<string>> ducktionary = [FighterAliases, MageAliases, ThiefAliases, ClericAliases, CursedAliases];
                            if (FighterAliases.Contains(s)) ducks.Add(new DuckFighter());
                            else if (MageAliases.Contains(s)) ducks.Add(new DuckMage());
                            else if (ThiefAliases.Contains(s)) ducks.Add(new DuckThief());
                            else if (ClericAliases.Contains(s)) ducks.Add(new DuckCleric());
                            else if (CursedAliases.Contains(s)) ducks.Add(new DuckCursed());
                            else
                            {
                                Console.WriteLine("Invalid duck.");
                                ducks = new List<Duck>();
                            }
                        }
                        Console.WriteLine(string.Join("\n", ducks));
                        break;
                    }

                }
            }
            if (!skip)
            {
                Console.Write("Skip input? [Y/N]: ");
                string? answer = Console.ReadLine();
                if (answer != null) answer = answer.ToLower();
                if (answer != null && answer.ToLower() == "y") skip = true;
            }

            // now do the fighting
            // generate 10 random teams of geese and have them fight the duck party
            for (int i = 1; i <= NumBattles; i++)
            {
                Console.WriteLine($"Battle {i}\n");
                List<Goose> geese = GenerateGeese();
                int round = 1;
                while (ducks.Count > 0 && geese.Count > 0)
                {
                    Console.WriteLine($"ROUND {round}!");
                    PrintParties(ducks, geese, felledDucks, felledGeese);

                    // ducks go first i guess
                    Console.WriteLine("\nDucks Turn:");
                    if (ducks.Count > 0 && geese.Count > 0)
                    {
                        Combatant? felled = Fight(ducks, geese, skip);
                        Console.WriteLine();
                        if (felled != null) felledGeese.Add(felled);
                    }

                    Console.WriteLine("\nGeese Turn:");
                    if (ducks.Count > 0 && geese.Count > 0)
                    {
                        // geese go second
                        Combatant? felled = Fight(geese, ducks, skip);
                        Console.WriteLine();
                        if (felled != null) felledDucks.Add(felled);
                    }
                    round++;
                }
                Console.WriteLine("Battle Over!");
                PrintParties(ducks, geese, felledDucks, felledGeese);
                // reset the felled geese list
                felledGeese = new List<Combatant>();


                if (ducks.Count == 0) // if ducks go down it's game over
                {
                    Console.WriteLine("Game Over.\nThe ducks have been vanquished...");
                    break;
                }
                else // if geese go down good for you
                {
                    Console.WriteLine("The geese have been conquered!");
                    if (i < NumBattles) Console.WriteLine("Next battle!!");
                    // for now let's just heal up the duck party
                    foreach (Combatant duck in ducks) duck.HealAll();
                }
            }

            // print a message if the player didn't lose
            if (ducks.Count > 0)
            {
                Console.WriteLine("\nYou won! The evil geese have been defeated!");
            }
        }

        static void PrintParties<T1, T2, T3, T4>(List<T1> ducks, List<T2> geese, List<T3>? felledDucks=null, List<T4>? felledGeese=null)
           where T1 : Combatant
           where T2 : Combatant
           where T3 : Combatant
           where T4 : Combatant
        {
            Console.WriteLine($"Ducks: [{string.Join(", ", ducks)}]");
            Console.WriteLine($"Geese: [{string.Join(", ", geese)}]\n");
            if (felledDucks != null) Console.WriteLine($"Felled Ducks: [{string.Join(", ", felledDucks)}]");
            if (felledGeese != null) Console.WriteLine($"Felled Geese: [{string.Join(", ", felledGeese)}]");
        }

        static Combatant? Fight<T1, T2>(List<T1> attacking, List<T2> defending, bool skip = false) where T1 : Combatant where T2 : Combatant
        {
            // what does a fight even look like
            // one combattant strikes a random combattant in the opposing party
            // each combattant in party 1 attacks a random combattant in party 2

            foreach (Combatant c in attacking)
            {
                // target should be a random member of the defending party
                int targetIndex = RNG.Next(defending.Count);
                Combatant target = defending[targetIndex];
                Console.WriteLine($"\n{c} aims for {target}!");

                // attack target
                Attack attack = c.Attack();
                int damage = target.TakeDamage(attack);
                Console.WriteLine($"Attack: {attack}");
                Console.WriteLine($"{target.GetTitle()} is attacked by {c.GetTitle()} for {damage} damage!\n{target.GetTitle()} HP: {target.GetHPString()}");

                // if attack knocked the target out, remove the target from the defending party
                if (!target.IsConscious())
                {
                    Console.WriteLine($"{target.GetTitle()} was felled by {c.GetTitle()}...\n");
                    // could keep track of the felled combatants
                    Combatant felled = target;
                    defending.RemoveAt(targetIndex);
                    return felled;

                }
                if (!skip) Console.ReadLine();
            }
            return null;
        }

        /// <summary>
        /// Helper method to determine if an entire party has been rendered unconscious.
        /// </summary>
        /// <param name="party">Party whose status should be checked</param>
        /// <returns> true if whole party is unconscious, false otherwise </returns>
        static bool PartyDown<T>(List<T> party) where T : Combatant
        {
            return party.Count == 0;
        }

        private static List<Goose> GenerateGeese()
        {
            List<Goose> geese = new List<Goose>();
            // just choose 6 random geese
            for (int i = 0; i < GoosePartySize; i++)
            {
                int selection = RNG.Next(GooseTypes);
                switch (selection)
                {
                    case 0:
                        geese.Add(new GooseFighter());
                        break;
                    case 1:
                        geese.Add(new GooseMage());
                        break;
                    case 2:
                        geese.Add(new GooseThief());
                        break;
                    case 3:
                        geese.Add(new GooseCleric());
                        break;
                    case 4:
                        geese.Add(new GooseCursed());
                        break;
                }
            }

            return geese;
        }
        static void Main(string[] args)
        {
            // Test();
            Battle();
        }

    }
}