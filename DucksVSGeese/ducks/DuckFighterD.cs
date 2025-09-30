namespace DucksVSGeese
{
    /// <summary>
    /// Class for a more defensive Duck Fighter
    /// </summary>
    public class DuckFighterD : Duck
    {
        private const int MaxHP = 150;
        public const string CombatantClass = "Duck Fighter";
        private const bool AttacksAllies = false;
        public DuckFighterD(string name) : base(CombatantClass, name, MaxHP, AttacksAllies)
        {
            // idk maybe do some duck stuff here
        }
        public DuckFighterD() : this(Duck.GetRandomName()) { }

        /// <summary>
        /// Duck Fighters attack once for 25 base damage. Their attacks deal Physical damage.
        /// </summary>
        /// <returns>An instance of the class Attack.</returns>
        public override Attack Attack()
        {
            return new Attack("Eye Poke", ScaleHits([25]), Attribute.Physical);
        }

        public override int TakeDamage(Attack attack)
        {
            double modifier = 1.0;
            Attribute attribute = attack.Attribute;
            if (attribute == Attribute.Physical) modifier = .75; // take less damage from physical attacks
            else if (attribute == Attribute.Magical || attribute == Attribute.Elemental) modifier = 1.25; // take extra damage from magic and elemental attacks

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