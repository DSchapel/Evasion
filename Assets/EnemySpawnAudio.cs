using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnAudio : MonoBehaviour
{
    private AudioSource audioSource;
    public EnemySpawner enemySpawner;

    public bool soundPlayed;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        /*if (enemySpawner.getTimer() <= 0 && soundPlayed == false)
        {
            audioSource.Play();
            soundPlayed = true;
        }*/
    }
}