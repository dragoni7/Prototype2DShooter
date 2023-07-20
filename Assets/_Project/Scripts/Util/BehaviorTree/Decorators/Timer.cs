using UnityEngine;
using WUG.BehaviorTreeVisualizer;

namespace dragoni7
{
    public class Timer : Decorator
    {
        private float _startTime;
        private bool _useFixedTime;
        private float _timeToWait;
        public Timer(float timeToWait, Node childNode, bool useFixedTime = false) : base($"Timer for {timeToWait}", childNode)
        {
            _useFixedTime = useFixedTime;
            _timeToWait = timeToWait;
        }

        protected override void OnReset()
        {

        }

        protected override NodeStatus OnRun(AIData aiData)
        {
            // confirm valid child node was passed in constructor
            if (ChildNodes.Count == 0 || ChildNodes[0] == null)
            {
                return NodeStatus.Failure;
            }

            // run the child node and calculate the elapsed
            NodeStatus originalStatus = (ChildNodes[0] as Node).Run(aiData);

            // if this is the first eval, then the start time needs to be set up.
            if (evaluationCount == 0)
            {
                StatusReason = $"Starting timer for {_timeToWait}.Child node status is: { originalStatus}";
                _startTime = _useFixedTime ? Time.fixedTime : Time.time;
            }

            // calculate time passed
            float elapsedTime = Time.fixedTime - _startTime;

            // if more time passed than we wanted, stop
            if (elapsedTime > _timeToWait)
            {
                StatusReason = $"Timer complete - Child node status is: {originalStatus}";
                return NodeStatus.Success;
            }

            // otherwise keep running
            StatusReason = $"Timer is {elapsedTime} out of {_timeToWait}.Child node status is: { originalStatus}";
            return NodeStatus.Running;
        }
    }
}
