using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dyelaga.Levels;
using Dyelaga.GameManager;

namespace Dyelaga.GameManager
{
    public class GameFinish : MonoBehaviour
    {
        public void EndGameDestroyed()
        {
            GetComponent<Score>().Died = true;
            GetComponent<SceneFlipper>().CompleteScene();
        }
        public void EndGameComplete()
        {
            GetComponent<Score>().Died = false;
            GetComponent<SceneFlipper>().CompleteScene();
        }
    }
}
