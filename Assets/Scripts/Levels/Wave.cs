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

        private List<GameObject> _instantiatedEnemies = new List<GameObject>();

        private bool _waveComplete = false;

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

            if (!_waveComplete && _NumberOfEnemiesSpawned == NumberOfEnemies && 
                _instantiatedEnemies.All(go => go == null))  {
                _waveComplete = true;
            }
        }

        public void SpawnEnemy()
        {
            LineRenderer pathLineRenderer = _pathInstance.GetComponent<LineRenderer>();
            Vector3 firstPosition = pathLineRenderer.GetPosition(0);

            firstPosition = new Vector3(firstPosition.x, firstPosition.y, 0);

            GameObject enemy = Instantiate(Enemy, firstPosition, new Quaternion(), transform);

            Vector3[] positions = new Vector3[pathLineRenderer.positionCount];
            pathLineRenderer.GetPositions(positions);
            enemy.GetComponent<Enemy.Movement>().SetPath(positions);
            
            _instantiatedEnemies.Add(enemy);
            // Selection.activeGameObject = enemy;
        }

        public bool GetWaveComplete() {
            return _waveComplete;
        }
    }
}