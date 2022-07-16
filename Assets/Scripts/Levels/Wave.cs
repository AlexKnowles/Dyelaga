using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dyelaga.Levels
{
    public class Wave : MonoBehaviour
    {
        public GameObject Path;
        public float StartTimeSeconds;
        public float OffsetY;
        public GameObject Enemy;
        public int NumberOfEnemies;
        public float ReleaseIntervalSeconds = 0.5f;   
    }
}