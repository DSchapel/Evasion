using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AbilitySystem : MonoBehaviour
{
    public static UnityAction abilityInput;

    [SerializeField] private KeyCode abilityKey;
    void Update()
    {
        if(Input.GetKeyDown(abilityKey))
        {
            abilityInput?.Invoke();
        }
    }
}
