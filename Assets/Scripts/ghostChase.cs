using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostChase : MonoBehaviour
{
   
    private Transform target;
    public float speed;
    private float distance;
    private float personalSpeed;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        personalSpeed = (float) speed;
    }

    // Update is called once per frame
    void Update()
    {
        personalSpeed = (float) speed;
        distance = Vector2.Distance(transform.position, target.position);
        transform.position = Vector2.MoveTowards(transform.position, target.position,personalSpeed*Time.deltaTime);
    }

}
