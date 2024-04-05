using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueWitchChase : MonoBehaviour
{
    private Transform target;
    public float speed;
    private float distance;
    private float personalSpeed;
    public float distanceToShoot;
    private float timeToFire;
    public float fireRate;
    public GameObject smallGhost;

    private Transform firingPoint;
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

       

        Shoot();
        if (distance<distanceToShoot){
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
        transform.position = Vector2.MoveTowards(transform.position, transform.position-target.position,personalSpeed*Time.deltaTime);
        }
    }


private void Shoot() {

    if (timeToFire<=0){
        


        Vector2 directionOne = new Vector2(transform.position.x + (float)0.3, transform.position.y + (float) 0.3);
    
        Vector2 directionThree = new Vector2(transform.position.x - (float) 0.3, transform.position.y - (float) 0.3);
        

        Instantiate(smallGhost, directionOne, transform.rotation);
    
        Instantiate(smallGhost, directionThree, transform.rotation);
        
        timeToFire = fireRate;

    }else{
    
        timeToFire -=Time.deltaTime;

    }

}
}


