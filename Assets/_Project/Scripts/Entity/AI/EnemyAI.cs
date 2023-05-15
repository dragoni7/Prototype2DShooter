using System.Collections.Generic;
using UnityEngine;

namespace dragoni7
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private List<AbstractDetector> detectors;
        [SerializeField] private AIData aiData;
        [SerializeField] private float detectionDelay = 0.05f;

        private void Start()
        {
            // Detect Player and Obstacles around
            InvokeRepeating("PerformDetection", 0, detectionDelay);
        }

        private void PerformDetection()
        {
            foreach (AbstractDetector detector in detectors)
            {
                detector.Detect(aiData);
            }
        }
    }
}
