using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dyelaga.Bullet
{
    public class Collision : MonoBehaviour
    {
        FadeOut fadeOut;

        void Start()
        {
            fadeOut = GetComponent<FadeOut>();
        }

        void OnCollisionEnter2D (Collision2D collision)
        {
            if(collision.gameObject.name == "TheShip" 
                || collision.gameObject.name.Contains("Bullet"))
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
            } 
            else 
            {
                Enemy.Health enemy = collision.gameObject.GetComponent< Enemy.Health>();

                if(enemy != null)
                {
                    if(enemy.TakeHit(this.gameObject.name))
                    {
                        Destroy(this.gameObject);
                    }
                    else
                    {
                        fadeOut.Begin();
                        GetComponent<Collider2D>().enabled = false;
                    }
                }
                else
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}