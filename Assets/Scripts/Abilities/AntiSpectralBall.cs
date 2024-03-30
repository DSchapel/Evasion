using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiSpectralBall : MonoBehaviour
{
    public float damage;
    public Vector2 col;
    int isCol;
    public LayerMask enemies;
    Collider2D[] results;
    public Transform circle;

    

    void Start()
    {
        results = new Collider2D[20];
    }

    void Update()
    {
        Destroy(gameObject, 3f);
    }

    void FixedUpdate()
    {
        col.x = circle.position.x; col.y = circle.position.y;
        isCol = Physics2D.OverlapCircleNonAlloc(col, 1, results, enemies);
        if (isCol > 0)
        {
            AbilityDatabase master = FindObjectOfType<AbilityDatabase>();
            for (int i = 0; i < isCol; i++)
            {
                master.checkHit(results[i], damage);
            }
        }
    }
}
