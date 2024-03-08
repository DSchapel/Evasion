using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
   public AudioMixer mixer;
   public void SetLevel (float volValue)
   {
    mixer.SetFloat("SoundVol",Mathf.Log10(volValue) * 20);
   }
}
