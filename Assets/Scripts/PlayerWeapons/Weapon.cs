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

    public float timeSinceLastShot = 50f;
    public int currentAmmo;
    Vector2 mousePos;

    void Start()
    {
        currentAmmo = data.maxAmmo;
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += startReload;
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

    public void Shoot()
    {
        if (currentAmmo > 0)
        {
            if (canShoot())
            {
                Debug.Log("shot");

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
    }

    void FixedUpdate()
    {
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
    }

    private void OnGunShot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }

    public void checkHit(Collision2D col)
    {
        GameObject hit = col.gameObject;
        if (hit.tag == "Enemy")
        {
            EnemyManager enemy = hit.GetComponent<EnemyManager>();
            enemy.takeDamage(data.damage);
        }
    }

    public int getMaxAmmo() {
        return data.maxAmmo;
    }
    public void setDamage(int damage)
    {
        data.damage += damage;
    }
}
