using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradableText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] UpgradeData data;
    [SerializeField] UpgradePickup swapper;

    void Start()
    {
        text.text = data.name + "\n" + data.description + "\n\nPress F to switch";
    }

    void Update()
    {
        if (swapper.popup == true)
        {
            text.enabled = true;
        }
        else
        {
            text.enabled = false;
        }
    }
}
