using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesDrop : MonoBehaviour
{
    private EnemySpawnManager esm;
    [SerializeField] GameObject[] weaponUpgrades;
    [SerializeField] GameObject[] abilities;
    private AbilityDatabase ability;
    List<GameObject> list = new List<GameObject>();
    void Start()
    {
        esm = GetComponent<EnemySpawnManager>();
        ability = GetComponent<AbilityDatabase>();
    }
    private GameObject pickAbility()
    {
        int size = abilities.Length;
        GameObject gameObject =  abilities[Random.Range(0,size-1)];
        var theAbility = gameObject.GetComponent<Swappable>();
        
        while(ability.ability == theAbility.data.abilityNum)
        {
            gameObject =  abilities[Random.Range(0,size)];
            theAbility = gameObject.GetComponent<Swappable>();
        }
        return gameObject;
    }
    private GameObject picker()
    {
        GameObject[] gameObjects;
        if(Random.Range(0,2) == 0)
        {
            gameObjects = weaponUpgrades;
        }
        else
        {
            return pickAbility();
        }
        int size = gameObjects.Length;
        int num = Random.Range(0,size);
        return gameObjects[num];
    }
    public void upgradeDroper()
    {
        GameObject thing = picker();
        GameObject thing2 = picker();
        while(thing == thing2)
        {
            thing2 = picker();
        }
        GameObject thing3 = picker();
        while( thing2 == thing3 || thing3 == thing)
        {
            thing3 = picker();
        }
        var one = GameObject.Instantiate(thing, new Vector3(0,0,0), Quaternion.identity);
        var oneone = one.GetComponent<UpgradePickup>();
        var onetwo = one.GetComponent<Swappable>();
        if(oneone != null)
        {
           oneone.upgradesDrop = this;
        }
        if(onetwo != null)
        {
           onetwo.upgradesDrop = this;
        }
        list.Add(one);
        var two = GameObject.Instantiate(thing2, new Vector3 (2,0,0),Quaternion.identity);
        var twotwo = two.GetComponent<UpgradePickup>();
        var twoone = two.GetComponent<Swappable>();
        if(twotwo != null)
        {
           twotwo.upgradesDrop = this;
        }
        if(twoone != null)
        {
           twoone.upgradesDrop = this;
        }
        
        list.Add(two);
        var three = GameObject.Instantiate(thing3, new Vector3(-2,0,0), Quaternion.identity);
        var threethree = three.GetComponent<UpgradePickup>();
        var threefour = three.GetComponent<Swappable>();
        if(threethree != null)
        {
           threethree.upgradesDrop = this;
        }
        if(threefour != null)
        {
           threefour.upgradesDrop = this;
        }
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
