using System.ComponentModel;
using System.Runtime.ConstrainedExecution;

namespace DucksVSGeese
{
    public abstract class Combatant
    {
        protected readonly string combatClass;
        protected readonly string name;
        protected readonly int maxHP;
        protected int currentHP;
        /// <summary>
        /// whether this Combatant attacks allies or not
        /// </summary>
        protected readonly bool attackAllies;

        /// <summary>
        /// whether this Combatant attacks allies or not
        /// </summary>
        protected int cursedTimer;

        /// <summary>
        /// For scaling at some point.
        /// </summary>
        private int level;
        public const int LevelCap = 1000;
        public Combatant(string combatClass, string name, int maxHP, bool attackAllies)
        {
            this.combatClass = combatClass;
            this.attackAllies = attackAllies;
            cursedTimer = 0;
            level = 1;
            this.name = name;
            this.maxHP = maxHP*level;
            currentHP = this.maxHP;
        }
        /**<summary>
        Name for a class of duck or goose.
        </summary>*/
        public string CombatClass
        {
            get { return combatClass; }
        }
        public string Name
        {
            get { return name; }
        }
        public int CurrentHP
        {
            get { return currentHP; }
        }

        public bool AttackAllies
        {
            get { return attackAllies; }
        }

        public int CursedTimer
        {
            get { return cursedTimer; }
            set
            {
                cursedTimer = value;
                if (cursedTimer < 0) cursedTimer = 0;
            }
        }

        public int Level
        {
            get { return level; }
            set
            {
                level = value;
                // level is capped at the level cap or 0
                level = level > LevelCap ? LevelCap : level < 0 ? 0 : level;
            }
        }


        public string GetHPString()
        {
            return $"{currentHP}/{maxHP}";
        }

        public string GetTitle()
        {
            return $"{combatClass} {name}";
        }
        public override string? ToString()
        {
            return $"{GetTitle()} ({GetHPString()})";
        }

        /** <summary>
        * What attack this Combatant will use.
        * </summary> */
        public abstract Attack Attack();

        /** <summary>
        * Process damage from an attack. Returns the total amount of damage taken.
        * </summary> */
        public abstract int TakeDamage(Attack attack);

        /** <summary>
        * Returns true if the Combatant has HP greater than 0, false otherwise.
        * </summary> */
        public bool IsConscious()
        {
            return currentHP > 0;
        }

        /** <summary>
        * Heal a certain amount of HP.
        * <param>amount - amount of HP to heal</param>
        * </summary> */
        public void Heal(int amount)
        {
            currentHP += amount;
            // in case heal amount goes above or below allowable HP
            currentHP = currentHP < maxHP ? currentHP : maxHP;
        }

        public static int CapHP(int hp, int? cap = null)
        {
            int newHP = hp;
            // HP shouldn't go below 0
            newHP = newHP < 0 ? 0 : newHP;

            // HP shouldn't be above maxHP
            if (cap != null) newHP = newHP > Convert.ToInt32(cap) ? Convert.ToInt32(cap) : newHP;

            return newHP;
        }

        public int[] ScaleHits(int[] hits)
        {
            int[] newHits = new int[hits.Length];
            for (int i = 0; i < newHits.Length; i++)
            {
                newHits[i] = hits[i] * level;
            }
            return newHits;
        }
    }
}