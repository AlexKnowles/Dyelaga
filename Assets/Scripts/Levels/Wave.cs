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

        public void SpawnEnemy()
        {
            Vector3 firstPosition = Path.GetComponent<LineRenderer>().GetPosition(0);

            firstPosition = new Vector3(firstPosition.x, firstPosition.y, 0);

            Instantiate(Enemy, firstPosition, new Quaternion(), transform);
        }
    }
}