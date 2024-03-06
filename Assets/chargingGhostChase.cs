using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chargingGhostChase : MonoBehaviour
{
   private Transform target;
    public float speed;
    public float chargeSpeed;
    private float distance;
    private float personalSpeed;
    private float chargeTime = 4;
    public float pauseTime;
    private float mePauseTime;
    public float chargeLength;
    private Vector2 lastPosition; 
    private float xPos;
    private float yPos;
    public float rangeOfCharge;
    public float chargingUpSpeed;
    public int randomNumber;
    
   
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        personalSpeed = (float) speed;
        mePauseTime = (float) pauseTime;
    }

    // Update is called once per frame
    void Update()
    {
        personalSpeed = (float) speed;
        distance = Vector2.Distance(transform.position, target.position);
        if (distance<rangeOfCharge){
            Charge();
            personalSpeed = (float) chargingUpSpeed;
        } else{
             chargeTime = chargeLength;
             transform.position = Vector2.MoveTowards(transform.position, target.position,personalSpeed*Time.deltaTime);
        }
    }


    void Charge(){
         if (chargeTime<=chargeLength){
        personalSpeed = chargeSpeed;
        transform.position = Vector2.MoveTowards(transform.position, lastPosition,personalSpeed*Time.deltaTime);
        chargeTime+=Time.deltaTime;
        
    } else{
        if (mePauseTime<=0){
            chargeTime=0;
            mePauseTime = (float) pauseTime;
            lastPosition = target.position;
        } else{
            randomNumber = Random.Range(1,2000)%10;
            if (randomNumber>5){
            transform.position = Vector2.MoveTowards(transform.position, target.position,(float)1*Time.deltaTime);
            } else{
            transform.position = Vector2.MoveTowards(transform.position, target.position,(float)-1*Time.deltaTime);}
            mePauseTime-=Time.deltaTime;
         
        }

    }
    }
}
