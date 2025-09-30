namespace DucksVSGeese
{
    public class DuckCleric : Duck
    {
        private const int MaxHP = 125;
        public const string CombatantClass = "Duck Cleric";
        private const bool AttacksAllies = false;
        public DuckCleric(string name) : base(CombatantClass, name, MaxHP, AttacksAllies)
        {
            // idk maybe do some duck stuff here
        }

        public DuckCleric() : this(Duck.GetRandomName()) {}

        public override Attack Attack()
        {
            return new Attack("Duck, Duck, Goose", ScaleHits([5, 10, 15]), Attribute.Holy);
        }

        public override int TakeDamage(Attack attack)
        {
            double modifier = 1.0;
            Attribute attribute = attack.Attribute;
            if (attribute == Attribute.Physical) modifier = -.5; // get healed from holy attacks
            else if (attribute == Attribute.Cursed) modifier = .5; // greatly resist cursed attacks
            else if (attribute == Attribute.Elemental) modifier = 1.25; // everyone takes more damage from elemental attacks
            else modifier = 1.15; // take extra damage from every other attack

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