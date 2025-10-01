using DucksVSGeese.Attributes;

namespace DucksVSGeese.Ducks
{
    /// <summary>
    /// Class for a Protean Duck, an alternate elemental attacker.
    /// </summary>
    public class DuckElementalB : Duck
    {
        private const int MaximumHP = 145;
        private const string CombatantClass = "Protean Duck";
        private const bool AttacksAllies = false;
        public DuckElementalB(string name) : base(CombatantClass, name, MaximumHP, AttacksAllies)
        {
            // idk maybe do some duck stuff here
        }
        public DuckElementalB() : this(Duck.GetRandomName()) { }

        public static new string ClassName
        {
            get { return CombatantClass; }
        }

        /// <summary>
        /// Protean Ducks attack 3 times for 10 base damage per hit. Their attacks are of a random element.
        /// </summary>
        /// <returns>An instance of the class Attack.</returns>
        public override Attack Attack()
        {
            DAttribute[] attributes = Enum.GetValues<DAttribute>();
            DAttribute attribute = attributes[RNG.Next(attributes.Length)];
            return new Attack($"Whirlwind of {attribute} Plumes", ScaleHits([10, 10, 10]), attribute);
        }

        /// <summary>
        /// Lowers the current HP of this combatant depending on the given attack.
        /// Protean Ducks take less damage from every attack.
        /// </summary>
        /// <param name="attack">The attack the combatant is taking damage from.</param>
        /// <returns>The total amount of damage the attack will deal.</returns>
        public override int TakeDamage(Attack attack)
        {
            double modifier = .75;

            return GetHit(attack.Hits, modifier);
        }
    }
}