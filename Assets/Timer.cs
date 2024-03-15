using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float curTime = 0.0f;
    public TMP_Text timeText;
    public bool isRunning = true;
    // Update is called once per frame
    void Update()
    {
        if(isRunning){
            curTime += Time.deltaTime;
            int seconds = ((int)curTime % 60);
            int minutes = ((int)curTime / 60);
            timeText.SetText(string.Format("{0:00}:{1:00}", minutes, seconds));
        }
    }

    void pause() {
        isRunning = false;
    }

    //When gameover event is sent, call pause
}
