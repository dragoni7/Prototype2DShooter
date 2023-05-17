using WUG.BehaviorTreeVisualizer;

namespace dragoni7
{
    public abstract class Node : NodeBase
    {
        // number of times node has been evaluated in a single run
        public int evaluationCount;

        private string _LastStatusReason { get; set; } = "";

        // runs logic for the node
        public virtual NodeStatus Run(AIData aiData)
        {
            // run custom logic
            NodeStatus nodeStatus = OnRun(aiData);

            if (LastNodeStatus != nodeStatus || !_LastStatusReason.Equals(StatusReason))
            {
                LastNodeStatus = nodeStatus;
                _LastStatusReason = StatusReason;
                OnNodeStatusChanged(this);
            }

            evaluationCount++;

            // if node status is not running, then it is success or failure and can be reset
            if (nodeStatus != NodeStatus.Running)
            {
                Reset();
            }

            return nodeStatus;
        }

        public void Reset()
        {
            evaluationCount = 0;
            OnReset();
        }

        protected abstract NodeStatus OnRun(AIData aiData);
        protected abstract void OnReset();
    }
}
