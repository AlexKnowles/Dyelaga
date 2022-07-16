using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Dyelaga.Levels
{
    public class Wave : MonoBehaviour
    {
        public GameObject Path;
        public float StartTimeSeconds;
        public float OffsetY;
        public GameObject Enemy;
        public int NumberOfEnemies;
        public float ReleaseIntervalSeconds = 2f;

        private int _NumberOfEnemiesSpawned = 0;

        private double _startTime = 0;

        private GameObject _pathInstance;

        void Start() {
            // Doing this so we can have an abstract startime
            _startTime = Time.timeAsDouble;
            _pathInstance = Instantiate(Path, transform.position, new Quaternion(), transform);
            Vector3[] lrPos = new Vector3[_pathInstance.GetComponent<LineRenderer>().positionCount];
            _pathInstance.GetComponent<LineRenderer>().GetPositions(lrPos);
            lrPos = lrPos.Select(vec => {
                vec.y = vec.y + OffsetY;
                return vec;
            }).ToArray();
            _pathInstance.GetComponent<LineRenderer>().SetPositions(lrPos);
        }

        void Update() {
            if (_NumberOfEnemiesSpawned < NumberOfEnemies &&
                Time.timeAsDouble > (double)(_startTime + (_NumberOfEnemiesSpawned * ReleaseIntervalSeconds))) {
                _NumberOfEnemiesSpawned++;
                SpawnEnemy();
            }
        }

        public void SpawnEnemy()
        {
            LineRenderer pathLineRenderer = _pathInstance.GetComponent<LineRenderer>();
            Vector3 firstPosition = pathLineRenderer.GetPosition(0);
            Vector3 nextPosition = pathLineRenderer.GetPosition(1);

            firstPosition = new Vector3(firstPosition.x, firstPosition.y, 0);

            GameObject enemy = Instantiate(Enemy, firstPosition, Quaternion.LookRotation(nextPosition), transform);

            Vector3[] positions = new Vector3[pathLineRenderer.positionCount];
            pathLineRenderer.GetPositions(positions);
            enemy.GetComponent<Enemy.Movement>().SetPath(positions);
            
            // Selection.activeGameObject = enemy;
        }
    }
}