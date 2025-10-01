using DucksVSGeese.Attributes;

namespace DucksVSGeese.Geese
{
    /// <summary>
    /// Class for an Unholy Goose, a basic cursed attacker.
    /// </summary>
    public class GooseCursed : Goose
    {
        private const int MaximumHP = 58;
        private const string CombatantClass = "Unholy Goose";
        private const bool AttacksAllies = false;
        private const double Regeneration = .02;
        public GooseCursed(string name) : base(CombatantClass, name, MaximumHP, AttacksAllies, Regeneration)
        {
            // idk maybe do some goose stuff here
        }
        public GooseCursed() : this(Goose.GetRandomName()) { }

        public static new string ClassName
        {
            get { return CombatantClass; }
        }

        /// <summary>
        /// Unholy Geese attack twice for 0-30 base damage per hit. Their attacks deal Cursed damage.
        /// </summary>
        /// <returns>An instance of the class Attack.</returns>
        public override Attack Attack()
        {
            int[] hits = [0, 0];
            // hits twice for between 0 and 30 points of damage
            for (int i = 0; i < hits.Length; i++)
            {
                hits[i] = RNG.Next(16);
                // hits are weighed towards 0-15 points
                if (RNG.Next(3) == 1)
                {
                    // 1 in 3 chance to add 1 - 15 points of damage
                    hits[i] += RNG.Next(1, 16);
                }
            }
            return new Attack("Unearthly Honk", ScaleHits(hits), DAttribute.Cursed);
        }

        /// <summary>
        /// Lowers the current HP of this combatant depending on the given attack.
        /// Unholy Geese take more damage from elemental and holy attacks and less damage from every other attack.
        /// </summary>
        /// <param name="attack">The attack the combatant is taking damage from.</param>
        /// <returns>The total amount of damage the attack will deal.</returns>
        public override int TakeDamage(Attack attack)
        {
            double modifier;
            DAttribute attribute = attack.DAttribute;
            if (attribute == DAttribute.Cursed) modifier = 0.5; // greatly resist cursed attacks
            else if (attribute == DAttribute.Holy) modifier = 1.5; // take a lot of extra damage from holy attacks
            else if (attribute == DAttribute.Elemental) modifier = 1.25; // everyone takes more damage from elemental attacks
            else modifier = .75; // take less damage from every other attack

            return GetHit(attack.Hits, modifier);
        }
    }
}