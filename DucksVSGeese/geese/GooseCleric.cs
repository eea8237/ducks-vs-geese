namespace DucksVSGeese
{
    public class GooseCleric : Goose
    {
        private const int MaxHP = 78;
        private const string CombatantClass = "Goose Priest";
        private const bool AttacksAllies = false;
        private const double Regeneration = .02;

        public GooseCleric(string name) : base(CombatantClass, name, MaxHP, AttacksAllies, Regeneration)
        {
            // idk maybe do some goose stuff here
        }

        public GooseCleric() : this(Goose.GetRandomName()) {}

        public override Attack Attack()
        {
            return new Attack("All That Is Goose and Holy", ScaleHits([5, 5]), Attribute.Holy);
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