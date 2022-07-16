using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dyelaga.Ship
{
    public class Shooting : MonoBehaviour
    {
        public GameObject ShootingPoint;
        public float BaseFireSpeed = 0.05f;
        public float BaseFireSpread = 0.5f;
        
        DicePool _dicePool;
        Bullets _bullets;
        List<string> _bulletsToFire;
        float _timeSinceLastFire;
        float _timeBetweenShots;
        float _shotSpread;
        int _lastShotIndex;

        void Start()
        {
            _dicePool = GameObject.FindGameObjectWithTag("GameManager").GetComponent<DicePool>();
            _bullets = GetComponent<Bullets>();
            _bulletsToFire = new List<string>();
            _timeSinceLastFire = BaseFireSpeed;
            _lastShotIndex = 0; 
        }
        
        void Update()
        {
            _bulletsToFire.Clear();

            if(_dicePool.Red.NumberOfBullets > 0){
                for(int i = 0; i < _dicePool.Red.NumberOfBullets; i++) {
                    _bulletsToFire.Add("Red");
                }
            }

            if(_dicePool.Green.NumberOfBullets > 0){
                for(int i = 0; i < _dicePool.Green.NumberOfBullets; i++) {
                    _bulletsToFire.Add("Green");
                }
            }

            if(_dicePool.Blue.NumberOfBullets > 0){
                for(int i = 0; i < _dicePool.Blue.NumberOfBullets; i++) {
                    _bulletsToFire.Add("Blue");
                }
            }
            
            _timeBetweenShots = (BaseFireSpeed + (0.15f / _bulletsToFire.Count));
        }

        void FixedUpdate()
        {
            _timeSinceLastFire += Time.fixedDeltaTime;

            if (_timeSinceLastFire >= _timeBetweenShots && _bulletsToFire.Count > 0)
            { 
                float numberOfStreams = Mathf.Floor(_bulletsToFire.Count/10);

                for(var i = 0; i <= numberOfStreams; i++) 
                { 
                    _lastShotIndex++;

                    if(_lastShotIndex >= _bulletsToFire.Count){
                        _lastShotIndex = 0;
                    } 

                    Vector2 position = new Vector2(ShootingPoint.transform.position.x - (BaseFireSpread/2) + ((BaseFireSpread/numberOfStreams)*i), ShootingPoint.transform.position.y);
                    
                    _bullets.SpawnBullet(_bulletsToFire[_lastShotIndex], position);

                    _timeSinceLastFire = 0;
                }
            }
        }
    }
}