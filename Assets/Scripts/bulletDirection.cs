using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletDirection : MonoBehaviour
{
    public float speed;
    private Transform target;
    private float distance;
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        direction = transform.up;
        transform.rotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
         var healthController=gameObject.GetComponent<EnemyManager>();
        if (healthController.currentHealth<=0){
        Destroy(gameObject);
        }
        transform.position += direction * speed * Time.deltaTime;
        distance = Vector2.Distance(transform.position, target.position); 
        if (distance>10){
            Destroy(this.gameObject,2f);
        }
    }
}
