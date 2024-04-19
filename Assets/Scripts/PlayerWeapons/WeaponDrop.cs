using System.Collections.Generic;
using UnityEngine;

public class WeaponDrop : MonoBehaviour
{   
    [SerializeField] private GameObject rifle;
    [SerializeField] private GameObject shotgun;

    [SerializeField] private GameObject revolverUpgrade;
    public EnemySpawnManager esm;
    List<GameObject> list = new List<GameObject>();

    void Start()
    {
        esm = GetComponent<EnemySpawnManager>();
    }
    public void weaponDrop(){
        var one = GameObject.Instantiate(shotgun, new Vector3(0,0,0), Quaternion.identity);
        var oneone = one.GetComponent<ReplaceWeapon>();
        oneone.weaponDrop = this;
        list.Add(one);
        var two = GameObject.Instantiate(revolverUpgrade, new Vector3 (2,0,0),Quaternion.identity);
        var twotwo = two.GetComponent<UpgradePickup>();
        twotwo.weaponDrop = this;
        list.Add(two);
        var three = GameObject.Instantiate(rifle, new Vector3(-2,0,0), Quaternion.identity);
        var threethree = three.GetComponent<ReplaceWeapon>();
        threethree.weaponDrop = this;
        list.Add(three);
        
        
    }
    public void delete()
    {
        foreach(var gameObject in list)
        {
            Destroy(gameObject);
        }
        list.Clear();
        esm.WaveStarter();
    }
}
