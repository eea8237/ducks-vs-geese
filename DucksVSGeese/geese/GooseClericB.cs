using DucksVSGeese.Attributes;

namespace DucksVSGeese.Geese
{
    /// <summary>
    /// Class for a Goose Medic, an alternate holy goose.
    /// </summary>
    public class GooseClericB : Goose
    {
        private const int MaximumHP = 40;
        private const string CombatantClass = "Goose Medic";
        private const bool AttacksAllies = true;
        private const double Regeneration = .02;
        public GooseClericB(string name) : base(CombatantClass, name, MaximumHP, AttacksAllies, Regeneration)
        {
            // idk maybe do some goose stuff here
        }

        public GooseClericB() : this(Goose.GetRandomName()) { }

        public static new string ClassName
        {
            get { return CombatantClass; }
        }

        /// <summary>
        /// Goose Medics heal allies twice for -5 base points per hit. Their 'attacks' deal Holy damage.
        /// </summary>
        /// <returns>An instance of the class Attack.</returns>
        public override Attack Attack()
        {
            return new Attack("Surgical Pecks", ScaleHits([-5, -5]), DAttribute.Holy);
        }

        /// <summary>
        /// Lowers the current HP of this combatant depending on the given attack.
        /// Goose Medics take less damage from cursed attacks, get healed by holy attacks, and take more damage from every other attack.
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