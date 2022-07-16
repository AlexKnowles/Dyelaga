using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Dyelaga.Levels
{
    public class LevelManager : MonoBehaviour
    {
        public List<GameObject> Waves = new List<GameObject>();

        private List<Wave> _waveComponents;

        private List<GameObject> _instantiatedWaves = new List<GameObject>();

        private double _startTime;

        // Start is called before the first frame update
        void Start()
        {
            // This is just to be able to do it from "level start" in future;
            _startTime = Time.timeAsDouble;
            if (Waves.Count() == 0) {
                // We had no waves manually added, go get them all from the folder.
                Waves = Resources.LoadAll("Waves", typeof(GameObject)).Select(x => (GameObject)x).ToList();
            }
            Waves = Waves.OrderBy(x => x.GetComponent<Wave>().StartTimeSeconds).ToList();
            _waveComponents = Waves.Select(x => x.GetComponent<Wave>()).ToList();
        }

        // Update is called once per frame
        void Update()
        {
            // Find waves that haven't been instantiated that should have been
            var targetWaveCount = _waveComponents.Count(x => (double)x.StartTimeSeconds < (Time.timeAsDouble - _startTime));
            var currentWaveCount = _instantiatedWaves.Count();
            // instantitate themunity 
            if (targetWaveCount > currentWaveCount) {
                _instantiatedWaves.AddRange(
                    Waves.Skip(currentWaveCount)
                        .Take(targetWaveCount - currentWaveCount)
                        .Select(x => Instantiate(x, transform.position, new Quaternion(), transform)));

            }
        }
    }
}