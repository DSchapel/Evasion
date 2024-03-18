using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiSpectralBall : MonoBehaviour
{
    public int damage;

    void OnCollisionEnter2D(Collision2D collision)
    {
        AbilityDatabase master = FindObjectOfType<AbilityDatabase>();
        master.checkHit(collision, damage);
    }

    void Update()
    {
        Destroy(gameObject, 3f);
    }
}
