using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WaveTracker : MonoBehaviour
{
  
    public TMP_Text WaveText;

    private GameObject target;
    public static int yoyowassup;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
       
    }

    // Update is called once per frame
    void Update()
    {
        var waveNumber = target.GetComponent<EnemySpawnManager>();
        yoyowassup = waveNumber.Wave;
        WaveText.SetText("Wave " + waveNumber.Wave);
    }
}
