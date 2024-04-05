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
    public float buffTime;
    public bool isBuffed;
    // Start is called before the first frame update
    public AbilityAudioScript abilityAudioScript0;
    public AbilityAudioScript abilityAudioScript1;
    void Start()
    {
        AbilitySystem.abilityInput += Ability;
        AbilitySetup();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBuffed)
        {
            timeSinceLastShot -= Time.deltaTime;
        }
        else
        {
            timeSinceLastShot += Time.deltaTime;
        }
    }

    public void AbilitySwitch(int newNum)
    {
        ability = newNum;
        AbilitySetup();
    }

    public void AbilitySetup()
    {
        if (ability == 0)
        {
            cooldown = 5f;
        }
        if (ability == 1)
        {
            cooldown = 10f;
            buffTime = 10f;
        }
        if(ability == 2)
        {
            cooldown = 3f;
        }
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
        if(ability == 1)
        {
            if(timeSinceLastShot >= cooldown)
            {
                if (!isBuffed)
                {
                    SpectralEnhancement();
                }
            }
        }
        if(ability == 2)
        {
            if(timeSinceLastShot < cooldown)
            {
                SwordOfExorcism();
            }
        }
    }

    public void AntiSpectralBall()
    {
        GameObject ball = Instantiate(ballPrefab, firePoint.position, new());
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * 3f, ForceMode2D.Impulse);
        abilityAudioScript0.playSound();
    }

    public void SpectralEnhancement()
    {
        timeSinceLastShot = buffTime;
        Weapon weapon = FindObjectOfType<Weapon>();
        WeaponData data = weapon.getData();
        isBuffed = true;
        weapon.currentAmmo = 9999;
        data.damage *= 1.3f;
        StartCoroutine(DeEnhance());
    }

    public IEnumerator DeEnhance()
    {
        Weapon weapon = FindObjectOfType<Weapon>();
        WeaponData data = weapon.getData();
        yield return new WaitForSeconds(buffTime);
        isBuffed = false;
        timeSinceLastShot = 0;
        weapon.currentAmmo = data.maxAmmo;
        data.damage /= 1.3f;
    }

    public void SwordOfExorcism()
    {

    }

    public void checkHit(Collider2D col, float damage)
    {
        GameObject hit = col.gameObject;
        if (hit.tag == "Enemy")
        {
            EnemyManager enemy = hit.GetComponent<EnemyManager>();
            enemy.takeDamage(damage);
        }
    }

    public float getShotTime() {
        return timeSinceLastShot;
    }

    public float getCooldown() {
        return cooldown;
    }
}
