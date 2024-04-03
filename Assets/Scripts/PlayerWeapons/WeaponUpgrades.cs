using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] UpgradeData upgradeData;
    void Start()
    {
    Weapon gun = FindAnyObjectByType<Weapon>();
    WeaponData data = gun.getData();
    data.damage  += upgradeData.upgradeDamage;
    data.maxAmmo += upgradeData.maxAmmo;
    data.fireRate += upgradeData.fireRate;
    }
}
