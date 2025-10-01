namespace DucksVSGeese
{
    /// <summary>
    /// Class for a Goose Priest, a basic holy attacker.
    /// </summary>
    public class GooseCleric : Goose
    {
        private const int MaximumHP = 78;
        public const string CombatantClass = "Goose Priest";
        private const bool AttacksAllies = false;
        private const double Regeneration = .02;

        public GooseCleric(string name) : base(CombatantClass, name, MaximumHP, AttacksAllies, Regeneration)
        {
            // idk maybe do some goose stuff here
        }

        public GooseCleric() : this(Goose.GetRandomName()) { }

        /// <summary>
        /// Goose Priests attack twice for 5 base damage per hit. Their attacks deal Holy damage.
        /// </summary>
        /// <returns>An instance of the class Attack.</returns>
        public override Attack Attack()
        {
            return new Attack("All That Is Goose and Holy", ScaleHits([5, 5]), Attribute.Holy);
        }

        /// <summary>
        /// Lowers the current HP of this combatant depending on the given attack.
        /// Goose Priests take less damage from cursed attacks, get healed by holy attacks, and take more damage from every other attack.
        /// </summary>
        /// <param name="attack">The attack the combatant is taking damage from.</param>
        /// <returns>The total amount of damage the attack will deal.</returns>
        public override int TakeDamage(Attack attack)
        {
            double modifier;
            Attribute attribute = attack.Attribute;
            if (attribute == Attribute.Holy && attack.Hits[0] > 0) modifier = -.5;
            else if (attribute == Attribute.Cursed || attribute == Attribute.Holy) modifier = .5; // greatly resist cursed attacks and get healed less by helpful holy attacks
            else if (attribute == Attribute.Cursed) modifier = .5; // greatly resist cursed attacks
            else if (attribute == Attribute.Elemental) modifier = 1.25; // everyone takes more damage from elemental attacks
            else modifier = 1.15; // take extra damage from every other attack

            return GetHit(attack.Hits, modifier);
            
        }
    }
}