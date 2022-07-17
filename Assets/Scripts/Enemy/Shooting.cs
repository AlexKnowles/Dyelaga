using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dyelaga.Enemy
{
    public class Shooting : MonoBehaviour
    {
        public float BaseFireSpeed = 0.05f;
        public float BaseTimeBetweenShots = 0.5f;
        
        Bullets _bullets;
        float _timeSinceLastFire;
        float _timeBetweenShots;
        BulletDirection _fireDirection;

        void Start()
        {
            _bullets = GetComponent<Bullets>();
            _timeSinceLastFire = BaseFireSpeed;
            _timeBetweenShots = BaseTimeBetweenShots;
            _fireDirection = BulletDirection.Down;
        }
        
        void Update()
        {
                // TODO: Figure out where the player is for position

                // TODO: Make a random decision on whether to fire (will replace timing logic)
        }

        void FixedUpdate()
        {

            _timeSinceLastFire += Time.fixedDeltaTime;
            if (_timeSinceLastFire >= _timeBetweenShots)
            { 
                _bullets.SpawnBullet(_fireDirection, transform.position);
                _timeSinceLastFire = 0;
            }
        }
    }
}