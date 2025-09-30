namespace DucksVSGeese
{
    public class GooseFighter : Goose
    {
        private const int MaxHP = 64;
        private const string CombatantClass = "Goose Warrior";
        private const bool AttacksAllies = false;
        private const double Regeneration = .05;
        public GooseFighter(string name) : base(CombatantClass, name, MaxHP, AttacksAllies, Regeneration)
        {
            // idk maybe do some duck stuff here
        }
        public GooseFighter() : this(Goose.GetRandomName()) {}

        public override Attack Attack()
        {
            return new Attack("Eye Peck", ScaleHits([15]), Attribute.Physical);
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