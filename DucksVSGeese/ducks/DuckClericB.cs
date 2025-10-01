namespace DucksVSGeese
{
    /// <summary>
    /// Class for a Duck Physician, an alternate holy duck.
    /// </summary>
    public class DuckClericB : Duck
    {
        private const int MaxHP = 100;
        public const string CombatantClass = "Duck Physician";
        private const bool AttacksAllies = true;
        public DuckClericB(string name) : base(CombatantClass, name, MaxHP, AttacksAllies)
        {
            // idk maybe do some duck stuff here
        }

        public DuckClericB() : this(Duck.GetRandomName()) {}

        /// <summary>
        /// Duck Physicians heal allies 3 times for 5, 10, and 15 base points. Their 'attacks' deal Holy damage.
        /// </summary>
        /// <returns>An instance of the class Attack.</returns>
        public override Attack Attack()
        {
            return new Attack("Tender Duck and Care", ScaleHits([-5, -10, -15]), Attribute.Holy);
        }

        /// <summary>
        /// Lowers the current HP of this combatant depending on the given attack.
        /// Duck Clerics take less damage from cursed attacks, get healed by holy attacks, and take more damage from every other attack.
        /// </summary>
        /// <param name="attack">The attack the combatant is taking damage from.</param>
        /// <returns>The total amount of damage the attack will deal.</returns>
        public override int TakeDamage(Attack attack)
        {
            double modifier;
            Attribute attribute = attack.Attribute;
            if (attribute == Attribute.Holy) modifier = -.25; // get healed from holy attacks
            else if (attribute == Attribute.Cursed) modifier = .75; // resist cursed attacks
            else if (attribute == Attribute.Elemental) modifier = 1.5; // everyone takes more damage from elemental attacks (this duck takes even more damage)
            else modifier = 1.25; // take extra damage from every other attack

            int totalDamage = 0;
            foreach (int hit in attack.Hits)
            {
                int damage = Convert.ToInt32(hit * modifier);
                totalDamage += damage;
                currentHP -= damage;
                currentHP = Combatant.CapHP(currentHP, maxHP);
            }
            return totalDamage;
        }
    }
}