namespace DucksVSGeese.Ducks
{
    public abstract class Duck : Combatant
    {
        private readonly static string[] DuckNames = ["Duck", "Uck", "Muck", "Puck", "Plumage", "Feathers", "Quack", "Bread", "Kcud", "Duc", "Du", "*nibble*", "QUACK", "Duckley", "Duckbert", "Duckline", "Duckling", "Duckduck", "Ducduc", "Ducklin", "Truck", "Uckd", "Uck", "Cud", "Kud", "Duk", "Plume", "Crest", "Mallard", "Goosekiller"];
        public static Random RNG = new Random();
        private const string CombatantClass = "Duck";
        // should ducks have experience or something? so they can level up?
        // and maybe geese give out experience?
        protected Duck(string combatClass, string name, int maxHP, bool attackAllies) : base(combatClass, name, maxHP, attackAllies)
        {
            // idk maybe do some duck stuff here
        }
        public static new string ClassName
        {
            get { return CombatantClass; }
        }

        public static string GetRandomName()
        {
            return DuckNames[RNG.Next(DuckNames.Length)];
        }

        public override void EndTurn()
        {
            DecrementCurse();
        }

        public override void LevelUp()
        {
            Level++;
            // each level up increases MaxHP by 1/2 the current MaxHP
            MaxHP += Convert.ToInt32(MaxHP * .5);
            HealAll();
        }

        public override void SetLevel(int newLevel)
        {
            // get the difference between these levels
            int diff = newLevel - Level;
            

            // change maxHP depending on this
            // what if i just do what happens in a level up
            int maxHPChange = MaxHP;

            // if this would be a level up
            if (newLevel > Level)
            {
                for (int i = 0; i < diff; i++) MaxHP += Convert.ToInt32(MaxHP * .5);
            }
            else // if level down
            {
                for (int i = 0; i < diff; i++) maxHPChange += MaxHP + Convert.ToInt32(MaxHP * .5);
            }

            // = diff * Convert.ToInt32(MaxHP * .5);

            // if this would be a level down

            Level = newLevel;

            // alter maxHP
            MaxHP += maxHPChange;
            HealAll();
            
        }
    }
}