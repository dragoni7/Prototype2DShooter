using System.Linq;

namespace dragoni7
{
    public abstract class Composite : Node
    {
        protected int currentChildIndex = 0;

        protected Composite(string displayName, params Node[] childNodes)
        {
            Name = displayName;
            ChildNodes.AddRange(childNodes.ToList());
        }
    }
}
