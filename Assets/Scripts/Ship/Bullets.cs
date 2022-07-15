using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dyelaga.Ship
{
    public class Bullets : MonoBehaviour
    {
        public GameObject RedBullet;        
        public float bulletSpeed = 1;

        GameObject _bulletContainer;

        void Start()
        {
            _bulletContainer = new GameObject("BulletContainer");
        }

        public void SpawnBullet(string type, Vector2 position)
        {
            GameObject chosenBullet = null;

            switch (type)
            {
                case "Red":
                    chosenBullet = RedBullet;
                    break;
            }
            
            GameObject bullet = Instantiate(chosenBullet, position, new Quaternion(), _bulletContainer.transform);
            bullet.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up*bulletSpeed, ForceMode2D.Impulse);
        }
    }
}