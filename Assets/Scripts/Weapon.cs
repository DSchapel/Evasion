using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("References")]
    [SerializeField] WeaponData data;
    public float timeSinceLastShot = 50f;

    void Start()
    {
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
        data.currentAmmo = data.maxAmmo;

        data.reloading = false;
    }

    public void Shoot()
    {
        if (data.currentAmmo > 0)
        {
            if (canShoot())
            {
                Debug.Log("shot");
                if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, data.maxDistance))
                {
                    Debug.Log(hitInfo.transform.name);
                }

                data.currentAmmo--;
                timeSinceLastShot = 0;
                OnGunShot();
            }
        }
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
    }

    private void OnGunShot()
    {
    }
}
