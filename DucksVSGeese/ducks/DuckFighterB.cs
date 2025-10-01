namespace DucksVSGeese
{
    /// <summary>
    /// Class for a Duck Sentry, a more defensive Duck Fighter.
    /// </summary>
    public class DuckFighterB : Duck
    {
        private const int MaximumHP = 210;
        public const string CombatantClass = "Duck Sentry"; // goose version can be sentinel
        private const bool AttacksAllies = false;
        public DuckFighterB(string name) : base(CombatantClass, name, MaximumHP, AttacksAllies)
        {
            // idk maybe do some duck stuff here
        }
        public DuckFighterB() : this(Duck.GetRandomName()) { }

        /// <summary>
        /// Duck Sentries attack once for 5 base damage. Their attacks deal Physical damage.
        /// </summary>
        /// <returns>An instance of the class Attack.</returns>
        public override Attack Attack()
        {
            return new Attack("Follow Aggressively", ScaleHits([5]), Attribute.Physical);
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
            Attribute attribute = attack.Attribute;
            if (attribute == Attribute.Physical) modifier = .1; // take little damage from physical attacks
            else if (attribute == Attribute.Magical) modifier = 1.5; // take a lot more damage from magical attacks
            else if (attribute == Attribute.Elemental) modifier = 1.25; // take more damage from elemental attacks
            else modifier = .5; // take less damage from every other attack

            return GetHit(attack.Hits, modifier);
        }
    }
}