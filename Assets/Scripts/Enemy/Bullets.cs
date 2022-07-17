using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dyelaga.Enemy
{
    public class Bullets : MonoBehaviour
    {
        public GameObject EnemyBullet;
        public float bulletSpeed = 0.5f;
        public float bulletOffset = 0.2f;

        GameObject _bulletContainer;

        void Start()
        {
            _bulletContainer = new GameObject("BulletContainer");
        }

        public void SpawnBullet(Vector2 direction, Vector2 position)
        {
            // We are making an assumption here that the direction is always Vector2.up etc
            GameObject bullet = Instantiate(EnemyBullet, position + (direction * bulletOffset), new Quaternion(), _bulletContainer.transform);
            bullet.GetComponent<Rigidbody2D>().AddRelativeForce(direction*bulletSpeed, ForceMode2D.Impulse);
            Destroy(bullet, 5f);
        }

        void OnDestroy() {
            Destroy(_bulletContainer);
        }
    }
}