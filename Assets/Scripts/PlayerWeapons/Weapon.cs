using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("References")]
    [SerializeField] WeaponData data;
    [SerializeField] Camera cam;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float bulletForce;
    [SerializeField] Rigidbody2D rb;
    private AudioSource audioSource;

    public float timeSinceLastShot = 50f;
    public int currentAmmo;
    Vector2 mousePos;
    public SpriteRenderer sprite;

    void Start()
    {
        currentAmmo = data.maxAmmo;
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += startReload;
        audioSource = GetComponent<AudioSource>();
        data.reloading = false;
    }

    public bool canShoot() => !data.reloading && timeSinceLastShot > 1f / (data.fireRate / 60f);

    public void startReload()
    {
        if (!data.reloading)
        {
            StartCoroutine(reload());
        }
    }

    private IEnumerator reload()
    {
        data.reloading = true;

        yield return new WaitForSeconds(data.reloadTime);
        currentAmmo = data.maxAmmo;

        data.reloading = false;
    }

    public void instantReload()
    {
        data.reloading = true;
        currentAmmo = data.maxAmmo;
        data.reloading = false;
    }

    public void Shoot()
    {
        if (currentAmmo > 0)
        {
            if (canShoot())
            {
                currentAmmo--;
                timeSinceLastShot = 0;
                OnGunShot();
            }
        }
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if(currentAmmo <= 0)
        {
            startReload();
        }
    }

    void FixedUpdate()
    {
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;

        sprite.flipY = lookDir.x < 0;
    }

    private void OnGunShot()
    {
        if(data.name != "Shotgun")
        {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, new());
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        playShotAudio();
        }else
        {
            int shots = 5;
            for (int i = 0; i < shots; i++)
            {
                Vector3 sp = Camera.main.WorldToScreenPoint(firePoint.position);
                Vector3 dir = (firePoint.position - sp).normalized;
            
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg -90;
                float spread = Random.Range(-35, 35);
                Quaternion bulletRotation = Quaternion.Euler(new Vector3(0, 0, spread+angle+(i*20)));
                Vector3 place = new Vector3(firePoint.position.x + .01f* i, firePoint.position.y + .01f * i, 0f);
                // Instantiate the bullet using our new rotation
                GameObject bullet = Instantiate(bulletPrefab, place , bulletRotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce((firePoint.up) * bulletForce, ForceMode2D.Impulse);
            }
        }
    }

    public void checkHit(Collision2D col)
    {
        GameObject hit = col.gameObject;
        if (hit.tag == "Enemy")
        {
            EnemyManager enemy = hit.GetComponent<EnemyManager>();
            enemy.takeDamage(data.damage);
        }
        if(hit.tag == "Grenade")
        {
            EctoplasmGrenade grenade = hit.GetComponent<EctoplasmGrenade>();
            grenade.accExplode();
        }
    }

    public int getMaxAmmo() {
        return data.maxAmmo;
    }
    public void setDamage(int damage)
    {
        data.damage += damage;
    }
    public WeaponData getData()
    {
        return data;
    }
    public void setCurrAmmo()
    {
        currentAmmo = getMaxAmmo();
    }
    public void setData(WeaponData newData)
    {
        bulletPrefab = newData.bulletPrefab;
        sprite.sprite = newData.sprite;
        data = newData;
        data.reloading = false;
    }
    public void playShotAudio()
    {
        audioSource.Play();
    }
}
