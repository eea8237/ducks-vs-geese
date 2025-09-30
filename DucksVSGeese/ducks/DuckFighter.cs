namespace DucksVSGeese
{
    public class DuckFighter : Duck
    {
        private const int MaxHP = 150;
        public const string CombatantClass = "Duck Fighter";
        private const bool AttacksAllies = false;
        public DuckFighter(string name) : base(CombatantClass, name, MaxHP, AttacksAllies)
        {
            // idk maybe do some duck stuff here
        }
        public DuckFighter() : this(Duck.GetRandomName()) { }

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