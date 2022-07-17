using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dyelaga.Enemy
{
    public class Shooting : MonoBehaviour
    {
        public float MinimumTimeBetweenShots = 0.5f;
        public float OddsOfShot = 0.2f;
        
        Bullets _bullets;
        float _timeSinceLastFire;
        float _timeBetweenShots;
        float _oddsOfShot;
        Vector2 _fireDirection;
        GameObject _player;

        void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _bullets = GetComponent<Bullets>();
            _timeSinceLastFire = 0;
            _timeBetweenShots = MinimumTimeBetweenShots;
            _oddsOfShot = OddsOfShot;
            _fireDirection = Vector2.down;
        }
        
        void Update()
        {
            // TODO: Set fire direction to player direction
            var direction = _player.transform.position - transform.position;
            if (Math.Abs(direction.x) > Math.Abs(direction.y)) {
                // x axis
                if (direction.x > 0) {
                    _fireDirection = Vector2.right;
                } else {
                    _fireDirection = Vector2.left;
                }
            } else {
                // y axis
                if (direction.y > 0) {
                    _fireDirection = Vector2.up;
                } else {
                    _fireDirection = Vector2.down;
                }
            }
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