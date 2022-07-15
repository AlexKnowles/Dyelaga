using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dyelaga.Ship
{
    public class Movement : MonoBehaviour
    {
        public float ThrustSpeed = 1;
        public float MaxVelocity = 1;

        Rigidbody2D _rigidbody2D;

        float _verticalThrust;
        float _horizontalThrust;

        void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();

            _verticalThrust = 0;
            _horizontalThrust = 0;
        }

        void Update()
        {
            _verticalThrust = CalculateThrust(Input.GetAxis("Vertical"));
            _horizontalThrust = CalculateThrust(Input.GetAxis("Horizontal"));
        }

        void FixedUpdate()
        {
            UpdateThrust();

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
    }
}
