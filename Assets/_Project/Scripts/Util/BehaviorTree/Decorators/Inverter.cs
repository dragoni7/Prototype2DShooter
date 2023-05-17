using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WUG.BehaviorTreeVisualizer;

namespace dragoni7
{
    public class Inverter : Decorator
    {
        public Inverter(string displayName, Node node) : base(displayName, node) { }

        protected override void OnReset() { }

        protected override NodeStatus OnRun(AIData aiData)
        {
            // confirm that valid child was passed in constructor
            if (ChildNodes.Count == 0 || ChildNodes[0] == null)
            {
                return NodeStatus.Failure;
            }

            // run child node
            NodeStatus originalStatus = (ChildNodes[0] as Node).Run(aiData);
            
            // check the status of the child node and invert it
            switch(originalStatus)
            {
                case NodeStatus.Failure:
                    return NodeStatus.Success;
                case NodeStatus.Success:
                    return NodeStatus.Failure;
            }

            // otherwise, it's still running
            return originalStatus;
        }
    }
}
