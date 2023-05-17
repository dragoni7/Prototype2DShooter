using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace dragoni7
{
    [CustomEditor(typeof(EnemyBrain))]
    public class EnemyBrainEditor : UnityEditor.Editor
    {
        private EnemyBrain brain;

        private void OnEnable()
        {
            brain = target as EnemyBrain;
            EditorApplication.update += RedrawView;
        }

        void RedrawView()
        {
            Repaint();
        }

        public override void OnInspectorGUI()
        {
            //Makes sure the original inspector is drawn. Without this the button would replace any serialized properties
            DrawDefaultInspector();

            //Create the button
            if (GUILayout.Button("Draw Behavior Tree"))
            {
                //The method that is run if the button is pushed
                brain.ForceDrawingOfTree();
            }
        }
    }
}
