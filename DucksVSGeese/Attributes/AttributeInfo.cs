using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DucksVSGeese.Ducks;
using DucksVSGeese.Geese;

namespace DucksVSGeese.Attributes
{
    /// <summary>
    /// Class with more information on attributes
    /// </summary> <summary>
    /// 
    /// </summary>
    public class AttributeInfo
    {
        /// <summary>
        /// Struct describing the Physical attribute
        /// </summary>
        public struct Physical
        {
            private const DAttribute attribute = DAttribute.Physical;
            /// <summary>
            /// Ducks and geese generally associated with this attribute
            /// </summary>
            private static HashSet<string> _members = [DuckFighter.ClassName, DuckFighterB.ClassName, GooseFighter.ClassName, GooseFighterB.ClassName];
            /// <summary>
            /// Attributes members of this type are generally vulnerable to.
            /// </summary>
            private static HashSet<DAttribute> _generalWeak = [DAttribute.Magical, DAttribute.Elemental];

            /// <summary>
            /// Attributes members of this type are generally resistant to.
            /// </summary>
            private static HashSet<DAttribute> _generalResist = [DAttribute.Physical];

            public static DAttribute Attribute
            {
                get { return attribute; }
            }
            public static HashSet<string> Members
            {
                get { return new HashSet<string>(_members); }
            }
            public static HashSet<DAttribute> GeneralWeaknesses
            {
                get { return new HashSet<DAttribute>(_generalWeak); }
            }
            public static HashSet<DAttribute> GeneralResistances
            {
                get { return new HashSet<DAttribute>(_generalResist); }
            }

        }

        /// <summary>
        /// Struct describing the Magical attribute
        /// </summary>
        public struct Magical
        {
            private const DAttribute attribute = DAttribute.Magical;
            /// <summary>
            /// Ducks and geese generally associated with this attribute
            /// </summary>
            private static HashSet<string> _members = [DuckMage.ClassName, DuckMageB.ClassName, GooseMage.ClassName, GooseMageB.ClassName];
            /// <summary>
            /// Attributes members of this type are generally vulnerable to.
            /// </summary>
            private static HashSet<DAttribute> _generalWeak = [DAttribute.Physical, DAttribute.Elemental];

            /// <summary>
            /// Attributes members of this type are generally resistant to.
            /// </summary>
            private static HashSet<DAttribute> _generalResist = [DAttribute.Magical];

            public static DAttribute Attribute
            {
                get { return attribute; }
            }
            public static HashSet<string> Members
            {
                get { return new HashSet<string>(_members); }
            }
            public static HashSet<DAttribute> GeneralWeaknesses
            {
                get { return new HashSet<DAttribute>(_generalWeak); }
            }
            public static HashSet<DAttribute> GeneralResistances
            {
                get { return new HashSet<DAttribute>(_generalResist); }
            }

        }

        /// <summary>
        /// Struct describing the Holy attribute
        /// </summary>
        public struct Holy
        {
            private const DAttribute attribute = DAttribute.Holy;
            /// <summary>
            /// Ducks and geese generally associated with this attribute
            /// </summary>
            private static HashSet<string> _members = [DuckCleric.ClassName, DuckClericB.ClassName, GooseCleric.ClassName, GooseClericB.ClassName];
            /// <summary>
            /// Attributes members of this type are generally vulnerable to.
            /// </summary>
            private static HashSet<DAttribute> _generalWeak = [DAttribute.Physical, DAttribute.Magical, DAttribute.Poison, DAttribute.Elemental];

            /// <summary>
            /// Attributes members of this type are generally resistant to (or healed by in the case of Holy).
            /// </summary>
            private static HashSet<DAttribute> _generalResist = [DAttribute.Holy, DAttribute.Cursed];

            public static DAttribute Attribute
            {
                get { return attribute; }
            }
            public static HashSet<string> Members
            {
                get { return new HashSet<string>(_members); }
            }
            public static HashSet<DAttribute> GeneralWeaknesses
            {
                get { return new HashSet<DAttribute>(_generalWeak); }
            }
            public static HashSet<DAttribute> GeneralResistances
            {
                get { return new HashSet<DAttribute>(_generalResist); }
            }

        }

        /// <summary>
        /// Struct describing the Cursed attribute.
        /// </summary>
        public struct Cursed
        {
            private const DAttribute attribute = DAttribute.Cursed;
            /// <summary>
            /// Ducks and geese generally associated with this attribute
            /// </summary>
            private static HashSet<string> _members = [DuckCursed.ClassName, DuckCursedB.ClassName, GooseCursed.ClassName, GooseCursedB.ClassName];
            /// <summary>
            /// Attributes members of this type are generally vulnerable to.
            /// </summary>
            private static HashSet<DAttribute> _generalWeak = [DAttribute.Holy, DAttribute.Elemental];

            /// <summary>
            /// Attributes members of this type are generally resistant to.
            /// </summary>
            private static HashSet<DAttribute> _generalResist = [DAttribute.Physical, DAttribute.Magical, DAttribute.Cursed, DAttribute.Poison];

            public static DAttribute Attribute
            {
                get { return attribute; }
            }
            public static HashSet<string> Members
            {
                get { return new HashSet<string>(_members); }
            }
            public static HashSet<DAttribute> GeneralWeaknesses
            {
                get { return new HashSet<DAttribute>(_generalWeak); }
            }
            public static HashSet<DAttribute> GeneralResistances
            {
                get { return new HashSet<DAttribute>(_generalResist); }
            }

        }

        /// <summary>
        /// Struct describing the Poison attribute.
        /// </summary>
        public struct Poison
        {
            private const DAttribute attribute = DAttribute.Poison;
            /// <summary>
            /// Ducks and geese generally associated with this attribute
            /// </summary>
            private static HashSet<string> _members = [DuckThief.ClassName, DuckThiefB.ClassName, GooseThief.ClassName, GooseThiefB.ClassName];
            /// <summary>
            /// Attributes members of this type are generally vulnerable to.
            /// </summary>
            private static HashSet<DAttribute> _generalWeak = [DAttribute.Elemental];

            /// <summary>
            /// Attributes members of this type are generally resistant to.
            /// </summary>
            private static HashSet<DAttribute> _generalResist = [DAttribute.Poison];

            public static DAttribute Attribute
            {
                get { return attribute; }
            }
            public static HashSet<string> Members
            {
                get { return new HashSet<string>(_members); }
            }
            public static HashSet<DAttribute> GeneralWeaknesses
            {
                get { return new HashSet<DAttribute>(_generalWeak); }
            }
            public static HashSet<DAttribute> GeneralResistances
            {
                get { return new HashSet<DAttribute>(_generalResist); }
            }
        }
        
        /// <summary>
        /// Struct describing the Elemental attribute.
        /// </summary>
        public struct Elemental
        {
            private const DAttribute attribute = DAttribute.Elemental;
            /// <summary>
            /// Ducks and geese generally associated with this attribute
            /// </summary>
            private static HashSet<string> _members = [DuckElemental.ClassName, DuckElementalB.ClassName, GooseElemental.ClassName, GooseElementalB.ClassName];
            /// <summary>
            /// Attributes members of this type are generally vulnerable to.
            /// This is empty because the 2 elemental ducks/geese are opposites in this regard (resist everything vs weak to everything).
            /// </summary>
            private static HashSet<DAttribute> _generalWeak = [];

            /// <summary>
            /// Attributes members of this type are generally resistant to.
            /// This is empty because the 2 elemental ducks/geese are opposites in this regard (resist everything vs weak to everything).
            /// </summary>
            private static HashSet<DAttribute> _generalResist = [];

            public static DAttribute Attribute
            {
                get { return attribute; }
            }
            public static HashSet<string> Members
            {
                get { return new HashSet<string>(_members); }
            }
            public static HashSet<DAttribute> GeneralWeaknesses
            {
                get { return new HashSet<DAttribute>(_generalWeak); }
            }
            public static HashSet<DAttribute> GeneralResistances
            {
                get { return new HashSet<DAttribute>(_generalResist); }
            }
        }
    }
    
}