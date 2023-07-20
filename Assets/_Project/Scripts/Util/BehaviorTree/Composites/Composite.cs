using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

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
