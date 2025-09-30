namespace DucksVSGeese
{
    public class DuckCursed : Duck
    {
        private const int MaxHP = 125;
        public const string CombatantClass = "Accursed Duck";
        private const bool AttacksAllies = false;
        public DuckCursed(string name) : base(CombatantClass, name, MaxHP, AttacksAllies)
        {
            // idk maybe do some duck stuff here
        }
        public DuckCursed() : this(Duck.GetRandomName()) {}

        public override Attack Attack()
        {
            int[] hits = [0, 0];
            // hits twice for between 0 and 50 points of damage
            for (int i = 0; i < hits.Length; i++)
            {
                hits[i] = RNG.Next(16);
                // hits are weighed towards 0-15 points
                if (RNG.Next(3) == 1)
                {
                    // 1 in 3 chance to add 1 - 35 points of damage
                    hits[i] += RNG.Next(1, 36);
                }
            }
            return new Attack("Unholy Quack", ScaleHits(hits), Attribute.Cursed);
        }

        public override int TakeDamage(Attack attack)
        {
            double modifier = 1.0;
            Attribute attribute = attack.Attribute;
            if (attribute == Attribute.Cursed) modifier = 0.5; // greatly resist cursed attacks
            else if (attribute == Attribute.Holy) modifier = 1.5; // take a lot of extra damage from holy attacks
            else if (attribute == Attribute.Elemental) modifier = 1.25; // everyone takes more damage from elemental attacks
            else modifier = .75; // take less damage from every other attack

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