using DucksVSGeese.Attributes;

namespace DucksVSGeese.Geese
{
    /// <summary>
    /// Class for a Universal Goose, an alternate elemental attacker.
    /// </summary>
    public class GooseElementalB : Goose
    {
        private const int MaximumHP = 40;
        private const string CombatantClass = "Universal Goose";
        private const bool AttacksAllies = false;
        private const double Regeneration = .15;
        public GooseElementalB(string name) : base(CombatantClass, name, MaximumHP, AttacksAllies, Regeneration)
        {
            // idk maybe do some goose stuff here
        }
        public GooseElementalB() : this(Goose.GetRandomName()) { }

        public static new string ClassName
        {
            get { return CombatantClass; }
        }

        /// <summary>
        /// Universal Geese attack twice times for 5 and 10 base damage. Their attacks are of a random element.
        /// </summary>
        /// <returns>An instance of the class Attack.</returns>
        public override Attack Attack()
        {
            DAttribute[] attributes = Enum.GetValues<DAttribute>();
            DAttribute attribute = attributes[RNG.Next(attributes.Length)];
            return new Attack($"Burst of {attribute} Flaps", ScaleHits([5, 10]), attribute);
        }

        /// <summary>
        /// Lowers the current HP of this combatant depending on the given attack.
        /// Universal Geese take less damage from every attack.
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