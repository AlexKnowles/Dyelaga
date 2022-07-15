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


        public TMP_Text redLabel;
        public TMP_Text blueLabel;
        public TMP_Text greenLabel;

        public Button redButton;
        public Button blueButton;
        public Button greenButton;


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

        private int _clickedIndex = -1;

        // Start is called before the first frame update
        void Start()
        {
            if (dicePool is null)
                Console.Error.WriteLine("DiceManager needs a DicePool to manage");

            if (rerollButton is null)
                Console.Error.WriteLine("DiceManager needs a rerollButton to interact");

            if (redButton is null)
                Console.Error.WriteLine("DiceManager needs a redButton to interact");

            if (greenButton is null)
                Console.Error.WriteLine("DiceManager needs a greenButton to interact");

            if (blueButton is null)
                Console.Error.WriteLine("DiceManager needs a blueButton to interact");

            if (redLabel is null)
                Console.Error.WriteLine("DiceManager needs a redLabel to interact");

            if (greenLabel is null)
                Console.Error.WriteLine("DiceManager needs a greenLabel to interact");
                
            if (blueLabel is null)
                Console.Error.WriteLine("DiceManager needs a blueLabel to interact");

            Button btn = rerollButton.GetComponent<Button>();
            btn.onClick.AddListener(TaskReRollDice);

            redButton.onClick.AddListener(() => TaskClickedSlot(DiePosition.Red));
            greenButton.onClick.AddListener(() => TaskClickedSlot(DiePosition.Green));
            blueButton.onClick.AddListener(() => TaskClickedSlot(DiePosition.Blue));
            TaskReRollDice();
        }

        void UpdateDiceNumbers() 
        {
            dicePool.Red.NumberOfBullets = _Dice.Where(x => x.Position == DiePosition.Red).Select(x => x.Value).Sum();
            dicePool.Green.NumberOfBullets = _Dice.Where(x => x.Position == DiePosition.Green).Select(x => x.Value).Sum();
            dicePool.Blue.NumberOfBullets = _Dice.Where(x => x.Position == DiePosition.Blue).Select(x => x.Value).Sum();

            redLabel.text = dicePool.Red.NumberOfBullets.ToString();
            blueLabel.text = dicePool.Blue.NumberOfBullets.ToString();
            greenLabel.text = dicePool.Green.NumberOfBullets.ToString();
        }

        // Update is called once per frame
        void Update()
        {
        }

        void TaskClickedIndex(int index) {
            _clickedIndex = index;
        }

        void TaskClickedSlot(DiePosition position) {
            if (_clickedIndex != -1) {
                _Dice[_clickedIndex].GameObject.SetActive(false);
                _Dice[_clickedIndex].Position = position;
                _clickedIndex = -1;
                UpdateDiceNumbers();
            }
        }

        void TaskReRollDice() {
            _clickedIndex = -1;
            _Dice.ForEach(d => Destroy(d.GameObject));

            int mod = -50;
            _Dice = _Dice.Select((x, i) => { 
                x.Value = UnityEngine.Random.Range(1, 6); 
                x.Position = DiePosition.Pool;
                x.GameObject = Instantiate (dicebuttonPrefab, new Vector3(dicePoolSlot.transform.position.x + mod ,dicePoolSlot.transform.position.y, dicePoolSlot.transform.position.z) , Quaternion.identity);
                x.GameObject.transform.SetParent(dicePoolSlot.transform);
                Button btn = x.GameObject.GetComponent<Button>();
                btn.GetComponentInChildren<TMP_Text>().text = x.Value.ToString();
                btn.onClick.AddListener(() => TaskClickedIndex(i));
                mod = mod + 50;
                return x; 
            }).ToList();
            UpdateDiceNumbers();
        }

        
    }

}