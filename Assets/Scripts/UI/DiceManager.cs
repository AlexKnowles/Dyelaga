using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

#nullable enable
#nullable disable warnings

namespace Dyelaga {

    public class DiceManager : MonoBehaviour
    {
        public DicePool dicePool;

        public Button rerollButton;

        public GameObject dicebuttonPrefab;

        public GameObject dicePoolSlot;

        private (int, int, int) _RGBSlotNumbers = (0, 0, 0);

        private List<GameObject> _diceButtons = new List<GameObject>();

        private enum DiePosition {
            Red,
            Green,
            Blue,
            Pool
        }

        private class Die {
            public int Value;
            public DiePosition Position;
            public GameObject? GameObject;
        }

        private List<Die> _Dice = new List<Die>() {
            new Die { Value = 1, Position = DiePosition.Pool },
            new Die { Value = 1, Position = DiePosition.Pool },
            new Die { Value = 1, Position = DiePosition.Pool }
        };

        // Start is called before the first frame update
        void Start()
        {
            if (dicePool is null)
                Console.Error.WriteLine("DiceManager needs a DicePool to manage");

            if (rerollButton is null)
                Console.Error.WriteLine("DiceManager needs a rerollButton to interact");

            Button btn = rerollButton.GetComponent<Button>();
            btn.onClick.AddListener(TaskReRollDice);
            TaskReRollDice();
        }

        // Update is called once per frame
        void Update()
        {
            dicePool.Red.NumberOfBullets = _RGBSlotNumbers.Item1;
            dicePool.Green.NumberOfBullets = _RGBSlotNumbers.Item2;
            dicePool.Blue.NumberOfBullets = _RGBSlotNumbers.Item3;
        }

        void TaskReRollDice() {
            // _RGBSlotNumbers = (
            //     UnityEngine.Random.Range(1, 6),
            //     UnityEngine.Random.Range(1, 6),
            //     UnityEngine.Random.Range(1, 6)
            // );

            _Dice.ForEach(d => Destroy(d.GameObject));

            int mod = -50;
            _Dice = _Dice.Select(x => { 
                x.Value = UnityEngine.Random.Range(1, 6); 
                x.Position = DiePosition.Pool;
                x.GameObject = Instantiate (dicebuttonPrefab, new Vector3(dicePoolSlot.transform.position.x + mod ,dicePoolSlot.transform.position.y, dicePoolSlot.transform.position.z) , Quaternion.identity);
                x.GameObject.transform.SetParent(dicePoolSlot.transform);
                Button btn = x.GameObject.GetComponent<Button>();
                btn.GetComponentInChildren<TMP_Text>().text = x.Value.ToString();
                mod = mod + 50;
                return x; 
            }).ToList();
        }

        
    }

}