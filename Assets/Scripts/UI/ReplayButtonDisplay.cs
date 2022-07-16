using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

namespace Dyelaga.UI
{
    public class ReplayButtonDisplay : MonoBehaviour
    {
        TextMeshProUGUI _textObject;
        [SerializeField] public GameManager.Score _score;

        void Start()
        {
            _textObject = GetComponent<TextMeshProUGUI>();
            _textObject.text = "Play Again";
        }

        void Update()
        {
            _textObject.text = _score.Died ? "Try Again" : "Play Again";
        }
    }
}