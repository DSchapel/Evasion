using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour
{
    public static UnityAction enemyDeath;
    [Header("References")]
    [SerializeField] Enemy data;

    public float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = data.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(float dam)
    {
        currentHealth -= dam;

        if(currentHealth <= 0)
        {
            enemyDeath?.Invoke();
            Destroy(gameObject); 
        }
    }
}
