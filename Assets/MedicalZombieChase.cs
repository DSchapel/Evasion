using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicalZombieChase : MonoBehaviour
{
    private Transform target;
    public float speed;
    private float distance;
    private float personalSpeed;
    public GameObject medkit;

    public float spaceBetween = 1.5f;

    GameObject[] enemy;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        personalSpeed = (float) speed;
    }

    // Update is called once per frame
    void Update()
    {

         var healthController=gameObject.GetComponent<EnemyManager>();
        if (healthController.currentHealth<=0){
            Instantiate(medkit, transform.position, transform.rotation);
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
    transform.position = Vector2.MoveTowards(transform.position, target.position,personalSpeed*Time.deltaTime);
    }
}
