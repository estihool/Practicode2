using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTMLSerializer
{
    public class Attribute
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public Attribute(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public override string ToString()
        {
            return string.Format("{0}=\"{1}\"", Name, Value);
        }
    }
}
