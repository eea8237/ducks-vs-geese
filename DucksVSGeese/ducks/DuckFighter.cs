namespace DucksVSGeese
{
    /// <summary>
    /// Class for a Duck Fighter, a basic physical attacker.
    /// </summary>
    public class DuckFighter : Duck
    {
        private const int MaximumHP = 150;
        public const string CombatantClass = "Duck Fighter";
        private const bool AttacksAllies = false;
        public DuckFighter(string name) : base(CombatantClass, name, MaximumHP, AttacksAllies)
        {
            // idk maybe do some duck stuff here
        }
        public DuckFighter() : this(Duck.GetRandomName()) { }

        /// <summary>
        /// Duck Fighters attack once for 25 base damage. Their attacks deal Physical damage.
        /// </summary>
        /// <returns>An instance of the class Attack.</returns>
        public override Attack Attack()
        {
            return new Attack("Eye Poke", ScaleHits([25]), Attribute.Physical);
        }

        /// <summary>
        /// Lowers the current HP of this combatant depending on the given attack.
        /// Duck Fighters take more damage from magical and elemental attacks and less damage from physical attacks.
        /// </summary>
        /// <param name="attack">The attack the combatant is taking damage from.</param>
        /// <returns>The total amount of damage the attack will deal.</returns>
        public override int TakeDamage(Attack attack)
        {
            double modifier = 1.0;
            Attribute attribute = attack.Attribute;
            if (attribute == Attribute.Physical) modifier = .75; // take less damage from physical attacks
            else if (attribute == Attribute.Magical || attribute == Attribute.Elemental) modifier = 1.25; // take extra damage from magic and elemental attacks

            return GetHit(attack.Hits, modifier);
        }
    }
}