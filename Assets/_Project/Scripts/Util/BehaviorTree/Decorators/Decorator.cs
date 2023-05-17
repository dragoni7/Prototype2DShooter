using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dragoni7
{
    public abstract class Decorator : Node
    {
        public Decorator(string displayName, Node node)
        {
            Name = displayName;
            ChildNodes.Add(node);
        }
    }
}
