using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Dyelaga {

    public class DiceManager : MonoBehaviour
    {
        public DicePool dicePool;

        public Button rerollButton;

        private (int, int, int) _RGBDiceNumbers;

        // Start is called before the first frame update
        void Start()
        {
            if (dicePool is null)
                Console.Error.WriteLine("DiceManager needs a DicePool to manage");

            if (rerollButton is null)
                Console.Error.WriteLine("DiceManager needs a rerollButton to interact");

            Button btn = rerollButton.GetComponent<Button>();
            btn.onClick.AddListener(TaskGenerateRandomNumbers);
            TaskGenerateRandomNumbers();
        }

        // Update is called once per frame
        void Update()
        {
            dicePool.Red.NumberOfBullets = _RGBDiceNumbers.Item1;
            dicePool.Green.NumberOfBullets = _RGBDiceNumbers.Item2;
            dicePool.Blue.NumberOfBullets = _RGBDiceNumbers.Item3;
        }

        void TaskGenerateRandomNumbers() {
            _RGBDiceNumbers = (
                UnityEngine.Random.Range(1, 6),
                UnityEngine.Random.Range(1, 6),
                UnityEngine.Random.Range(1, 6)
            );
        }
    }

}