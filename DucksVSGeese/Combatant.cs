using System.ComponentModel;
using System.Runtime.ConstrainedExecution;

namespace DucksVSGeese
{
    public abstract class Combatant
    {
        private readonly string _combatClass;
        private readonly string _name;
        private readonly int _maxHP;
        private int _currentHP;
        /// <summary>
        /// whether this Combatant attacks allies or not
        /// </summary>
        private bool _attackAllies;

        /// <summary>
        /// whether this Combatant attacks allies or not
        /// </summary>
        private int _cursedTimer;
        /// <summary>
        /// Default amount of time a curse lasts for.
        /// </summary>
        private const int CurseTime = 5;

        /// <summary>
        /// For scaling at some point.
        /// </summary>
        private int _level;
        public const int LevelCap = 1000;
        public Combatant(string combatClass, string name, int maxHP, bool attackAllies)
        {
            _combatClass = combatClass;
            _attackAllies = attackAllies;
            _cursedTimer = 0;
            _level = 1;
            _name = name;
            _maxHP = maxHP * _level;
            _currentHP = _maxHP;
        }
        /**<summary>
        Name for a class of duck or goose.
        </summary>*/
        public string CombatClass
        {
            get { return _combatClass; }
        }
        public string Name
        {
            get { return _name; }
        }
        public int CurrentHP
        {
            get { return _currentHP; }
        }

        public int MaxHP
        {
            get { return _maxHP; }
        }

        public bool AttackAllies
        {
            get { return _attackAllies; }
        }

        public int CursedTimer
        {
            get { return _cursedTimer; }
        }

        public int Level
        {
            get { return _level; }
            set
            {
                _level = value;
                // level is capped at the level cap or 0
                _level = _level > LevelCap ? LevelCap : _level < 0 ? 0 : _level;
            }
        }


        public string GetHPString()
        {
            return $"{_currentHP}/{_maxHP}";
        }

        public string GetTitle()
        {
            return $"{_combatClass} {_name}";
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
            return _currentHP > 0;
        }

        /** <summary>
        * Heal a certain amount of HP.
        * <param name="amount"=> amount of HP to heal</param>
        * </summary> */
        public void Heal(int amount)
        {
            _currentHP += amount;
            // in case heal amount goes above or below allowable HP
            _currentHP = _currentHP < _maxHP ? _currentHP : _maxHP;
        }

        /** <summary>
        * Heal all of a combatant's HP
        * </summary> */
        public void HealAll()
        {
            _currentHP = _maxHP;
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
                newHits[i] = hits[i] * _level;
            }
            return newHits;
        }

        public void Curse()
        {
            // attack the opposite side until the curse timer ends
            _attackAllies = !_attackAllies;
            _cursedTimer = CurseTime;
        }
        public void DecrementCurse()
        {
            if (_cursedTimer > 0)
            {
                _cursedTimer--;
                if (_cursedTimer == 0)
                {
                    _attackAllies = !_attackAllies; // go back to normal
                    Console.WriteLine($"{_name} is no longer cursed!");
                }
            }
        }

        protected int GetHit(int[] hits, double modifier)
        {
            int totalDamage = 0;
            foreach (int hit in hits)
            {
                int damage = Convert.ToInt32(hit * modifier);
                totalDamage += damage;
                _currentHP -= damage;
                _currentHP = Combatant.CapHP(_currentHP, _maxHP);
            }
            return totalDamage;
        }

        public abstract void EndTurn();
    }
}