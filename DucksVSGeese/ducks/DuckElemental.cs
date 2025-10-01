namespace DucksVSGeese.ducks
{
    /// <summary>
    /// Class for an Ethereal Duck, a basic elemental attacker.
    /// </summary>
    public class DuckElemental : Duck
    {
        private const int MaximumHP = 40;
        private const string CombatantClass = "Ethereal Duck";
        private const bool AttacksAllies = false;
        public DuckElemental(string name) : base(CombatantClass, name, MaximumHP, AttacksAllies)
        {
            // idk maybe do some duck stuff here
        }
        public DuckElemental() : this(Duck.GetRandomName()) { }

        public static new string ClassName
        {
            get { return CombatantClass; }
        }

        /// <summary>
        /// Ethereal Ducks attack once for 25, 50, or 100 base damage. Their attacks deal Elemental damage.
        /// </summary>
        /// <returns>An instance of the class Attack.</returns>
        public override Attack Attack()
        {
            int[] hits = [50];
            // 20% chance to be halved, 20% chance to be doubled
            int chance = RNG.Next(9);
            if (chance < 2) hits[0] /= 2;
            else if (chance > 7) hits[0] *= 2;

            return new Attack("Fleeting Flaps", ScaleHits(hits), Attribute.Elemental);
        }

        /// <summary>
        /// Lowers the current HP of this combatant depending on the given attack.
        /// Ethereal Ducks take more damage from every attack.
        /// </summary>
        /// <param name="attack">The attack the combatant is taking damage from.</param>
        /// <returns>The total amount of damage the attack will deal.</returns>
        public override int TakeDamage(Attack attack)
        {
            double modifier = 1.25;

            return GetHit(attack.Hits, modifier);
        }
    }
}