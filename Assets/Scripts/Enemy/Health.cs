using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dyelaga.Enemy
{
    public class Health : MonoBehaviour
    {
        public int MaxHealth = 3;

        GameManager.Score _score;
        string _enemyColour;
        int _currentHealth;
        
        void Awake()
        {
            _score = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager.Score>();
            _enemyColour = this.gameObject.name.Split("_")[1];
            _currentHealth = MaxHealth;
        }

        public void TakeHit(string nameOfHittingObject)
        {
            if(nameOfHittingObject.Contains(_enemyColour))
            {
                _currentHealth -= 3;
            }
            else
            {
                _currentHealth--;
            }

            if(_currentHealth <= 0)
            {
                _score.Add(MaxHealth);
                Destroy(this.gameObject);
            }
        }
    }    
}