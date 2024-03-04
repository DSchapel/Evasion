using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("References")]
    [SerializeField] WeaponData data;

    void start()
    {
        Debug.Log("Set up");
        PlayerShoot.shootInput += Shoot;
        Debug.Log("Started");
    }

    public void Shoot()
    {
        Debug.Log("Shot Gun!");
    }
}
