using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DucksVSGeese.groupings
{
    public struct Physical
    {
        private HashSet<Combatant> _members;
        public HashSet<Combatant> Members
        {
            get { return new HashSet<Combatant>(_members); }
        }
    }
}