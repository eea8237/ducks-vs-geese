using DucksVSGeese.Attributes;

namespace DucksVSGeese.Geese
{
    /// <summary>
    /// Class for a Goose Vandal, a basic poison attacker.
    /// </summary>
    public class GooseVandal : Goose
    {
        private const int MaximumHP = 50;
        private const string CombatantClass = "Goose Vandal";
        private const bool AttacksAllies = false;
        private const double Regeneration = .1;
        public GooseVandal(string name) : base(CombatantClass, name, MaximumHP, AttacksAllies, Regeneration)
        {
            // idk maybe do some duck stuff here
        }
        public GooseVandal() : this(Goose.GetRandomName()) { }

        public static new string ClassName
        {
            get { return CombatantClass; }
        }

        /// <summary>
        /// Goose Vandals attack 1-3 times for 5 or 10 base damage per hit. Their attacks deal Poison damage.
        /// </summary>
        /// <returns>An instance of the class Attack.</returns>
        public override Attack Attack()
        {
            // hits 1-3 times
            int[] hits = new int[RNG.Next(1, 4)];
            for (int i = 0; i < hits.Length; i++)
            {
                // 1 in 3 chance for 10 damage, otherwise 5 damage
                hits[i] = RNG.Next(3) == 0 ? 10 : 5;
            }
            return new Attack("Harrass & Deface", ScaleHits(hits), DAttribute.Poison);
        }

        /// <summary>
        /// Lowers the current HP of this combatant depending on the given attack.
        /// Goose Vandals take more damage from elemental attacks and less damage from poison attacks.
        /// </summary>
        /// <param name="attack">The attack the combatant is taking damage from.</param>
        /// <returns>The total amount of damage the attack will deal.</returns>
        public override int TakeDamage(Attack attack)
        {
            double modifier = 1.0;
            DAttribute attribute = attack.DAttribute;
            if (attribute == DAttribute.Poison) modifier = .75; // take less damage from poison attacks
            else if (attribute == DAttribute.Elemental) modifier = 1.25; // everyone takes more damage from elemental attacks

            return GetHit(attack.Hits, modifier);
        }
    }
}