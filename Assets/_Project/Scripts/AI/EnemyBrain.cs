using System.Collections.Generic;

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
        }
        protected override void GenerateBehaviourTree()
        {
            ObstacleAvoidanceBehaviour obstacleAvoidanceBehaviour = new();
            SeekBehaviour seekBehaviour = new();

            BehaviorTree =
                new Sequence("Get Obstacles",
                    new DetectObstaclesTask(5f),
                    new Selector("Determine Navigation",
                        new Sequence("Pursue Player",
                            new HasPlayerInRange(20f),
                            new HasTarget(),
                            new Selector("Attack or Pursue",
                                new Sequence("Attack",
                                    new IsTargetWithinRange(10),
                                    new Timer(0.1f, new AttackTask(), true)),
                                new PursueTargetTask(new List<AbstractSteeringBehaviour> { obstacleAvoidanceBehaviour, seekBehaviour }))),
                        new Sequence("Wander",
                            new HasTarget(),
                            //new WanderTask(),
                            new PursueTargetTask(new List<AbstractSteeringBehaviour> { obstacleAvoidanceBehaviour, seekBehaviour })))
                    );
        }
    }
}
