using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwitchableText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] WeaponSwap data;
    [SerializeField] ReplaceWeapon swapper;

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
