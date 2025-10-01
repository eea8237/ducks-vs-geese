using System.Diagnostics;
using System.Reflection;
using DucksVSGeese.Geese;
using DucksVSGeese.Ducks;
using DucksVSGeese.Attributes;

namespace DucksVSGeese
{
    public abstract class Arena
    {
        private static readonly HashSet<string> FighterAliases = ["duckfighter", "fighter", "fi", "0"];
        private static readonly HashSet<string> MageAliases = ["duckmage", "mage", "m", "ma", "1"];
        private static readonly HashSet<string> ThiefAliases = ["duckthief", "thief", "t", "th", "2"];
        private static readonly HashSet<string> ClericAliases = ["duckcleric", "cleric", "c", "cl", "3"];
        private static readonly HashSet<string> AccursedAliases = ["duckaccursed", "accursedduck", "cursed", "cu", "a", "ac", "accursed", "4"];
        private static readonly HashSet<string> EtherealAliases = ["duckethereal", "etherealduck", "ethereal", "et", "e", "5"];

        private static readonly HashSet<string> SentryAliases = ["ducksentry", "sentry", "s", "se", "6"];
        private static readonly HashSet<string> WardAliases = ["duckward", "ward", "w", "wa", "7"];
        private static readonly HashSet<string> RogueAliases = ["duckrogue", "rogue", "r", "ro", "8"];
        private static readonly HashSet<string> PhysicianAliases = ["duckphysician", "physician", "ph", "9"];
        private static readonly HashSet<string> ForbiddenAliases = ["duckforbidden", "forbiddenduck", "forbidden", "fo", "10"];
        private static readonly HashSet<string> ProteanAliases = ["duckprotean", "proteanduck", "protean", "pr", "11"];

        

        private static readonly Random RNG = new Random();
        private const int DuckPartySize = 4;
        private const int GoosePartySize = 6;
        private const int GooseTypes = 12;
        private const int NumBattles = 5;
        static void Test()
        {
            try
            {
                Attack test = new Attack("Test Attack", [1, 2, 3, 4], DAttribute.Elemental);
                for (int i = 0; i < test.Hits.Length; i++)
                {
                    Console.Write($"{test.Hits[i]} ");
                }

                Console.WriteLine("\n" + test);

                Combatant fighterDuck = new DuckFighter();
                Combatant mageDuck = new DuckMage();
                Combatant thiefDuck = new DuckThief();
                Combatant clericDuck = new DuckCleric();
                Combatant cursedDuck = new AccursedDuck();
                Combatant elementalDuck = new EtherealDuck();
                
                Combatant fighterBDuck = new DuckSentry();
                Combatant mageBDuck = new DuckWard();
                Combatant thiefBDuck = new DuckRogue();
                Combatant clericBDuck = new DuckPhysician();
                Combatant cursedBDuck = new ForbiddenDuck();
                Combatant elementalBDuck = new ProteanDuck();

                Combatant fighterGoose = new GooseWarrior("Goose");
                Combatant mageGoose = new GooseWitch("Oose");
                Combatant thiefGoose = new GooseVandal("Gose");
                Combatant clericGoose = new GoosePriest("Geese");
                Combatant cursedGoose = new UnholyGoose("Esoog");

                // fighter.TakeDamage(mage.Attack());
                // mage.TakeDamage(fighter.Attack());
                // thief.TakeDamage(thief.Attack());
                // cleric.TakeDamage(cursed.Attack());
                // cursed.TakeDamage(thief.Attack());

                Combatant[] Combatants = [fighterDuck, fighterBDuck, mageDuck, mageBDuck, thiefDuck, thiefBDuck, clericDuck, clericBDuck, cursedDuck, cursedBDuck, elementalDuck, elementalBDuck, fighterGoose, mageGoose, thiefGoose, clericGoose, cursedGoose];
                // test each Combatant with each other Combatant
                foreach (Combatant combatant in Combatants)
                {
                    Combatant current = combatant;
                    foreach (Combatant c in Combatants)
                    {
                        Console.WriteLine(combatant);
                        Attack attack = c.Attack();
                        int damage = combatant.TakeDamage(attack);
                        Console.WriteLine($"Attack: {attack}");
                        if (damage >= 0) Console.WriteLine($"{combatant.GetTitle()} is attacked by {c.GetTitle()} for {damage} damage!\n{combatant}");
                        else Console.WriteLine($"{combatant.GetTitle()} is healed by {c.GetTitle()} for {damage*-1} health!\n{combatant}");
                        Console.WriteLine();
                        combatant.Heal(10000);
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
                            // A ducks
                            if (FighterAliases.Contains(s)) ducks.Add(new DuckFighter());
                            else if (MageAliases.Contains(s)) ducks.Add(new DuckMage());
                            else if (ThiefAliases.Contains(s)) ducks.Add(new DuckThief());
                            else if (ClericAliases.Contains(s)) ducks.Add(new DuckCleric());
                            else if (AccursedAliases.Contains(s)) ducks.Add(new AccursedDuck());
                            else if (EtherealAliases.Contains(s)) ducks.Add(new EtherealDuck());

                            // B ducks
                            else if (SentryAliases.Contains(s)) ducks.Add(new DuckSentry());
                            else if (WardAliases.Contains(s)) ducks.Add(new DuckWard());
                            else if (RogueAliases.Contains(s)) ducks.Add(new DuckRogue());
                            else if (PhysicianAliases.Contains(s)) ducks.Add(new DuckPhysician());
                            else if (ForbiddenAliases.Contains(s)) ducks.Add(new ForbiddenDuck());
                            else if (ProteanAliases.Contains(s)) ducks.Add(new ProteanDuck());

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
                Console.WriteLine($"\n\nBattle {i}\n");
                List<Goose> geese = GenerateGeese();
                foreach (Goose goose in geese) goose.SetLevel(i);
                foreach (Combatant duck in ducks) duck.SetLevel(i);

                int round = 1;
                while (ducks.Count > 0 && geese.Count > 0)
                {
                    Console.WriteLine($"\nROUND {round}!");
                    PrintParties(ducks, geese, felledDucks, felledGeese);

                    // ducks go first i guess
                    Console.WriteLine("\nDucks Turn:");
                    if (ducks.Count > 0 && geese.Count > 0)
                    {
                        Combatant? felled = Fight(ducks, geese, skip);
                        Console.WriteLine();
                        if (felled != null)
                        {
                            if (felled is Goose) felledGeese.Add(felled);
                            if (felled is Duck) felledDucks.Add(felled);
                        } 
                        foreach (Duck duck in ducks) duck.EndTurn();
                    }

                    Console.WriteLine("Geese Turn:");
                    if (ducks.Count > 0 && geese.Count > 0)
                    {
                        // geese go second
                        Combatant? felled = Fight(geese, ducks, skip);
                        Console.WriteLine();
                        if (felled != null)
                        {
                            if (felled is Goose) felledGeese.Add(felled);
                            if (felled is Duck) felledDucks.Add(felled);
                        } 
                        foreach (Goose goose in geese) goose.EndTurn();
                    }
                    round++;

                    // as a temporary fix to the infinite loop issues just cut the battle short if it goes on for too long
                    if (round > 100)
                    {
                        Console.WriteLine("\nBoth sides are exhausted...");
                        Console.WriteLine("The geese retreat back to their pond.");
                        break;
                    }
                }

                Console.WriteLine($"Battle {i} Over!\n");
                PrintParties(ducks, geese, felledDucks, felledGeese);
                // reset the felled geese list
                // or just print it when everything's done with. maybe keep track of the round they were felled in?
                felledGeese = new List<Combatant>();
                // perhaps do this with the ducks too if i want to complicate this a little more
                // like give the option to add new ducks inbetween battles
                // probably not for free though

                if (ducks.Count == 0) // if ducks go down it's game over
                {
                    Console.WriteLine("\nGame Over.\nThe ducks have been vanquished...");
                    break;
                }
                else // if geese go down good for you
                {
                    Console.WriteLine("\nThe geese have been conquered!");
                    if (i < NumBattles) Console.WriteLine("Next battle!!");
                }
            }

            // print a message if the player didn't lose
            if (ducks.Count > 0)
            {
                Console.WriteLine("\nYou won! The evil geese have been defeated!");
            }
        }

        static void PrintParties<T1, T2, T3, T4>(List<T1> ducks, List<T2> geese, List<T3>? felledDucks = null, List<T4>? felledGeese = null)
           where T1 : Combatant
           where T2 : Combatant
           where T3 : Combatant
           where T4 : Combatant
        {
            Console.WriteLine($"Ducks: [{string.Join(", ", ducks)}]");
            Console.WriteLine($"Geese: [{string.Join(", ", geese)}]");
            if (felledDucks != null) Console.WriteLine($"Felled Ducks: [{string.Join(", ", felledDucks)}]");
            if (felledGeese != null) Console.WriteLine($"Felled Geese: [{string.Join(", ", felledGeese)}]");
        }

        static Combatant? Fight<T1, T2>(List<T1> attacking, List<T2> defending, bool skip = false) where T1 : Combatant where T2 : Combatant
        {
            // each combattant in party 1 attacks a random combattant in party 2

            foreach (Combatant c in attacking)
            {
                
                int targetIndex;
                Combatant target;
                // what if i made these guys a little smarter
                // like they attack / heal based on type effectiveness and stuff if they can
                if (!c.AttackAllies) // target should be a random member of the defending party
                {
                    targetIndex = RNG.Next(defending.Count);
                    target = defending[targetIndex];
                }
                else // or attacking party if the attacker is currently aiming for its allies
                {
                    targetIndex = RNG.Next(attacking.Count);
                    target = attacking[targetIndex];
                }
                
                Console.WriteLine($"\n{c} aims for {target}!");

                // attack target
                Attack attack = c.Attack();
                int damage = target.TakeDamage(attack);
                Console.WriteLine($"Attack: {attack}");
                if (damage >= 0) Console.WriteLine($"{target.GetTitle()} is attacked by {c.GetTitle()} for {damage} damage!\n{target}");
                else Console.WriteLine($"{target.GetTitle()} is healed by {c.GetTitle()} for {damage*-1} health!\n{target}");
                // if this attack is a curse and the target hasn't been cursed, curse the target
                if (attack.IsCurse && target.CursedTimer == 0) target.Curse();
                // if attack knocked the target out, remove the target from the defending party
                if (!target.IsConscious())
                {
                    Console.WriteLine($"{target.GetTitle()} was felled by {c.GetTitle()}...");
                    // could keep track of the felled combatants
                    Combatant felled = target;
                    if (c.AttackAllies) attacking.RemoveAt(targetIndex);
                    else defending.RemoveAt(targetIndex);
                    return felled;

                }
                if (!skip) Console.ReadLine();
            }
            return null;
        }
        private static List<Goose> GenerateGeese()
        {
            List<Goose> geese = new List<Goose>();
            // just choose 6 random geese
            while (geese.Count < GoosePartySize)
            {
                int selection = RNG.Next(GooseTypes);
                switch (selection)
                {
                    case 0:
                        geese.Add(new GooseWarrior());
                        break;
                    case 1:
                        geese.Add(new GooseWitch());
                        break;
                    case 2:
                        geese.Add(new GooseVandal());
                        break;
                    case 3:
                        // i also feel like there will be an issue if there's a full party of clerics on both sides, so check the goose party to see if everyone else is already a cleric
                        // if (geese.Count == GoosePartySize - 1) // only do this check if we're filling out the last space
                        // {
                        //     int clerics = 0;
                        //     foreach (Goose goose in geese) if (goose.CombatClass == GoosePriest.ClassName) clerics++;
                        //     // if the party isn't entirely clerics you can add another cleric
                        //     if (clerics < GoosePartySize - 1) geese.Add(new GoosePriest());
                        // } else geese.Add(new GoosePriest());
                        geese.Add(new GoosePriest()); // whatever i'll just deal with it later
                        // similar issue occurs if you've got a bunch of accursed ducks
                        break;
                    case 4:
                        geese.Add(new UnholyGoose());
                        break;
                    case 5:
                        geese.Add(new EphemeralGoose());
                        break;
                    case 6:
                        geese.Add(new GooseSentinel());
                        break;
                    case 7:
                        geese.Add(new GooseGuardian());
                        break;
                    case 8:
                        geese.Add(new GooseGambler());
                        break;
                    case 9:
                        geese.Add(new GooseMedic());
                        break;
                    case 10:
                        geese.Add(new ImprisonedGoose());
                        break;
                    case 11:
                        geese.Add(new UniversalGoose());
                        break;
                }
            }

            return geese;
        }
        static void Main(string[] args)
        {
            // Test();
            Battle();
            // Console.WriteLine(GoosePriest.ClassName);
        }

    }
}