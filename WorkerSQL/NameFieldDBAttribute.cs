using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerSQL
{
    public class NameFieldDBAttribute: Attribute
    {
        public string Name { get; }

        public NameFieldDBAttribute(string name)
        {
            Name = name;
        }
    }
}
