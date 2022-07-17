using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dyelaga.Bullet
{
    public class EnemyBulletCollision : MonoBehaviour
    {
        FadeOut fadeOut;

        void Start()
        {
            fadeOut = GetComponent<FadeOut>();
        }

        void OnCollisionEnter2D (Collision2D collision)
        {
            if(collision.gameObject.name.Contains("Enemy") 
                || collision.gameObject.name.Contains("Bullet"))
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
            } 
            else 
            {
                TheShip.TakeDamage ship = collision.gameObject.GetComponent<TheShip.TakeDamage>();

                if(ship != null)
                {
                    ship.TakeDamageAmount(1000);
                    Destroy(this.gameObject);
                }
                else
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}