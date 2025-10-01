namespace DucksVSGeese.geese
{
    /// <summary>
    /// Class for a Goose Warrior, a basic physical attacker.
    /// </summary>
    public class GooseFighter : Goose
    {
        private const int MaximumHP = 64;
        private const string CombatantClass = "Goose Warrior";
        private const bool AttacksAllies = false;
        private const double Regeneration = .05;
        public GooseFighter(string name) : base(CombatantClass, name, MaximumHP, AttacksAllies, Regeneration)
        {
            // idk maybe do some duck stuff here
        }
        public GooseFighter() : this(Goose.GetRandomName()) { }

        public static new string ClassName
        {
            get { return CombatantClass; }
        }

        /// <summary>
        /// Goose Warriors attack once for 15 base damage. Their attacks deal Physical damage.
        /// </summary>
        /// <returns>An instance of the class Attack.</returns>
        public override Attack Attack()
        {
            return new Attack("Eye Peck", ScaleHits([15]), Attribute.Physical);
        }

        /// <summary>
        /// Lowers the current HP of this combatant depending on the given attack.
        /// Goose Warriors take more damage from magical and elemental attacks and less damage from physical attacks.
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