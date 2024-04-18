using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EctoplasmGrenade : MonoBehaviour
{
    [SerializeField] Transform location;

    void Update()
    {
        StartCoroutine(Explode());
        Destroy(gameObject, 2f);
    }

    public IEnumerator Explode()
    {
        yield return new WaitForSeconds(1.9f);
        AbilityDatabase master = FindObjectOfType<AbilityDatabase>();
        master.Explode(location);
    }

    public void accExplode()
    {
        AbilityDatabase master = FindObjectOfType<AbilityDatabase>();
        master.Explode(location);
        Destroy(gameObject);
    }
}
