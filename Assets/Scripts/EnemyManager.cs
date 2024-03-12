using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

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
        if(currentHealth <= 0) { Destroy(gameObject); }
    }
}
