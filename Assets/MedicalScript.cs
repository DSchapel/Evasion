using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicalScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int heal = 50;
    

private void OnCollisionEnter2D(Collision2D collision){
    if(collision.gameObject.tag==("Player")){
    
    var healthController=collision.gameObject.GetComponent<Health>();
    healthController.HealPlayer(heal);
    Destroy(gameObject);
    }
    }
}
