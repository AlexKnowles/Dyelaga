using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dyelaga.GameManager
{
    public class Score : MonoBehaviour
    {
        public int CurrentScore;
        public bool Died = false;

        void Awake()
        {
            CurrentScore = 0;
        }

        public void Add(int value)
        {
            CurrentScore += value;
        }
    }
}