using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dragoni7
{
    public abstract class Condition : Node
    {
        public Condition(string name)
        {
            Name = name;
        }
    }
}
