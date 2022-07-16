using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dyelaga.TheShip;

namespace Dyelaga.Enemy
{
    public class Collision : MonoBehaviour
    {

        void OnCollisionEnter2D (Collision2D collision)
        {
            if(collision.gameObject.name.Contains("Wall")
            || collision.gameObject.name.Contains("Enemy"))
            {
                Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }
            if (collision.gameObject.name.Contains("TheShip")) {
                collision.gameObject.GetComponent<TakeDamage>().TakeDamageAmount(1000);
            }
        }
    }
}