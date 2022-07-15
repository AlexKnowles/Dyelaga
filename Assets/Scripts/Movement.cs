using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dyelaga
{
    public class Movement : MonoBehaviour
    {
        public float ThrustSpeed = 1;
        public float MaxVelocity = 1;
        public float MaxFireSpeed = 0.5f;

        Rigidbody2D _rigidbody2D;
        //BulletManager _bulletManager;

        float _verticalThrust;
        float _horizontalThrust;
        bool _playerFiring;
        float _timeSinceLastFire;

        void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            //_bulletManager = GameObject.FindGameObjectWithTag("Managers").GetComponent<BulletManager>();

            _verticalThrust = 0;
            _horizontalThrust = 0;

            _playerFiring = false;
            _timeSinceLastFire = MaxFireSpeed;
        }

        void Update()
        {
            _verticalThrust = CalculateThrust(Input.GetAxis("Vertical"));
            _horizontalThrust = CalculateThrust(Input.GetAxis("Horizontal"));

            // if(Input.GetButton("Fire"))
            // {
            //     _playerFiring = true;
            // }
        }

        void FixedUpdate()
        {
            UpdateThrust();

            //UpdateFire();
        }

        float CalculateThrust(float inputAxis)
        {
            float thrust = Mathf.Clamp(inputAxis, -1, 1);

            return (thrust * ThrustSpeed);
        }

        void UpdateThrust()
        {
            if((_horizontalThrust > 0 && _rigidbody2D.velocity.x < 0)
                || (_horizontalThrust < 0 && _rigidbody2D.velocity.x > 0))
            {
                Debug.Log("Slow boi x");
                _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
            }


            if((_verticalThrust > 0 && _rigidbody2D.velocity.y < 0)
                || (_verticalThrust < 0 && _rigidbody2D.velocity.y > 0))
            {
                Debug.Log("Slow boi y");
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);
            }


            Vector2 force = (new Vector2(_horizontalThrust, _verticalThrust));

            _rigidbody2D.AddRelativeForce(force, ForceMode2D.Impulse);
            _rigidbody2D.velocity = Vector2.ClampMagnitude(_rigidbody2D.velocity, MaxVelocity);
        }

        void UpdateFire()
        {
            _timeSinceLastFire += Time.fixedDeltaTime;

            if (_playerFiring && _timeSinceLastFire >= MaxFireSpeed)
            {
                Vector2 bulletSpawn = _rigidbody2D.position;

                Vector2 bulletOffSet = _rigidbody2D.transform.TransformPoint(new Vector2(-1, 0.46f));
                //_bulletManager.SpawnBullet("BasicBullet", _rigidbody2D, bulletOffSet);

                bulletOffSet = _rigidbody2D.transform.TransformPoint(new Vector2(1, 0.46f));
                //_bulletManager.SpawnBullet("BasicBullet", _rigidbody2D, bulletOffSet);


                Vector2 force = (new Vector2(0, -1f));

                _rigidbody2D.AddRelativeForce(force, ForceMode2D.Impulse);

                _playerFiring = false;
                _timeSinceLastFire = 0;
            }
        }
    }
}
