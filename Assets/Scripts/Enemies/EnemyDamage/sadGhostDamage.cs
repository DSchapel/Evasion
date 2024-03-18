using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sadGhostDamage : MonoBehaviour
{
    // Start is called before the first frame update
 
    public int damage = 10;
    

private void OnCollisionEnter2D(Collision2D collision){
    if(collision.gameObject.tag==("Player")){
    
    var healthController=collision.gameObject.GetComponent<Health>();
    healthController.DamagePlayer(damage);
    }
    }
}

