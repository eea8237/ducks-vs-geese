namespace DucksVSGeese
{
    /// <summary>
    /// Class for a Duck Ward, a more defensive Duck Mage.
    /// </summary>
    public class DuckMageB : Duck
    {
        private const int MaximumHP = 210;
        public const string CombatantClass = "Duck Ward"; // goose version can be guardian
        private const bool AttacksAllies = false;
        public DuckMageB(string name) : base(CombatantClass, name, MaximumHP, AttacksAllies)
        {
            // idk maybe do some duck stuff here
        }
        public DuckMageB() : this(Duck.GetRandomName()) { }

        /// <summary>
        /// Duck Wards attack 3 times for 1, 2, and 3 base damage. Their attacks deal Physical damage.
        /// </summary>
        /// <returns>An instance of the class Attack.</returns>
        public override Attack Attack()
        {
            return new Attack("Quick Pecks", ScaleHits([1, 2, 3]), Attribute.Magical);
        }

        /// <summary>
        /// Lowers the current HP of this combatant depending on the given attack.
        /// Duck Wards take more damage from physical and elemental attacks and less damage from every other attack.
        /// </summary>
        /// <param name="attack">The attack the combatant is taking damage from.</param>
        /// <returns>The total amount of damage the attack will deal.</returns>
        public override int TakeDamage(Attack attack)
        {
            double modifier;
            Attribute attribute = attack.Attribute;
            if (attribute == Attribute.Magical) modifier = .1; // take little damage from magical attacks
            else if (attribute == Attribute.Physical) modifier = 1.5; // take a lot more damage from physical attacks
            else if (attribute == Attribute.Elemental) modifier = 1.25; // take more damage from elemental attacks
            else modifier = .5; // take less damage from every other attack

            return GetHit(attack.Hits, modifier);
        }
    }
}