namespace DucksVSGeese
{
    public class GooseThief : Goose
    {
        private const int MaxHP = 50;
        private const string CombatantClass = "Goose Vandal";
        private const bool AttacksAllies = false;
        private const double Regeneration = .1;
        public GooseThief(string name) : base(CombatantClass, name, MaxHP, AttacksAllies, Regeneration)
        {
            // idk maybe do some duck stuff here
        }
        public GooseThief() : this(Goose.GetRandomName()) {}

        public override Attack Attack()
        {
            // hits 1-3 times
            int[] hits = new int[RNG.Next(1, 4)];
            for (int i = 0; i < hits.Length; i++)
            {
                // 1 in 3 chance for 10 damage, otherwise 5 damage
                hits[i] = RNG.Next(3) == 0 ? 10 : 5;
            }
            return new Attack("Harrass & Deface", ScaleHits(hits), Attribute.Poison);
        }

        public override int TakeDamage(Attack attack)
        {
            double modifier = 1.0;
            Attribute attribute = attack.Attribute;
            if (attribute == Attribute.Poison) modifier = .75; // take less damage from poison attacks
            else if (attribute == Attribute.Elemental) modifier = 1.25; // everyone takes more damage from elemental attacks

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