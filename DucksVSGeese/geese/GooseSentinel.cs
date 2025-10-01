using DucksVSGeese.Attributes;

namespace DucksVSGeese.Geese
{
    /// <summary>
    /// Class for a Goose Sentinel, a more defensive Goose Warrior.
    /// </summary>
    public class GooseSentinel : Goose
    {
        private const int MaximumHP = 95;
        private const string CombatantClass = "Goose Sentinel";
        private const bool AttacksAllies = false;
        private const double Regeneration = .2;
        public GooseSentinel(string name) : base(CombatantClass, name, MaximumHP, AttacksAllies, Regeneration)
        {
            // idk maybe do some goose stuff here
        }
        public GooseSentinel() : this(Goose.GetRandomName()) { }

        public static new string ClassName
        {
            get { return CombatantClass; }
        }

        /// <summary>
        /// Goose Sentinels attack once for 1 base damage. Their attacks deal Physical damage.
        /// </summary>
        /// <returns>An instance of the class Attack.</returns>
        public override Attack Attack()
        {
            return new Attack("Retreat to the Pond", ScaleHits([1]), DAttribute.Physical);
        }

        /// <summary>
        /// Lowers the current HP of this combatant depending on the given attack.
        /// Goose Sentinels take more damage from magical and elemental attacks and less damage from every other attack.
        /// </summary>
        /// <param name="attack">The attack the combatant is taking damage from.</param>
        /// <returns>The total amount of damage the attack will deal.</returns>
        public override int TakeDamage(Attack attack)
        {
            double modifier;
            DAttribute attribute = attack.DAttribute;
            if (attribute == DAttribute.Physical) modifier = .1; // take little damage from physical attacks
            else if (attribute == DAttribute.Magical) modifier = 1.2; // take more damage from magical attacks
            else if (attribute == DAttribute.Elemental) modifier = 1.15; // take more damage from elemental attacks
            else modifier = .5; // take less damage from every other attack

            return GetHit(attack.Hits, modifier);
        }
    }
}