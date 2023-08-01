using WUG.BehaviorTreeVisualizer;

namespace dragoni7
{
    public class Selector : Composite
    {
        public Selector(string displayName, params Node[] childNodes) : base(displayName, childNodes) { }

        protected override NodeStatus OnRun(AIData aiData)
        {
            // we've reached the end of the ChildNodes and none were successful
            if (currentChildIndex >= ChildNodes.Count)
            {
                return NodeStatus.Failure;
            }

            // call current child
            NodeStatus nodeStatus = (ChildNodes[currentChildIndex] as Node).Run(aiData);


            // check the child's status, failure means try new child, success means done
            switch(nodeStatus)
            {
                case NodeStatus.Failure:
                    currentChildIndex++;
                    break;
                case NodeStatus.Success:
                    return NodeStatus.Success;
            }

            // if this point as been hit, current child is still running
            return NodeStatus.Running;
        }

        protected override void OnReset()
        {
            currentChildIndex = 0;

            for (int i = 0; i < ChildNodes.Count; i++)
            {
                (ChildNodes[i] as Node).Reset();
            }
        }
    }
}
