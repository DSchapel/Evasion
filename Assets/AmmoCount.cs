using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoCount : MonoBehaviour
{
    public TMP_Text ammoText;
    [SerializeField] Weapon weapon;
    // Update is called once per frame
    void Update()
    {
        ammoText.SetText(weapon.currentAmmo + "/" + weapon.getMaxAmmo());
    }
}
