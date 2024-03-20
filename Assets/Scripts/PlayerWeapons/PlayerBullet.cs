using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D collision)
    {
        Weapon gun = FindObjectOfType<Weapon>();
        Debug.Log(gun);
        gun.checkHit(collision);
        if(collision.gameObject.tag != ("Bullet"))
        {
            Destroy(gameObject);
        }

    }

    void Update()
    {
        Destroy(gameObject, 3f);
    }
}
