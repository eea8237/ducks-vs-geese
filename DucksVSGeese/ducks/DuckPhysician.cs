using DucksVSGeese.Attributes;

namespace DucksVSGeese.Ducks
{
    /// <summary>
    /// Class for a Duck Physician, an alternate holy duck.
    /// </summary>
    public class DuckPhysician : Duck
    {
        private const int MaximumHP = 100;
        private const string CombatantClass = "Duck Physician";
        private const bool AttacksAllies = true;
        public DuckPhysician(string name) : base(CombatantClass, name, MaximumHP, AttacksAllies)
        {
            // idk maybe do some duck stuff here
        }
        public DuckPhysician() : this(Duck.GetRandomName()) { }

        public static new string ClassName
        {
            get { return CombatantClass; }
        }

        /// <summary>
        /// Duck Physicians heal allies 3 times for 5, 10, and 15 base points. Their 'attacks' deal Holy damage.
        /// </summary>
        /// <returns>An instance of the class Attack.</returns>
        public override Attack Attack()
        {
            return new Attack("Tender Duck and Care", ScaleHits([-5, -10, -15]), DAttribute.Holy);
        }

        /// <summary>
        /// Lowers the current HP of this combatant depending on the given attack.
        /// Duck Physicians take less damage from cursed attacks, get healed by holy attacks, and take more damage from every other attack.
        /// </summary>
        /// <param name="attack">The attack the combatant is taking damage from.</param>
        /// <returns>The total amount of damage the attack will deal.</returns>
        public override int TakeDamage(Attack attack)
        {
            double modifier;
            DAttribute attribute = attack.DAttribute;
            if (attribute == DAttribute.Holy && attack.Hits[0] > 0) modifier = -.25; // get healed by harmful holy attacks
            else if (attribute == DAttribute.Holy) modifier = .25; // get healed less by helpful holy attacks
            else if (attribute == DAttribute.Cursed) modifier = .75; // resist cursed attacks
            else if (attribute == DAttribute.Elemental) modifier = 1.5; // everyone takes more damage from elemental attacks (this duck takes even more damage)
            else modifier = 1.25; // take extra damage from every other attack

            return GetHit(attack.Hits, modifier);
        }
    }
}