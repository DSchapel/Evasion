using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Swappable/Weapon")]

public class WeaponSwap : ScriptableObject
{
    [Header("Info")]
    public new string name;
    public WeaponData data;
}