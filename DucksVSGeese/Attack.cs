using DucksVSGeese.Attributes;
namespace DucksVSGeese
{
    public class Attack
    {
        private readonly string _name;
        private readonly int[] _hits;
        private readonly DAttribute _attribute;
        private readonly bool _isCurse;

        public Attack(string name, int[] hits, DAttribute attribute)
        {
            _name = name;
            _hits = hits;
            _attribute = attribute;
            _isCurse = false;
        }
        public Attack(string name, int[] hits, DAttribute attribute, bool curse) : this(name, hits, attribute)
        {
            _isCurse = true; // 
        }



        public string Name
        {
            get { return _name; }
        }
        public int[] Hits
        {
            get { return _hits; }
        }
        public DAttribute DAttribute
        {
            get { return _attribute; }
        }
        public bool IsCurse
        {
            get { return _isCurse; }
        }

        public override string ToString()
        {
            string hits = "[" + string.Join(", ", _hits) + "]";
            return $"{_name} hits for {hits} points of {_attribute} damage!";
        }
    }
}