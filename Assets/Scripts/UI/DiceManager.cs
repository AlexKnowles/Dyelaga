using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

#nullable enable
#nullable disable warnings

namespace Dyelaga.GameManager {

    public class DiceManager : MonoBehaviour
    {
        public DicePool dicePool;

        public Button rerollButton;

        public GameObject dicebuttonPrefab;

        public GameObject dicePoolSlot;

        public List<Sprite> diceImages;


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


        void TaskDieDroppedOn((GameObject, GameObject) slotAndDie) {
            var slot = slotAndDie.Item1;
            var die = slotAndDie.Item2;

            var dieIndex = _Dice.FindIndex(x => GameObject.ReferenceEquals(x.GameObject, die) && x.Position == DiePosition.Pool);
            if (dieIndex != -1) {
                // Mimicing click here so we can keep click for accessibility
                TaskClickedIndex(dieIndex);
                // This is f*cking hideous
                // I did try object equality
                // It didn't like it
                // Tried reference equals
                // Didn't like it
                // Tried instance ID
                // Didn't like it
                // So you get names...
                if (slot.name == redButton.name ) {
                    TaskClickedSlot(DiePosition.Red);
                } else if (slot.name == greenButton.name) {
                    TaskClickedSlot(DiePosition.Green);
                } else if (slot.name == blueButton.name) {
                    TaskClickedSlot(DiePosition.Blue);
                }
            }
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

            int xColumn = -125;
            int jiggle = 30;
            _Dice = _Dice.Select((x, i) => {
                int xFuzz = UnityEngine.Random.Range(-jiggle, jiggle);
                int yFuzz = UnityEngine.Random.Range(-jiggle, jiggle);
                int rotation = UnityEngine.Random.Range(-90, 90);
                x.Value = UnityEngine.Random.Range(1, 7); 
                x.Position = DiePosition.Pool;
                x.GameObject = Instantiate (dicebuttonPrefab, 
                    new Vector2(
                        dicePoolSlot.transform.position.x + xColumn + xFuzz,
                        dicePoolSlot.transform.position.y + yFuzz //,
                        //dicePoolSlot.transform.position.z
                    ) , Quaternion.identity * Quaternion.Euler(0, 0, rotation));
                x.GameObject.transform.SetParent(dicePoolSlot.transform);
                Button btn = x.GameObject.GetComponent<Button>();
                x.GameObject.GetComponent<Image>().sprite = diceImages[x.Value -1];
                btn.onClick.AddListener(() => TaskClickedIndex(i));
                xColumn = xColumn + 125;
                return x; 
            }).ToList();
            UpdateDiceNumbers();
        }

        
    }

}