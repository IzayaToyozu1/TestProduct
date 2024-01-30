using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairRequestDB
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
