using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FinalWave : MonoBehaviour
{
  
    public TMP_Text WaveText;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       
        WaveText.SetText("Wave " + WaveTracker.yoyowassup);
    }
}