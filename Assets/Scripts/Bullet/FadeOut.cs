using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dyelaga.Bullet
{
    public class FadeOut : MonoBehaviour
    {
        public float FadeOutSpeedInSeconds = 1;

        SpriteRenderer _spriteRenderer;
        bool _doFade;
        float _startTime;

        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _doFade = false;
        }

        void Update()
        {
            if(!_doFade)
            {
                return;
            }

            float t = (Time.time - _startTime) / FadeOutSpeedInSeconds;
            _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, Mathf.SmoothStep(1, 0, t));     
        }

        public void Begin()
        {
            _startTime = Time.time;
            _doFade = true;
        }
    }
}