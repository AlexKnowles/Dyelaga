using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dyelaga.Ship
{
    public class Shooting : MonoBehaviour
    {
        public GameObject ShootingPoint;
        public float MaxFireSpeed = 0.5f;
        
        DicePool _dicePool;
        Bullets _bullets;
        float _timeSinceLastFire;

        void Start()
        {
            _dicePool = GameObject.FindGameObjectWithTag("GameManager").GetComponent<DicePool>();
            _bullets = GetComponent<Bullets>();
            _timeSinceLastFire = MaxFireSpeed;            
        }
        
        void Update()
        {
            // Choose bullet
        }

        void FixedUpdate()
        {
            UpdateFire();
        }

        void UpdateFire()
        {
            _timeSinceLastFire += Time.fixedDeltaTime;

            if (_timeSinceLastFire >= MaxFireSpeed)
            {
                if(_dicePool.Red.NumberOfBullets > 0)
                {
                    _bullets.SpawnBullet("Red", ShootingPoint.transform.position);
                }

                _timeSinceLastFire = 0;
            }
        }
    }
}