namespace DucksVSGeese.Ducks
{
    public abstract class Duck : Combatant
    {
        private readonly static string[] DuckNames = ["Duck", "Uck", "Muck", "Puck", "Plumage", "Feathers", "Quack", "Bread", "Kcud", "Duc", "Du", "*nibble*", "QUACK", "Duckley", "Duckbert", "Duckline", "Duckling", "Duckduck", "Ducduc", "Ducklin", "Truck", "Uckd", "Uck", "Cud", "Kud", "Duk", "Plume", "Crest", "Mallard", "Goosekiller"];
        public static readonly Random RNG = new Random();
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
            // update max hp and stuff
            MaxHP += Convert.ToInt32(MaxHP * .5);
            HealAll();
        }

        public override void SetLevel(int level)
        {
            int diff = level - Level;
            Level = level;
            // update max hp and stuff
            // this will probably only work at level 1. ...
            MaxHP += diff * Convert.ToInt32(MaxHP * .5);
            HealAll();
        }
    }
}