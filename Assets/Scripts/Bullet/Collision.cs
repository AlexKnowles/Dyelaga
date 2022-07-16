using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dyelaga.Bullet
{
    public class Collision : MonoBehaviour
    {

        void OnCollisionEnter2D (Collision2D collision)
        {
            if(collision.gameObject.name == "TheShip" 
                || collision.gameObject.name.Contains("Bullet"))
            {
                Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            } else {
                Destroy(this.gameObject);
            }
        }
    }
}