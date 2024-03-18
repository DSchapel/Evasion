using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{

    public float movSpeed=6.0f;
    public float Speed = 50.0f;
    public float friction=50.0f;
    Vector3 currspeed;
    float speedX, speedY;
    private bool canDash = true;
    private bool isDashing;
    public float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    //[SerializeField] private TrailRenderer tr;
    Rigidbody2D rb;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();



    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }
        float speedX = Input.GetAxisRaw("Horizontal");
        float speedY = Input.GetAxisRaw("Vertical") ;
        Vector3 vector = new Vector3(speedX, speedY, 0);
        currspeed += vector.normalized * Time.deltaTime * (Speed + friction);
        if(currspeed.magnitude < friction*Time.deltaTime)
        {
            currspeed = new(0,0,0);
        }
        else
        {
            currspeed -= currspeed.normalized * friction * Time.deltaTime;
        }
        if (currspeed.magnitude > movSpeed)
        {
            currspeed = currspeed.normalized*movSpeed;
        }
        //transform.position += Vector3.Normalize(vector) * Time.deltaTime * 6;
        transform.position += (currspeed * Time.deltaTime);
    
    }
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * (dashingPower), Input.GetAxisRaw("Vertical") * (dashingPower));
        //tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        //tr.emitting = false;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;

    }
}
