namespace DucksVSGeese
{
    /// <summary>
    /// Class for a Duck Rogue, an alternate poison attacker.
    /// </summary>
    public class DuckThiefB : Duck
    {
        private const int MaximumHP = 105;
        public const string CombatantClass = "Duck Rogue";
        private const bool AttacksAllies = false;
        public DuckThiefB(string name) : base(CombatantClass, name, MaximumHP, AttacksAllies)
        {
            // idk maybe do some duck stuff here
        }
        public DuckThiefB() : this(Duck.GetRandomName()) { }

        /// <summary>
        /// Duck Rogues attack 6 times for 0, 1, 10, or 20 base damage per hit. Their attacks deal Poison damage.
        /// </summary>
        /// <returns>An instance of the class Attack.</returns>
        public override Attack Attack()
        {
            // hits 1-2 times
            int[] hits = [0, 0, 0, 0, 0, 0];
            for (int i = 0; i < hits.Length; i++)
            {
                int gamble = RNG.Next(100);
                
                // 5% chance for 20 damage
                if (gamble >= 95) hits[i] = 20;
                // 20% chance for 10 damage
                else if (gamble >= 75) hits[i] = 10;
                // 40% chance for 5 damage
                else if (gamble >= 35) hits[i] = 5;
                // 25% chance for 1 damage
                else if (gamble >= 10) hits[i] = 1;
                // 10% chance for 0 damage
            }
            return new Attack("Nab", ScaleHits(hits), Attribute.Poison);
        }

        /// <summary>
        /// Lowers the current HP of this combatant depending on the given attack.
        /// Duck Rogues take more damage from elemental attacks and less damage from poison attacks.
        /// </summary>
        /// <param name="attack">The attack the combatant is taking damage from.</param>
        /// <returns>The total amount of damage the attack will deal.</returns>
        public override int TakeDamage(Attack attack)
        {
            double modifier = 1.0;
            Attribute attribute = attack.Attribute;
            if (attribute == Attribute.Poison) modifier = .75; // take less damage from poison attacks
            else if (attribute == Attribute.Elemental) modifier = 1.25; // everyone takes more damage from elemental attacks

            return GetHit(attack.Hits, modifier);
        }
    }
}