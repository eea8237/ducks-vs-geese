using DucksVSGeese.Attributes;

namespace DucksVSGeese.Geese
{
    /// <summary>
    /// Class for an Ephemeral Goose, a basic elemental attacker.
    /// </summary>
    public class EphemeralGoose : Goose
    {
        private const int MaximumHP = 15;
        private const string CombatantClass = "Ephemeral Goose";
        private const bool AttacksAllies = false;
        private const double Regeneration = 1;
        public EphemeralGoose(string name) : base(CombatantClass, name, MaximumHP, AttacksAllies, Regeneration)
        {
            // idk maybe do some goose stuff here
        }
        public EphemeralGoose() : this(Goose.GetRandomName()) { }

        public static new string ClassName
        {
            get { return CombatantClass; }
        }

        /// <summary>
        /// Ephemeral Geese attack once for 25, 50, or 100 base damage. Their attacks deal Elemental damage.
        /// </summary>
        /// <returns>An instance of the class Attack.</returns>
        public override Attack Attack()
        {
            int[] hits = [50];
            // 20% chance to be halved, 20% chance to be doubled
            int chance = RNG.Next(9);
            if (chance < 2) hits[0] /= 2;
            else if (chance > 7) hits[0] *= 2;

            return new Attack("Drag to a Submerged Grave", ScaleHits(hits), DAttribute.Elemental);
        }

        /// <summary>
        /// Lowers the current HP of this combatant depending on the given attack.
        /// Ephemeral Geese take more damage from every attack.
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