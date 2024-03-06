using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletDirection : MonoBehaviour
{
    public float speed;
    private Transform target;
    private float distance;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up*speed*Time.deltaTime);
        distance = Vector2.Distance(transform.position, target.position); 
        if (distance>10){
            Destroy(this.gameObject,2f);
        }
    }
}
