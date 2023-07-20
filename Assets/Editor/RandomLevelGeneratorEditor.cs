using UnityEditor;
using UnityEngine;

namespace dragoni7
{
    [CustomEditor(typeof(AbstractLevelGenerator), true)]
    public class RandomLevelGeneratorEditor : Editor
    {
        AbstractLevelGenerator generator;

        private void Awake()
        {
            generator = (AbstractLevelGenerator)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Create Level"))
            {
                generator.GenerateLevel();
            }
        }
    }
}
