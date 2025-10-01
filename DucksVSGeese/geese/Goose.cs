namespace DucksVSGeese
{
    public abstract class Goose : Combatant
    {
        protected static readonly Random RNG = new();
        private static readonly string[] GooseNames = ["Goosetaf", "Agoosetus", "Agoosetine", "Goose", "Geese", "Gayce", "Gus", "Giise", "Gysse", "Asparagoose", "Goosewich", "Gooselin", "Gooserett", "Bildegoose", "Honkrietta", "Honking", "Bread?", "Fishkiller", "Duckslayer", "HONK", "Honk", "Loose", "Choose", "Moose", "Noose", "Joose", "Gose", "Oose", "Esoog", "Canada"];
        protected double regenAmount;
        protected Goose(string combatClass, string name, int maxHP, bool attackAllies, double regenAmount) : base(combatClass, name, maxHP, attackAllies)
        {
            this.regenAmount = regenAmount;
        }

        public double RegenAmount
        {
            get { return regenAmount; }
        }

        /**<summary>
        * Regenerate some HP at the start of a round.
        </summary>*/
        public void Regenerate()
        {
            Heal((int)(MaxHP * regenAmount));
        }

        public static string GetRandomName()
        {
            return GooseNames[RNG.Next(GooseNames.Length)];
        }

        public override void EndTurn()
        {
            Regenerate();
            DecrementCurse();
        }


        public override void LevelUp()
        {
            Level++;
            // update max hp and stuff
            MaxHP += MaxHP;
            HealAll();
        }

        public override void SetLevel(int level)
        {
            Level = level;
            // update max hp and stuff
            MaxHP *= level;
            HealAll();
        }
    }
}