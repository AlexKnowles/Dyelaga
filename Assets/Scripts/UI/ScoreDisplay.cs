using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

namespace Dyelaga.UI
{
    public class ScoreDisplay : MonoBehaviour
    {
        TextMeshProUGUI _textObject;
        GameManager.Score _score;

        void Start()
        {
            _textObject = GetComponent<TextMeshProUGUI>();
            _score = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager.Score>();
            _textObject.text = "0";
        }

        void Update()
        {
            _textObject.text = _score.CurrentScore.ToString("G");
        }
    }
}