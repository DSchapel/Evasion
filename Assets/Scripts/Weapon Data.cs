using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName="Weapon", menuName="Weapon/Gun")]

public class WeaponData: ScriptableObject
{
    [Header ("Info")]
    public new string name;

    [Header ("Shooting")]
    public float damage;
    public float maxDistance;

    [Header ("Reloading")]
    public int currentAmmo;
    public int maxAmmo;
    public float fireRate;
    public float reloadTime;
    [HideInInspector]
    public bool reloading;
}
