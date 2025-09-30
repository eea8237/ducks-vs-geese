namespace DucksVSGeese
{
    public class GooseMage : Goose
    {
        private const int MaxHP = 64;
        private const string CombatantClass = "Goose Witch";
        private const bool AttacksAllies = false;
        private const double Regeneration = .03;
        public GooseMage(string name) : base(CombatantClass, name, MaxHP, AttacksAllies, Regeneration)
        {
            // idk maybe do some duck stuff here
        }
        public GooseMage() : this(Goose.GetRandomName()) {}

        public override Attack Attack()
        {
            return new Attack("Feather Cast", ScaleHits([5, 5, 5, 5]), Attribute.Magical);
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