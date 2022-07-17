using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dyelaga.Enemy
{
    public class Bullets : MonoBehaviour
    {
        public GameObject EnemyBullet;
        public float bulletSpeed = 1;

        GameObject _bulletContainer;

        void Start()
        {
            _bulletContainer = new GameObject("BulletContainer");
        }

        public void SpawnBullet(Vector2 direction, Vector2 position)
        {
            GameObject bullet = Instantiate(EnemyBullet, position, new Quaternion(), _bulletContainer.transform);
            bullet.GetComponent<Rigidbody2D>().AddRelativeForce(direction*bulletSpeed, ForceMode2D.Impulse);
            Destroy(bullet, 5f);
        }

        void OnDestroy() {
            Destroy(_bulletContainer);
        }
    }
}