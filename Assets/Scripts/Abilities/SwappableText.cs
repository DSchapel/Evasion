using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwappableText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] ASwapData data;
    [SerializeField] Swappable swapper;

    void Start()
    {
        text.text = data.name + "\n" + data.description + "\n\nPress F to switch";
    }

    void Update()
    {
        if(swapper.popup == true)
        {
            text.enabled = true;
        }
        else
        {
            text.enabled = false;
        }
    }
}
