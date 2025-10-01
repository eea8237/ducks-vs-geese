using DucksVSGeese.Attributes;

namespace DucksVSGeese.Ducks
{
    /// <summary>
    /// Class for a Duck Mage, a basic magical attacker.
    /// </summary>
    public class DuckMage : Duck
    {
        private const int MaximumHP = 150;
        private const string CombatantClass = "Duck Mage";
        private const bool AttacksAllies = false;
        public DuckMage(string name) : base(CombatantClass, name, MaximumHP, AttacksAllies)
        {
            // idk maybe do some duck stuff here
        }
        public DuckMage() : this(Duck.GetRandomName()) { }

        public static new string ClassName
        {
            get { return CombatantClass; }
        }

        /// <summary>
        /// Duck Mages attack 4 times for 9 base damage per hit. Their attacks deal Magical damage.
        /// </summary>
        /// <returns>An instance of the class Attack.</returns>
        public override Attack Attack()
        {
            return new Attack("Feather Bombardment", ScaleHits([9, 9, 9, 9]), DAttribute.Magical);
        }

        /// <summary>
        /// Lowers the current HP of this combatant depending on the given attack.
        /// Duck Mages take more damage from physical and elemental attacks and less damage from magical attacks.
        /// </summary>
        /// <param name="attack">The attack the combatant is taking damage from.</param>
        /// <returns>The total amount of damage the attack will deal.</returns>
        public override int TakeDamage(Attack attack)
        {
            double modifier = 1.0;
            DAttribute attribute = attack.DAttribute;
            if (attribute == DAttribute.Magical) modifier = .75; // take less damage from magical attacks
            else if (attribute == DAttribute.Physical || attribute == DAttribute.Elemental) modifier = 1.25; // take extra damage from physical and elemental attacks

            return GetHit(attack.Hits, modifier);
        }
    }
}