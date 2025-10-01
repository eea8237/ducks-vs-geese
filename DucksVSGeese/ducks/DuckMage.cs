namespace DucksVSGeese
{
    /// <summary>
    /// Class for a Duck Mage, a basic magical attacker.
    /// </summary>
    public class DuckMage : Duck
    {
        private const int MaximumHP = 150;
        public const string CombatantClass = "Duck Mage";
        private const bool AttacksAllies = false;
        public DuckMage(string name) : base(CombatantClass, name, MaximumHP, AttacksAllies)
        {
            // idk maybe do some duck stuff here
        }
        public DuckMage() : this(Duck.GetRandomName()) { }

        /// <summary>
        /// Duck Mages attack 4 times for 9 base damage per hit. Their attacks deal Magical damage.
        /// </summary>
        /// <returns>An instance of the class Attack.</returns>
        public override Attack Attack()
        {
            return new Attack("Feather Bombardment", ScaleHits([9, 9, 9, 9]), Attribute.Magical);
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
            Attribute attribute = attack.Attribute;
            if (attribute == Attribute.Magical) modifier = .75; // take less damage from magical attacks
            else if (attribute == Attribute.Physical || attribute == Attribute.Elemental) modifier = 1.25; // take extra damage from physical and elemental attacks

            return GetHit(attack.Hits, modifier);
        }
    }
}