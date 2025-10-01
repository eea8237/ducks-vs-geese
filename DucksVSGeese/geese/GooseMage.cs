using DucksVSGeese.Attributes;

namespace DucksVSGeese.Geese
{
    /// <summary>
    /// Class for a Goose Witch, a basic magical attacker.
    /// </summary>
    public class GooseMage : Goose
    {
        private const int MaximumHP = 64;
        private const string CombatantClass = "Goose Witch";
        private const bool AttacksAllies = false;
        private const double Regeneration = .03;
        public GooseMage(string name) : base(CombatantClass, name, MaximumHP, AttacksAllies, Regeneration)
        {
            // idk maybe do some duck stuff here
        }
        public GooseMage() : this(Goose.GetRandomName()) { }

        public static new string ClassName
        {
            get { return CombatantClass; }
        }

        /// <summary>
        /// Goose Witches attack 4 times for 5 base damage per hit. Their attacks deal Magical damage.
        /// </summary>
        /// <returns>An instance of the class Attack.</returns>
        public override Attack Attack()
        {
            return new Attack("Feather Cast", ScaleHits([5, 5, 5, 5]), DAttribute.Magical);
        }

        /// <summary>
        /// Lowers the current HP of this combatant depending on the given attack.
        /// Goose Witches take more damage from physical and elemental attacks and less damage from magical attacks.
        /// </summary>
        /// <param name="attack">The attack the combatant is taking damage from.</param>
        /// <returns>The total amount of damage the attack will deal.</returns>
        public override int TakeDamage(Attack attack)
        {
            double modifier = 1.0;
            DAttribute attribute = attack.DAttribute;
            if (attribute == DAttribute.Magical) modifier = .75; // take less damage from magical attacks
            else if (attribute == DAttribute.Physical || attribute == DAttribute.Elemental) modifier = 1.25; // take extra damage from physical and elemental attacks

            return GetHit(attack.Hits, modifier);
        }
    }
}