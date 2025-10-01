namespace DucksVSGeese.ducks
{
    /// <summary>
    /// Class for an Accursed Duck, a basic cursed attacker.
    /// </summary>
    public class DuckCursed : Duck
    {
        private const int MaximumHP = 125;
        private const string CombatantClass = "Accursed Duck";
        private const bool AttacksAllies = false;
        public DuckCursed(string name) : base(CombatantClass, name, MaximumHP, AttacksAllies)
        {
            // idk maybe do some duck stuff here
        }
        public DuckCursed() : this(Duck.GetRandomName()) { }

        public static new string ClassName
        {
            get { return CombatantClass; }
        }

        /// <summary>
        /// Accursed Ducks attack twice for 0-50 base damage per hit. Their attacks deal Cursed damage.
        /// </summary>
        /// <returns>An instance of the class Attack.</returns>
        public override Attack Attack()
        {
            int[] hits = [0, 0];
            // hits twice for between 0 and 50 points of damage
            for (int i = 0; i < hits.Length; i++)
            {
                hits[i] = RNG.Next(16);
                // hits are weighed towards 0-15 points
                if (RNG.Next(3) == 1)
                {
                    // 1 in 3 chance to add 1 - 35 points of damage
                    hits[i] += RNG.Next(1, 36);
                }
            }
            return new Attack("Unholy Quack", ScaleHits(hits), Attribute.Cursed);
        }

        /// <summary>
        /// Lowers the current HP of this combatant depending on the given attack.
        /// Accursed Ducks take more damage from elemental and holy attacks and less damage from every other attack.
        /// </summary>
        /// <param name="attack">The attack the combatant is taking damage from.</param>
        /// <returns>The total amount of damage the attack will deal.</returns>
        public override int TakeDamage(Attack attack)
        {
            double modifier;
            Attribute attribute = attack.Attribute;
            if (attribute == Attribute.Cursed) modifier = 0.5; // greatly resist cursed attacks
            else if (attribute == Attribute.Holy) modifier = 1.5; // take a lot of extra damage from holy attacks
            else if (attribute == Attribute.Elemental) modifier = 1.25; // everyone takes more damage from elemental attacks
            else modifier = .75; // take less damage from every other attack

            return GetHit(attack.Hits, modifier);
        }
    }
}