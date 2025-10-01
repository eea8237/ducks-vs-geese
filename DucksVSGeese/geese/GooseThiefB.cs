namespace DucksVSGeese.geese
{
    /// <summary>
    /// Class for a Goose Gambler, an alternate poison attacker.
    /// </summary>
    public class GooseThiefB : Goose
    {
        private const int MaximumHP = 55;
        private const string CombatantClass = "Goose Gambler";
        private const bool AttacksAllies = false;
        private const double Regeneration = .1;
        public GooseThiefB(string name) : base(CombatantClass, name, MaximumHP, AttacksAllies, Regeneration)
        {
            // idk maybe do some goose stuff here
        }
        public GooseThiefB() : this(Goose.GetRandomName()) { }

        public static new string ClassName
        {
            get { return CombatantClass; }
        }

        /// <summary>
        /// Goose Gamblers attack 5 times for 0, 1, 5, 10, or 20 base damage per hit. Their attacks deal Poison damage.
        /// </summary>
        /// <returns>An instance of the class Attack.</returns>
        public override Attack Attack()
        {
            int[] hits = [0, 0, 0, 0, 0];
            for (int i = 0; i < hits.Length; i++)
            {
                int gamble = RNG.Next(100);

                // 5% chance for 20 damage
                if (gamble >= 95) hits[i] = 20;
                // 10% chance for 10 damage
                else if (gamble >= 85) hits[i] = 10;
                // 35% chance for 5 damage
                else if (gamble >= 50) hits[i] = 5;
                // 40% chance for 1 damage
                else if (gamble >= 10) hits[i] = 1;
                // 10% chance for 0 damage
            }
            return new Attack("Gamble on Bread", ScaleHits(hits), Attribute.Poison);
        }

        /// <summary>
        /// Lowers the current HP of this combatant depending on the given attack.
        /// Goose Gamblers take more damage from elemental attacks and less damage from poison attacks.
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