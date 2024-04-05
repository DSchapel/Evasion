using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicalScript : MonoBehaviour
{
    
    public int heal = 50;
    public Vector2 col;
    int isCol;
    public LayerMask layer;
    Collider2D[] results;
    public Transform circle;

    void Start()
    {
        results = new Collider2D[20];
    }

    void Update()
    {
        col.x = circle.position.x; col.y = circle.position.y;
        isCol = Physics2D.OverlapCircleNonAlloc(col, 0.35f, results, layer);
        if(isCol > 0 )
        {
            Debug.Log("collision");
            for(int i = 0; i < isCol; i++)
            {
                GameObject collision = results[i].gameObject;
                if (collision.tag == ("Player"))
                {

                    var healthController = collision.GetComponent<Health>();
                    healthController.HealPlayer(heal);
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){
    if(collision.gameObject.tag==("Player")){
    
    var healthController=collision.gameObject.GetComponent<Health>();
    healthController.HealPlayer(heal);
    Destroy(gameObject);
    }
    }
}
