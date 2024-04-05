using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAudio : MonoBehaviour
{
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayerShoot.shootInput += ShootSound;
    }

    // Update is called once per frame
    void ShootSound()
    {
        audioSource.Play();
    }
}
