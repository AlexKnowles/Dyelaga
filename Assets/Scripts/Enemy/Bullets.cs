using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dyelaga.Enemy
{
    public enum BulletDirection {
        Up,
        Right,
        Down,
        Left
    }

    public class Bullets : MonoBehaviour
    {
        public GameObject EnemyBullet;
        public float bulletSpeed = 1;

        GameObject _bulletContainer;

        void Start()
        {
            _bulletContainer = new GameObject("BulletContainer");
        }

        public void SpawnBullet(BulletDirection direction, Vector2 position)
        {
            Vector2 bulletVector = GetVectorFromDirection(direction);

            GameObject bullet = Instantiate(EnemyBullet, position, new Quaternion(), _bulletContainer.transform);
            bullet.GetComponent<Rigidbody2D>().AddRelativeForce(bulletVector*bulletSpeed, ForceMode2D.Impulse);
            Destroy(bullet, 5f);
        }

        private static Vector2 GetVectorFromDirection(BulletDirection direction) {
               switch (direction) {
                case BulletDirection.Up:
                    return Vector2.up;
                case BulletDirection.Right:
                    return Vector2.right;
                case BulletDirection.Down:
                    return Vector2.down;
                case BulletDirection.Left:
                    return Vector2.left;
                default:
                    Debug.Log($"You forgot to implement a vector for {direction}");
                    return Vector2.down;
            }
        }

        void OnDestroy() {
            Destroy(_bulletContainer);
        }
    }
}