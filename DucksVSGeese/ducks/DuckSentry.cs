using DucksVSGeese.Attributes;

namespace DucksVSGeese.Ducks
{
    /// <summary>
    /// Class for a Duck Sentry, a more defensive Duck Fighter.
    /// </summary>
    public class DuckSentry : Duck
    {
        private const int MaximumHP = 210;
        private const string CombatantClass = "Duck Sentry"; // goose version can be sentinel
        private const bool AttacksAllies = false;
        public DuckSentry(string name) : base(CombatantClass, name, MaximumHP, AttacksAllies)
        {
            // idk maybe do some duck stuff here
        }
        public DuckSentry() : this(Duck.GetRandomName()) { }

        public static new string ClassName
        {
            get { return CombatantClass; }
        }

        /// <summary>
        /// Duck Sentries attack once for 5 base damage. Their attacks deal Physical damage.
        /// </summary>
        /// <returns>An instance of the class Attack.</returns>
        public override Attack Attack()
        {
            return new Attack("Follow Aggressively", ScaleHits([5]), DAttribute.Physical);
        }

        /// <summary>
        /// Lowers the current HP of this combatant depending on the given attack.
        /// Duck Sentries take more damage from magical and elemental attacks and less damage from every other attack.
        /// </summary>
        /// <param name="attack">The attack the combatant is taking damage from.</param>
        /// <returns>The total amount of damage the attack will deal.</returns>
        public override int TakeDamage(Attack attack)
        {
            double modifier;
            DAttribute attribute = attack.DAttribute;
            if (attribute == DAttribute.Physical) modifier = .1; // take little damage from physical attacks
            else if (attribute == DAttribute.Magical) modifier = 1.2; // take a lot damage from magical attacks
            else if (attribute == DAttribute.Elemental) modifier = 1.15; // take more damage from elemental attacks
            else modifier = .5; // take less damage from every other attack

            return GetHit(attack.Hits, modifier);
        }
    }
}