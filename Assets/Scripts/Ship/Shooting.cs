using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dyelaga.Ship
{
    public class Shooting : MonoBehaviour
    {
        public GameObject ShootingPoint;
        public float BaseFireSpeed = 0.05f;
        public float FireStreamSpacing = 0.5f;
        public int NumberOfBulletsToSplit = 6;
        
        DicePool _dicePool;
        Bullets _bullets;
        List<string> _bulletsToFire;
        float _timeSinceLastFire;
        float _timeBetweenShots;
        float _shotSpread;
        int _lastShotIndex;
        float _numberOfStreams;
        float _fullWidthOfStreams;

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
            
            _numberOfStreams = Mathf.Floor(_bulletsToFire.Count/NumberOfBulletsToSplit);
            _timeBetweenShots = (BaseFireSpeed + ((0.15f / (_bulletsToFire.Count / (_numberOfStreams + 1)))));
            _fullWidthOfStreams = (_numberOfStreams * FireStreamSpacing);
        }

        void FixedUpdate()
        {
            _timeSinceLastFire += Time.fixedDeltaTime;

            if (_timeSinceLastFire >= _timeBetweenShots && _bulletsToFire.Count > 0)
            { 
                for(var i = 0; i <= _numberOfStreams; i++) 
                { 
                    _lastShotIndex++;

                    if(_lastShotIndex >= _bulletsToFire.Count){
                        _lastShotIndex = 0;
                    } 

                    Vector3 position = new Vector3(ShootingPoint.transform.position.x - (_fullWidthOfStreams / 2) + (FireStreamSpacing * i) , ShootingPoint.transform.position.y, ShootingPoint.transform.position.z);
                    
                    _bullets.SpawnBullet(_bulletsToFire[_lastShotIndex], position);

                    _timeSinceLastFire = 0;
                }
            }
        }
    }
}