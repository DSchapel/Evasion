using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy")]

public class Enemy : ScriptableObject
{
    [Header("Info")]
    public new string name;
    public GameObject prefab;

    [Header("Spawn Info")]
    // how difficult the enemy is (higher: less enemies of this type can exist at once)
    public float power;
    // how likely the enemy is to spawn (higher: higher chance)
    public float weight;
    // limit of how many of the same enemy type can spawn
    // (resets if no spawnable enemies are left)
    public int limit;

    [Header("Values")]
    public float maxHealth;
    public float damage;
}
