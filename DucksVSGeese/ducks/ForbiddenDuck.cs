using DucksVSGeese.Attributes;

namespace DucksVSGeese.Ducks
{
    /// <summary>
    /// Class for an Forbidden Duck, an alternate cursed duck.
    /// </summary>
    public class ForbiddenDuck : Duck
    {
        private const int MaximumHP = 125;
        private const string CombatantClass = "Forbidden Duck";
        private const bool AttacksAllies = false;
        public ForbiddenDuck(string name) : base(CombatantClass, name, MaximumHP, AttacksAllies)
        {
            // idk maybe do some duck stuff here
        }
        public ForbiddenDuck() : this(Duck.GetRandomName()) { }

        public static new string ClassName
        {
            get { return CombatantClass; }
        }

        /// <summary>
        /// Forbidden Ducks attack once for 1 base point of Cursed damage.
        /// Forbidden Ducks also curse their opponents so that their attacks strike the opposite side for a few turns.
        /// </summary>
        /// <returns>An instance of the class Attack.</returns>
        public override Attack Attack()
        {
            return new Attack("Eldritch Quacking", ScaleHits([1]), DAttribute.Cursed, true);
        }

        /// <summary>
        /// Lowers the current HP of this combatant depending on the given attack.
        /// Forbidden Ducks take more damage from elemental and holy attacks and less damage from every other attack.
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