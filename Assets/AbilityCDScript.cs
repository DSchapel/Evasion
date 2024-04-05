using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCDScript : MonoBehaviour
{
    [SerializeField] AbilityDatabase anthony;
    public Slider cdBar;
    public bool gotCD = false;
    public int max;
    // Start is called before the first frame update
    void Start()
    {
        cdBar = GetComponent<Slider>();
        max = 1; //temp
    }

    // Update is called once per frame
    void Update()
    {
        if(!gotCD) {
            max = (int)(anthony.getCooldown() * 100);
            cdBar.maxValue = max;
            gotCD = true;
        }
        int cdValue = (int)(anthony.getShotTime() * 100);
        cdBar.value = cdValue;
    }
}
