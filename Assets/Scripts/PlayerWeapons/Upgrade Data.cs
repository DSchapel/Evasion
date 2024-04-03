using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName="WeaponUpgrade", menuName="Upgrade/up")]

public class UpgradeData: ScriptableObject
{   
    [Header ("Info")]
    public new string name;
    [Header ("Upgrades")]
    public float upgradeDamage;

    public int maxAmmo;
    
    public float fireRate;
    
}
