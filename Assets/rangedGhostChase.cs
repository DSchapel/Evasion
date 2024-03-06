using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangedGhostChase : MonoBehaviour
{
    private Transform target;
    public float speed;
    private float distance;
    private float personalSpeed;
    public float distanceToShoot;
    private float timeToFire;
    public float fireRate;
    public GameObject bullet;

    public Transform firingPoint;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        personalSpeed = (float) speed;
        timeToFire = fireRate;

    }

    // Update is called once per frame
    void Update()
    {
        personalSpeed = (float) speed;
        distance = Vector2.Distance(transform.position, target.position);
        Vector2 direction = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
        transform.up = direction;
        Shoot();
        if (distance>distanceToShoot){
        transform.position = Vector2.MoveTowards(transform.position, target.position,personalSpeed*Time.deltaTime);
        }
    }


private void Shoot() {
    if (timeToFire<=0){
        Instantiate(bullet, transform.position, transform.rotation);
        timeToFire = fireRate;
    } else{
        timeToFire -=Time.deltaTime;

    }

}



}
