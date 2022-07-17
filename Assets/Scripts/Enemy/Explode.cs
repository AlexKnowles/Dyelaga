using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dyelaga.Enemy
{
    public class Explode : MonoBehaviour
    {
        SpriteRenderer _spriteRenderer;
        bool _doFade;
        float _startTime;
        float _fadeTime;

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

            float t = (Time.time - _startTime) / _fadeTime;
            var step = Mathf.SmoothStep(1, 0, t);
            var scaleStep = step/2;
            _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, step);
            gameObject.transform.localScale = new Vector3(0.5f - scaleStep, 0.5f - scaleStep, 0.5f - scaleStep);     
        }

        public void Begin(float fadeTime)
        {
            _fadeTime = fadeTime;
            _startTime = Time.time;
            _doFade = true;
            gameObject.transform.localScale = new Vector3(0, 0, 0);
        }
    }
}