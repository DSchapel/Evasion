using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy")]

public class Enemy : ScriptableObject
{
    [Header("Info")]
    public new string name;

    [Header("Values")]
    public float maxHealth;
    public float damage;
}
