using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDatabase : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;
    [SerializeField] Transform firePoint;

    public int ability;
    public float timeSinceLastShot = 50f;
    public float cooldown;
    // Start is called before the first frame update
    void Start()
    {
        AbilitySystem.abilityInput += Ability;
        if(ability == 0)
        {
            cooldown = 5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastShot += Time.deltaTime;
    }

    public void Ability()
    {
        if(ability == 0)
        {
            if (timeSinceLastShot >= cooldown)
            {
                AntiSpectralBall();
                timeSinceLastShot = 0;
            }
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
