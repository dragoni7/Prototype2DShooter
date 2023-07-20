using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WUG.BehaviorTreeVisualizer;

namespace dragoni7
{
    public abstract class AbstractBrain : MonoBehaviour, IBehaviorTree
    {
        public NodeBase BehaviorTree { get; set; }

        [SerializeField] protected float _detectionDelay = 0.05f, _aiUpdateDelay = 0.1f;
        [SerializeField] protected AIData _aiData;
        public AIData AIData => _aiData;

        protected Coroutine _behaviourTreeRoutine;
        protected YieldInstruction _waitTime;
        protected virtual void Start()
        {
            _waitTime = new WaitForSeconds(_aiUpdateDelay);
        }
        protected abstract void GenerateBehaviourTree();
        protected IEnumerator RunBehaviourTree()
        {
            while (enabled)
            {
                if (BehaviorTree == null)
                {
                    $"{this.GetType().Name} is missing Behavior Tree. Did you set the BehaviorTree property?".BTDebugLog();
                    continue;
                }

                (BehaviorTree as Node).Run(_aiData);
                yield return _waitTime;
            }
        }

        private void OnDestroy()
        {
            if (_behaviourTreeRoutine != null)
            {
                StopCoroutine(_behaviourTreeRoutine);
            }
        }

        public void ForceDrawingOfTree()
        {
            if (BehaviorTree == null)
            {
                $"Behavior tree is null - nothing to draw.".BTDebugLog();
            }
            //Tell the tool to draw the referenced behavior tree. The 'true' parameter tells it to give focus to the window. 
            BehaviorTreeGraphWindow.DrawBehaviorTree(BehaviorTree, true);
        }
    }
}
