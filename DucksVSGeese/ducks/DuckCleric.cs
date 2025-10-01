using DucksVSGeese.Attributes;

namespace DucksVSGeese.Ducks
{
    /// <summary>
    /// Class for a Duck Cleric, a basic holy attacker.
    /// </summary>
    public class DuckCleric : Duck
    {
        private const int MaximumHP = 125;
        private const string CombatantClass = "Duck Cleric";
        private const bool AttacksAllies = false;
        public DuckCleric(string name) : base(CombatantClass, name, MaximumHP, AttacksAllies)
        {
            // idk maybe do some duck stuff here
        }
        public DuckCleric() : this(Duck.GetRandomName()) { }

        public static new string ClassName
        {
            get { return CombatantClass; }
        }

        /// <summary>
        /// Duck Clerics attack 3 times for 5, 10, and 15 base damage. Their attacks deal Holy damage.
        /// </summary>
        /// <returns>An instance of the class Attack.</returns>
        public override Attack Attack()
        {
            return new Attack("Duck, Duck, Goose", ScaleHits([5, 10, 15]), DAttribute.Holy);
        }

        /// <summary>
        /// Lowers the current HP of this combatant depending on the given attack.
        /// Duck Clerics take less damage from cursed attacks, get healed by holy attacks, and take more damage from every other attack.
        /// </summary>
        /// <param name="attack">The attack the combatant is taking damage from.</param>
        /// <returns>The total amount of damage the attack will deal.</returns>
        public override int TakeDamage(Attack attack)
        {
            double modifier;
            DAttribute attribute = attack.DAttribute;
            if (attribute == DAttribute.Holy && attack.Hits[0] > 0) modifier = -.5; // get healed by harmful holy attacks
            else if (attribute == DAttribute.Cursed || attribute == DAttribute.Holy) modifier = .5; // greatly resist cursed attacks and get healed less by helpful holy attacks
            else if (attribute == DAttribute.Elemental) modifier = 1.25; // everyone takes more damage from elemental attacks
            else modifier = 1.15; // take extra damage from every other attack

            return GetHit(attack.Hits, modifier);
        }
    }
}