using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedWitchChase : MonoBehaviour
{
    private Transform target;
    public float speed;
    private float distance;
    private float personalSpeed;
    public float distanceToShoot;
    private float timeToFire;
    public float fireRate;
    public GameObject bullet;

    private Transform firingPoint;
    public bool shouldRotate = true;

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
        var healthController=gameObject.GetComponent<EnemyManager>();
        if (healthController.currentHealth<=0){
        Destroy(gameObject);
    }
        personalSpeed = (float) speed;
        distance = Vector2.Distance(transform.position, target.position);

        if (shouldRotate) {
            Vector2 direction = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
            transform.up = direction;
        }

        Shoot();
        if (distance>distanceToShoot){
        transform.position = Vector2.MoveTowards(transform.position, target.position,personalSpeed*Time.deltaTime);
        }
    }


private void Shoot() {
    if (timeToFire<=0){
        Vector2 direction = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
        transform.up = direction;
float x = (float) 0;
float y = (float)0;
float ydir = target.position.y - transform.position.y;
float xdir = target.position.x - transform.position.x;


if (xdir<(float) 0){
    xdir = -xdir;
}
if (ydir<(float) 0){
    ydir = -ydir;
}


float xOrY = (float) xdir - ydir;

if ( xOrY>(float)0){
y = (float) 1;

} else {
    x = (float) 1;
}

        Vector2 directionOne = new Vector2(transform.position.x + (float)x, transform.position.y + (float) y);
        Vector2 directionTwo = new Vector2(transform.position.x, transform.position.y);
        Vector2 directionThree = new Vector2(transform.position.x - (float) x, transform.position.y - (float) y);

        Instantiate(bullet, directionOne, transform.rotation);
        Instantiate(bullet, directionTwo, transform.rotation);
        Instantiate(bullet, directionThree, transform.rotation);
        
        timeToFire = fireRate;

        if (!shouldRotate) {
            transform.up = Vector2.up;
        }
    } else{
        timeToFire -=Time.deltaTime;

    }

}



}