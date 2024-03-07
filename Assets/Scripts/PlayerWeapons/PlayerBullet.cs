using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    void Update()
    {
        Destroy(gameObject, 3f);
    }
}
