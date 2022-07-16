using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dyelaga.Enemy
{
    public class Movement : MonoBehaviour
    {
        
        public float ThrustSpeed = 1;
        public float RotationSpeed = 1;
        public float MaxVelocity = 1;
        public float MaxAngularVelocity = 1;
        public float PointSwitchDistance = 1;

        Vector3[] _pathPositions;
        Vector3 _nextPosition;
        Rigidbody2D _rigidbody2D;            
        float _torque;
        int _nextPositionIndex;
        bool _startMoving;

            
        void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _torque = 0;
            _startMoving = false;
        }

        void Update()
        {
            if(!_startMoving)
            {
                return;
            }

            Vector2 distanceFromNextPosition = (_nextPosition - transform.position);

            if(Mathf.Sqrt(Mathf.Pow(distanceFromNextPosition.x, 2) + Mathf.Pow(distanceFromNextPosition.x, 2)) < PointSwitchDistance)
            {
                _nextPositionIndex++;

                if(_nextPositionIndex < _pathPositions.Length)
                {
                    _nextPosition = _pathPositions[_nextPositionIndex];
                }
                else
                {
                    Destroy(this.gameObject);
                }
            }
 
        }   

        void FixedUpdate()
        {
            if(!_startMoving)
            {
                return;
            }

            
            Vector3 dir = ((Vector2)_nextPosition - _rigidbody2D.position);
            dir /= Time.fixedDeltaTime;
            dir = Vector3.ClampMagnitude(dir, ThrustSpeed);
            _rigidbody2D.velocity = dir;

            // UpdateThrust();
            // UpdateTorque();
        }

        public void SetPath(Vector3[] path)
        {
            _pathPositions = path;
            _nextPositionIndex = 1;
            _nextPosition = path[_nextPositionIndex];
            _startMoving = true;
        }

        float CalculateTorque()
        {   
            // rotate if angle is bigger than that to decrease it
            float maxAngle = 10f;
    
            // where is the target?
            Vector2 targetDirection = (_nextPosition - transform.position);
            // where are we looking?
            Vector2 lookDirection = transform.up;
    
            // to indicate the sign of the (otherwise positive 0 .. 180 deg) angle
            Vector3 cross = Vector3.Cross(targetDirection, lookDirection);
            // actually get the sign (either 1 or -1)
            float sign = Mathf.Sign(cross.z);
    
            // the angle, ranging from 0 to 180 degrees
            float angle = Vector2.Angle(targetDirection, lookDirection);
    
            // apply the sign to get angles ranging from -180 to 0 to +180 degrees
            angle *= sign;
    
            // apply torque in the opposite direction to decrease angle
            // if (Mathf.Abs(angle) > maxAngle) 
            // {Gi
                return -sign*RotationSpeed;
            // }

            // return 0;
        }

        void UpdateThrust()
        {
            Vector2 force = (new Vector2(0, ThrustSpeed));

            _rigidbody2D.AddRelativeForce(force, ForceMode2D.Impulse);
            _rigidbody2D.velocity = Vector2.ClampMagnitude(_rigidbody2D.velocity, MaxVelocity);
        }
        
        void UpdateTorque()
        {
            _rigidbody2D.AddTorque(_torque);
            _rigidbody2D.angularVelocity = Mathf.Clamp(_rigidbody2D.angularVelocity, -MaxAngularVelocity, MaxAngularVelocity);
        }
    }
}