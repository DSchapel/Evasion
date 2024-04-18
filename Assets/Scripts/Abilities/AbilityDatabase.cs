using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDatabase : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject swordPrefab;
    [SerializeField] GameObject grenadePrefab;
    [SerializeField] GameObject explosionPrefab;
    

    public int ability;
    public float timeSinceLastShot = 50f;
    public float cooldown;
    public float buffTime;
    public bool isBuffed;
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
        if(ability == 3)
        {
            cooldown = 5f;
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
            if(timeSinceLastShot >= cooldown)
            {
                SwordOfExorcism();
            }
        }
        if(ability == 3)
        {
            if(timeSinceLastShot >= cooldown)
            {
                EctoplasmGrenade();
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
        /*
        GameObject sword = Instantiate(swordPrefab, firePoint.position, new());
        Rigidbody2D rb = sword.GetComponent<Rigidbody2D>();
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle + 60f, Vector3.forward);
        sword.transform.rotation = rotation;
        */
    }

    public void EctoplasmGrenade()
    {
        GameObject nade = Instantiate(grenadePrefab, firePoint.position, new());
        Rigidbody2D rb = nade.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * 1.5f, ForceMode2D.Impulse);
        timeSinceLastShot = 0f;
    }

    public void Explode(Transform location)
    {
        Debug.Log("Explode plz");
        GameObject explosion = Instantiate(explosionPrefab, location.position, new());
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
