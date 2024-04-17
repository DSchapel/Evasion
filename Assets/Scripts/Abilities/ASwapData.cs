using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Swappable/Ability")]

public class ASwapData : ScriptableObject
{
    [Header("Info")]
    public new string name;
    public new string description;

    [Header("Ability Num")]
    public int abilityNum;
}
