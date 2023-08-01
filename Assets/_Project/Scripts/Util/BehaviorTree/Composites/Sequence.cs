using WUG.BehaviorTreeVisualizer;

namespace dragoni7
{
    public class Sequence : Composite
    {
        public Sequence(string displayName, params Node[] childNodes) : base(displayName, childNodes) { }

        protected override NodeStatus OnRun(AIData aiData)
        {
            // check status of last child

            NodeStatus childNodeStatus = (ChildNodes[currentChildIndex] as Node).Run(aiData);

            // evaluate the current child node. If it's failed - sequence should fail

            switch(childNodeStatus)
            {
                // child failed, return failure
                case NodeStatus.Failure:
                    return childNodeStatus;
                // if succeeded, move to next child
                case NodeStatus.Success:
                    currentChildIndex++;
                    break;
            }

            // all children run successfully, return success
            if (currentChildIndex >= ChildNodes.Count)
            {
                return NodeStatus.Success;
            }

            // the child was successful but there are still more nodes, call method again:
            return childNodeStatus == NodeStatus.Success ? OnRun(aiData) : NodeStatus.Running;
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
