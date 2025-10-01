using DucksVSGeese.Attributes;

namespace DucksVSGeese.Ducks
{
    /// <summary>
    /// Class for a Duck Thief, a basic poison attacker.
    /// </summary>
    public class DuckThief : Duck
    {
        private const int MaximumHP = 125;
        private const string CombatantClass = "Duck Thief";
        private const bool AttacksAllies = false;
        public DuckThief(string name) : base(CombatantClass, name, MaximumHP, AttacksAllies)
        {
            // idk maybe do some duck stuff here
        }
        public DuckThief() : this(Duck.GetRandomName()) { }

        public static new string ClassName
        {
            get { return CombatantClass; }
        }

        /// <summary>
        /// Duck Thieves attack 1-2 times for 10 or 20 base damage per hit. Their attacks deal Poison damage.
        /// </summary>
        /// <returns>An instance of the class Attack.</returns>
        public override Attack Attack()
        {
            // hits 1-2 times
            int[] hits = new int[RNG.Next(1, 3)];
            for (int i = 0; i < hits.Length; i++)
            {
                // 1 in 3 chance for 20 damage, otherwise 10 damage
                hits[i] = RNG.Next(3) == 0 ? 20 : 10;
            }
            return new Attack("Snatch", ScaleHits(hits), DAttribute.Poison);
        }

        /// <summary>
        /// Lowers the current HP of this combatant depending on the given attack.
        /// Duck Thieves take more damage from elemental attacks and less damage from poison attacks.
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