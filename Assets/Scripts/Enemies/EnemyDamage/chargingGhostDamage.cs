using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chargingGhostDamage : MonoBehaviour
{
   
    public int damage = 30;
    private void OnCollisionEnter2D(Collision2D collision){
    if(collision.gameObject.tag==("Player")){
    
    var healthController=collision.gameObject.GetComponent<Health>();
    healthController.DamagePlayer(damage);
    }
    }
}
