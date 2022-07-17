using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dyelaga.Enemy
{
    public class Health : MonoBehaviour
    {
        public int MaxHealth = 3;
        public Sprite ExplosionImage;

        [SerializeField] public GameManager.Score _score;
        string _enemyColour;
        int _currentHealth;

        private double? _disposeTime = null;
        
        void Awake()
        {
            _enemyColour = this.gameObject.name.Split("_")[1];
            _currentHealth = MaxHealth;
        }

        void Update()
        {
            if (_disposeTime != null && _disposeTime < Time.timeAsDouble) {
                Destroy(this.gameObject);
            }
        }

        public bool TakeHit(string nameOfHittingObject)
        {
            if(nameOfHittingObject.Contains(_enemyColour))
            {
                _currentHealth -= 3;
                
                if(_currentHealth <= 0)
                {
                    _score.Add(MaxHealth);
                    // delete collider
                    Destroy(GetComponent<Collider2D>());
                    // delete shooting
                    Destroy(GetComponent<Shooting>());
                    // switch sprite
                    if (ExplosionImage != null) {
                        GetComponent<SpriteRenderer>().sprite = ExplosionImage;
                    } else {
                        Destroy(GetComponent<SpriteRenderer>());
                    }
                    var fadeComponent = GetComponent<Explode>();
                    var fadeDuration = 0.3f;
                    if (fadeComponent != null) {
                        fadeComponent.Begin(fadeDuration);
                    }
                    // mark for disposal in 0.3 sec
                    _disposeTime = Time.timeAsDouble + (double)fadeDuration;
                }

                return true;
            }
            
            return false;
        }
    }    
}