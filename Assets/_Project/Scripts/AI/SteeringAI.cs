using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dragoni7
{
    public class SteeringAI : MonoBehaviour
    {
        [SerializeField] private List<AbstractDetector> _detectors;
        [SerializeField] private List<AbstractSteeringBehaviour> _behaviours;
        [SerializeField] private AIData _aiData;
        [SerializeField] private float _detectionDelay = 0.05f, _aiUpdateDelay = 0.06f, _attackDelay = 1f;
        [SerializeField] private Vector2 _movementInput;
        [SerializeField] private SteeringContextSolver _movementDirectionSolver;
        [SerializeField] private float _attackDistance = 0.5f;

        private bool following = false;


        // inputs sent from the Enemy AI to the Enemy controller
        public Action<Vector3> OnAttack;
        public Action<Vector3> OnMove, OnPointerInput;

        private void Start()
        {
            // detect Player and Obstacles around
            InvokeRepeating("PerformDetection", 0, _detectionDelay);
        }

        private void PerformDetection()
        {
            foreach (AbstractDetector detector in _detectors)
            {
                detector.Detect(_aiData);
            }
        }

        private void Update()
        {
            // enemy AI movement based on target availability
            if (_aiData.currentTarget != null)
            {
                // looking at target
                OnPointerInput?.Invoke(_aiData.currentTarget.position);
                if (following == false)
                {
                    following = true;
                    StartCoroutine(ChaseAndAttack());
                }
            }
            else if (_aiData.GetTargetsCount() > 0)
            {
                // target acquisition logic
                _aiData.currentTarget = _aiData.targets[0];
            }

            // moving the subject
            OnMove?.Invoke(_movementInput);
        }

        private IEnumerator ChaseAndAttack()
        {
            if (_aiData.currentTarget == null)
            {
                // stop logic
                _movementInput = Vector2.zero;
                following = false;
                yield return null;
            }
            else
            {
                float distance = Vector2.Distance(_aiData.currentTarget.position, transform.position);

                if (distance < _attackDistance)
                {
                    // attack logic
                    _movementInput = Vector2.zero;

                    if (_aiData.attackDirection != Vector3.zero)
                    {
                        OnAttack?.Invoke(_aiData.attackDirection);
                    }
                    yield return new WaitForSeconds(_attackDelay);
                    StartCoroutine(ChaseAndAttack());
                }
                else
                {
                    // chase logic
                    _movementInput = _movementDirectionSolver.GetDirectionToMove(_behaviours, _aiData);
                    yield return new WaitForSeconds(_aiUpdateDelay);
                    StartCoroutine(ChaseAndAttack());
                }
            }
        }
    }
}
