using UnityEngine;
using System.Collections;
using UnityEditor;

namespace Dyelaga.Levels
{
    [CustomEditor(typeof(Wave))]
    public class WaveEditor : Editor 
    {

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            Wave wave = (Wave)target;
            if(GUILayout.Button("Spawn Enemy"))
            {
                wave.SpawnEnemy();                
            }
        }

    }
}