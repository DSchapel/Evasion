using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UpgradePickup : MonoBehaviour
{
void upgrade(UpgradeData upgradeData)
    {
    Weapon gun = FindAnyObjectByType<Weapon>();
    WeaponData newData = new WeaponData();
    WeaponData data = gun.getData();
    newData.name = upgradeData.name;
    newData.damage =  data.damage;
    newData.maxAmmo = data.maxAmmo;
    newData.maxDistance = data.maxDistance;
    newData.fireRate = data.fireRate;
    newData.reloadTime = data.reloadTime;
    newData.reloading = false;
    newData.sprite = data.sprite;
    newData.bulletPrefab = data.bulletPrefab;
    newData.damage  += upgradeData.upgradeDamage;
    newData.maxAmmo += upgradeData.maxAmmo;
    newData.fireRate += upgradeData.fireRate;
    newData.reloadTime += upgradeData.reloadTime;
    gun.setData(newData);
    gun.setCurrAmmo();
    }
int isCol;
public Vector2 col;
public LayerMask layer;
Collider2D[] results;
public Transform circle;
[SerializeField] private KeyCode swapKey;
[SerializeField] private UpgradeData upgradeData;

void Start()
{
    results = new Collider2D[20];
}

void Update()
{
    col.x = circle.position.x; col.y = circle.position.y;
    isCol = Physics2D.OverlapCircleNonAlloc(col, .35f, results, layer);
    if (isCol > 0)
    {
        for(int i = 0; i < isCol; i++)
        {
            GameObject hit = results[i].gameObject;
            if (hit.tag == ("Player")) 
            {
                if (Input.GetKeyDown(swapKey))
                {
                    
                    upgrade(upgradeData);
                    Destroy(gameObject);
                }
            }
        }
    }
}

}
