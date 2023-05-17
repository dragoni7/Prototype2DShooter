namespace dragoni7
{
    public class EnemyBrain : AbstractBrain
    {
        protected override void Start()
        {
            GenerateBehaviourTree();

            if (_behaviourTreeRoutine == null && BehaviorTree != null)
            {
                _behaviourTreeRoutine = StartCoroutine(RunBehaviourTree());
            }

            InvokeRepeating("PerformDetection", 0, _detectionDelay);
        }
        private void PerformDetection()
        {
            foreach (AbstractDetector detector in _detectors)
            {
                detector.Detect(_aiData);
            }
        }

        protected override void GenerateBehaviourTree()
        {
            BehaviorTree = new Sequence("Chase Player",
                new HasTarget("Has Target?"),
                new Inverter("Inverter", new IsFollowingTarget("Is Following a Target?")),
                new ChaseTarget()
                );
        }
    }
}
