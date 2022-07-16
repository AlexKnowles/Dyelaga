using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dyelaga.Levels;
using Dyelaga.GameManager;

namespace Dyelaga.GameManager
{
    public class GameFinish : MonoBehaviour
    {
        public Score _score;
        public void EndGameDestroyed()
        {
            _score.Died = true;
            GetComponent<SceneFlipper>().CompleteScene();
        }
        public void EndGameComplete()
        {
            _score.Died = false;
            GetComponent<SceneFlipper>().CompleteScene();
        }
    }
}
