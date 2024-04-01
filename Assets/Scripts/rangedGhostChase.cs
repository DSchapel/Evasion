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
    public bool shouldRotate = true;

    public float spaceBetween = 1.5f;

    GameObject[] enemy;

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
    enemy = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject go in enemy){
            if (go!=gameObject){
                float distanceBetween = Vector3.Distance(go.transform.position, this.transform.position);
                if (distanceBetween<spaceBetween){
                    Vector2 directionee = (transform.position - go.transform.position);
                    
                    transform.Translate(directionee*Time.deltaTime);
                } 
            }
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

        Instantiate(bullet, transform.position, transform.rotation);
        timeToFire = fireRate;

        if (!shouldRotate) {
            transform.up = Vector2.up;
        }
    } else{
        timeToFire -=Time.deltaTime;

    }

}



}
