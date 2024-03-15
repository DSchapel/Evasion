using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sadGhostDamage : MonoBehaviour
{
    // Start is called before the first frame update
   public Health Health;
    public int damage;
    private void OnCollisionEnter2D(Collision2D collision){
    if(collision.gameObject.tag=="Player"){
    Health.DamagePlayer(damage);

    }
    }
}
