using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float curTime = 0.0f;
    public bool isRunning = true;
    // Update is called once per frame
    void Update()
    {
        if(isRunning){
            curTime += Time.deltaTime;
        }
    }

    void pause() {
        isRunning = false;
    }
}
