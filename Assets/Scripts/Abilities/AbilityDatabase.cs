using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDatabase : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;
    [SerializeField] Transform firePoint;

    public int ability;
    // Start is called before the first frame update
    void Start()
    {
        AbilitySystem.abilityInput += Ability;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Ability()
    {
        if(ability == 0)
        {
            AntiSpectralBall();
        }
    }

    public void AntiSpectralBall()
    {
        GameObject ball = Instantiate(ballPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * 20f, ForceMode2D.Impulse);
    }

    public void checkHit(Collision2D col, int damage)
    {
        GameObject hit = col.gameObject;
        if (hit.tag == "Enemy")
        {
            EnemyManager enemy = hit.GetComponent<EnemyManager>();
            enemy.takeDamage(damage);
        }
    }
}
