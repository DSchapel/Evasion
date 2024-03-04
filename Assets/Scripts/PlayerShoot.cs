using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerShoot : MonoBehaviour
{
    public static UnityAction shootInput;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            shootInput?.Invoke();
        }
    }
}
