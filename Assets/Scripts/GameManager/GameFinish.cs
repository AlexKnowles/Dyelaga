using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dyelaga.Levels;

namespace Dyelaga
{
    public class GameFinish : MonoBehaviour
    {
        public void EndGameDestroyed()
        {
            GetComponent<SceneFlipper>().DeathScene();
        }
    }
}
