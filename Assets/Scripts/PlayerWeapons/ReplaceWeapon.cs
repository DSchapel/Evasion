using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ReplaceWeapon : MonoBehaviour
{

int isCol;
public Vector2 col;
public LayerMask layer;
Collider2D[] results;
public Transform circle;
[SerializeField] private KeyCode swapKey;
[SerializeField] private WeaponSwap swapWeapon;

void Start()
{
    results = new Collider2D[20];
}

void Update()
{
    col.x = circle.position.x; col.y = circle.position.y;
    isCol = Physics2D.OverlapCircleNonAlloc(col, 100000f, results, layer);
    if (isCol > 0)
    {
        for(int i = 0; i < isCol; i++)
        {
            GameObject hit = results[i].gameObject;
            if (hit.tag == ("Player")) 
            {
                if (Input.GetKeyDown(swapKey))
                {
                    Weapon old = FindObjectOfType<Weapon>();
                    old.setData(swapWeapon.data);
                }
            }
        }
    }
}

}
