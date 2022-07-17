using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dyelaga.Enemy
{
    public class Shooting : MonoBehaviour
    {
        public float MinimumTimeBetweenShots = 0.5f;
        public float OddsOfShot = 0.5f;
        
        Bullets _bullets;
        float _timeSinceLastFire;
        float _timeBetweenShots;
        float _oddsOfShot;
        BulletDirection _fireDirection;
        GameObject _player;

        void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _bullets = GetComponent<Bullets>();
            _timeSinceLastFire = 0;
            _timeBetweenShots = MinimumTimeBetweenShots;
            _oddsOfShot = OddsOfShot;
            _fireDirection = BulletDirection.Down;
        }
        
        void Update()
        {
            // TODO: Set fire direction to player direction
        }

        void FixedUpdate()
        {

            _timeSinceLastFire += Time.fixedDeltaTime;
            if (_timeSinceLastFire >= _timeBetweenShots)
            { 
                if (UnityEngine.Random.Range(0f, 1f) < _oddsOfShot) {
                    _bullets.SpawnBullet(_fireDirection, transform.position);
                }
                _timeSinceLastFire = 0;
            }
        }
    }
}