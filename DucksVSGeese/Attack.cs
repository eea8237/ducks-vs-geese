namespace DucksVSGeese
{
    public class Attack
    {
        private readonly string _name;
        private readonly int[] _hits;
        private readonly Attribute _attribute;

        public Attack(string name, int[] hits, Attribute attribute)
        {
            _name = name;
            _hits = hits;
            _attribute = attribute;
        }

        public string Name
        {
            get { return _name; }
        }
        public int[] Hits
        { 
            get { return _hits; }
        }
        public Attribute Attribute
        { 
            get { return _attribute; }
        }

        public override string ToString()
        {
            string hits = "[" + string.Join(", ", _hits) + "]";
            return $"{_name} hits for {hits} points of {_attribute} damage!";
        }
    }
}