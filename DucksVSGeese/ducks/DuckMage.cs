namespace DucksVSGeese
{
    public class DuckMage : Duck
    {
        private const int MaxHP = 120;
        public const string CombatantClass = "Duck Mage";
        private const bool AttacksAllies = false;
        public DuckMage(string name) : base(CombatantClass, name, MaxHP, AttacksAllies)
        {
            // idk maybe do some duck stuff here
        }
        public DuckMage() : this(Duck.GetRandomName()) {}

        public override Attack Attack()
        {
            return new Attack("Feather Bombardment", ScaleHits([9, 9, 9, 9]), Attribute.Magical);
        }

        public override int TakeDamage(Attack attack)
        {
            double modifier = 1.0;
            Attribute attribute = attack.Attribute;
            if (attribute == Attribute.Magical) modifier = .75; // take less damage from magical attacks
            else if (attribute == Attribute.Physical || attribute == Attribute.Elemental) modifier = 1.25; // take extra damage from physical and elemental attacks

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