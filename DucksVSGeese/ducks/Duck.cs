namespace DucksVSGeese
{
    public abstract class Duck : Combatant
    {
        private readonly static string[] DuckNames = ["Duck", "Uck", "Muck", "Puck", "Plumage", "Feathers", "Quack", "Bread", "Kcud", "Duc", "Du", "*nibble*", "QUACK", "Duckley", "Duckbert", "Duckline", "Duckling", "Duckduck", "Ducduc", "Ducklin", "Truck"];
        public static readonly Random RNG = new Random();
        // should ducks have experience or something? so they can level up?
        // and maybe geese give out experience?
        protected Duck(string combatClass, string name, int maxHP, bool attackAllies) : base(combatClass, name, maxHP, attackAllies)
        {
            // idk maybe do some duck stuff here
        }

        public static string GetRandomName()
        {
            return DuckNames[RNG.Next(DuckNames.Length)];
        }

        public override void EndTurn()
        {
            DecrementCurse();
        }

    }
}