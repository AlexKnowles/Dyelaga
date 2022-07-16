using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization;


namespace Dyelaga.GameManager
{
    public class DicePool : MonoBehaviour
    {
        public ColourDice Red;
        public ColourDice Blue;
        public ColourDice Green;
    }


    [Serializable]
    public class ColourDice {
        [Range(0, 10)]
        public int NumberOfBullets;
    }

}