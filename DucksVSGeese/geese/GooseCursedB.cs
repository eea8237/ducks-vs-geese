using DucksVSGeese.Attributes;

namespace DucksVSGeese.Geese
{
    /// <summary>
    /// Class for an Imprisoned Goose, an alternate cursed goose.
    /// </summary>
    public class GooseCursedB : Goose
    {
        private const int MaximumHP = 45;
        private const string CombatantClass = "Imprisoned Goose";
        private const bool AttacksAllies = false;
        private const double Regeneration = .01;
        public GooseCursedB(string name) : base(CombatantClass, name, MaximumHP, AttacksAllies, Regeneration)
        {
            // idk maybe do some goose stuff here
        }
        public GooseCursedB() : this(Goose.GetRandomName()) { }

        public static new string ClassName
        {
            get { return CombatantClass; }
        }

        /// <summary>
        /// Imprisoned Geese attack once for 1 base point of Cursed damage.
        /// Imprisoned Geese also curse their opponents so that their attacks strike the opposite side for a few turns.
        /// </summary>
        /// <returns>An instance of the class Attack.</returns>
        public override Attack Attack()
        {
            return new Attack("Knowledge No Bird Was Meant To Know", ScaleHits([1]), DAttribute.Cursed, true);
        }

        /// <summary>
        /// Lowers the current HP of this combatant depending on the given attack.
        /// Imprisoned Geese take more damage from elemental and holy attacks and less damage from every other attack.
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