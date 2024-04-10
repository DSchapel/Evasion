using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
public class ShotGun : MonoBehaviour
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

    // Start is called before the first frame update
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

    // Update is called once per frame
    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 spread = Vector2.zero;
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
        int shots = 10;
        for (int i = 0; i < shots; i++)
        {
            Vector3 sp = Camera.main.WorldToScreenPoint(firePoint.position);
            Vector3 dir = (firePoint.position - sp).normalized;
            
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg -90;
            float spread = Random.Range(-10, 10);
            Quaternion bulletRotation = Quaternion.Euler(new Vector3(0, 0, spread+angle+(i*20)));
            Vector3 place = new Vector3(firePoint.position.x + .01f* i, firePoint.position.y + .01f * i, 0f);
            // Instantiate the bullet using our new rotation
            GameObject bullet = Instantiate(bulletPrefab, place , bulletRotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
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
    }
}
