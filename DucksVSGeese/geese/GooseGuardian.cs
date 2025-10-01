using DucksVSGeese.Attributes;

namespace DucksVSGeese.Geese
{
    /// <summary>
    /// Class for a Goose Guardian, a more defensive Goose Witch.
    /// </summary>
    public class GooseGuardian : Goose
    {
        private const int MaximumHP = 95;
        private const string CombatantClass = "Goose Guardian";
        private const bool AttacksAllies = false;
        private const double Regeneration = .2;
        public GooseGuardian(string name) : base(CombatantClass, name, MaximumHP, AttacksAllies, Regeneration)
        {
            // idk maybe do some goose stuff here
        }
        public GooseGuardian() : this(Goose.GetRandomName()) { }

        public static new string ClassName
        {
            get { return CombatantClass; }
        }

        /// <summary>
        /// Duck Wards attack 3 times for 1 base damage per hit. Their attacks deal Magical damage.
        /// </summary>
        /// <returns>An instance of the class Attack.</returns>
        public override Attack Attack()
        {
            return new Attack("Early Migration", ScaleHits([1, 1, 1]), DAttribute.Magical);
        }

        /// <summary>
        /// Lowers the current HP of this combatant depending on the given attack.
        /// Goose Guardians take more damage from physical and elemental attacks and less damage from every other attack.
        /// </summary>
        /// <param name="attack">The attack the combatant is taking damage from.</param>
        /// <returns>The total amount of damage the attack will deal.</returns>
        public override int TakeDamage(Attack attack)
        {
            double modifier;
            DAttribute attribute = attack.DAttribute;
            if (attribute == DAttribute.Magical) modifier = .1; // take little damage from magical attacks
            else if (attribute == DAttribute.Physical) modifier = 1.2; // take a lot more damage from physical attacks
            else if (attribute == DAttribute.Elemental) modifier = 1.15; // take more damage from elemental attacks
            else modifier = .5; // take less damage from every other attack

            return GetHit(attack.Hits, modifier);
        }
    }
}