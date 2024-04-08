using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{

    public float movSpeed=6.0f;
    public float acceleration = 50.0f;
    public float friction=50.0f;
    Vector3 currspeed;
    float speedX, speedY;
    private bool canDash = true;
    public bool isDashing;
    public float dashingPower = 5f;
    public float dashingTime = 0.1f;
    public float dashingPower2 = 10f;
    public float dashingTime2 = 0.3f;
    public float dashingCooldown = 1f;
    public LayerMask collisionMask;
    private int dashtype = 3;
    public Health healthval;
    public Weapon weaponinfo;
    public int currentAmmo;
    //public int currentAmmo;
    public int healthreset;
    //[SerializeField] WeaponData data;
    //public int curHealth;
    //[SerializeField] private TrailRenderer tr;
    Rigidbody2D rb;
    private Collider2D col;
    private ContactFilter2D filter;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        //curHealth = GameObject.FindGameObjectWithTag("curHealth").GetComponent<int>();
        //data= GameObject.FindGameObjectWithTag("data").GetComponent<WeaponData>();
        //currentAmmo = GameObject.FindGameObjectWithTag("currentAmmo").GetComponent<int>();
        //private GameObject player;
        //player = GameObject.Find("Player");
        //BoxCollider boxCollider = player.GetComponent<BoxCollider>();

        canDash = true;

        filter = new();
        filter.SetLayerMask(collisionMask);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing==true)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space) && canDash == true && dashtype == 3)
        {
            StartCoroutine(ReloadDash());
        }
        
            float speedX = Input.GetAxisRaw("Horizontal");
            float speedY = Input.GetAxisRaw("Vertical");
            Vector3 playerinput = new Vector3(speedX, speedY, 0);
            currspeed += playerinput.normalized * Time.deltaTime * (acceleration + friction);
            if (currspeed.magnitude < friction * Time.deltaTime)
            {
                currspeed = new(0, 0, 0);
            }
            else
            {
                currspeed -= currspeed.normalized * friction * Time.deltaTime;
            }
            if (currspeed.magnitude > movSpeed)
            {
                currspeed = currspeed.normalized * movSpeed;
            }
            transform.position += (currspeed * Time.deltaTime);


        var results = new List<Collider2D>();
        Physics2D.OverlapBox((Vector2) col.bounds.center, (Vector2) col.bounds.extents * 2.0f, 0.0f, filter, results);

        foreach (var other in results) {
            var obj = other.gameObject;
        
            var wall = obj.GetComponent<Wall>();
            if (wall == null) {
                return;
            }

            var bounds = col.bounds;
            var wallBounds = other.bounds;

            // magical number soup chosen
            // through trial and error
            // (if anyone can figure out why
            //  this needs to be here please
            //  fix it i am begging you)
            var extraSpace = 0.0201f;

            switch (wall.pushDirection) {
                case Wall.PushDirection.North:
                    transform.position = new(
                        transform.position.x,
                        wallBounds.max.y + bounds.extents.y + extraSpace,
                        transform.position.z
                    );
                    currspeed = new(
                        currspeed.x,
                        0.0f,
                        currspeed.z
                    );
                    break;
                case Wall.PushDirection.South:
                    transform.position = new(
                        transform.position.x,
                        wallBounds.min.y - bounds.extents.y - extraSpace,
                        transform.position.z
                    );
                    currspeed = new(
                        currspeed.x,
                        0.0f,
                        currspeed.z
                    );
                    break;
                case Wall.PushDirection.East:
                    transform.position = new(
                        wallBounds.max.x + bounds.extents.x + extraSpace,
                        transform.position.y,
                        transform.position.z
                    );
                    currspeed = new(
                        0.0f,
                        currspeed.y,
                        currspeed.z
                    );
                    break;
                case Wall.PushDirection.West:
                    transform.position = new(
                        wallBounds.min.x - bounds.extents.x - extraSpace,
                        transform.position.y,
                        transform.position.z
                    );
                    currspeed = new(
                        0.0f,
                        currspeed.y,
                        currspeed.z
                    );
                    break;
            }
        }
    }
    private IEnumerator TeleportDash()
    {
        float speedX = Input.GetAxisRaw("Horizontal");
        float speedY = Input.GetAxisRaw("Vertical");
        Vector3 playerinput = new Vector3(speedX, speedY, 0);
        canDash = false;
        //healthreset = curHealth;
        isDashing = true;
        //boxCollider.enabled = false;
        //rb.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * (dashingPower), Input.GetAxisRaw("Vertical") * (dashingPower), 0);
        transform.position += playerinput.normalized * (dashingPower);
        //tr.emitting = true;
        //rb.velocity = playerinput.normalized * (dashingPower);
        yield return new WaitForSeconds(dashingTime);
        //curHealth = healthreset;
        isDashing = false;
        //boxCollider.enabled = true;
        //tr.emitting = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

private IEnumerator DefaultDash()
{
    float speedX = Input.GetAxisRaw("Horizontal");
    float speedY = Input.GetAxisRaw("Vertical");
    Vector3 playerinput = new Vector3(speedX, speedY, 0);
    canDash = false;
    isDashing = true;
    //boxCollider.enabled = false;
    rb.velocity = new Vector3(transform.position.x * dashingPower2, transform.position.y * dashingPower2, 0);
    rb.velocity += rb.velocity * (dashingPower2);
    //transform.position += playerinput.normalized * ( * dashingPower2);
    //tr.emitting = true;
    //transform.position += playerinput.normalized * (dashingPower2);
    yield return new WaitForSeconds(dashingTime2);
    isDashing = false;
    //boxCollider.enabled = true;
    //tr.emitting = false;
    yield return new WaitForSeconds(dashingCooldown);
    canDash = true;
    }

 private IEnumerator InvulnerableDash()
 {
        float speedX = Input.GetAxisRaw("Horizontal");
        float speedY = Input.GetAxisRaw("Vertical");
        Vector3 playerinput = new Vector3(speedX, speedY, 0);
        canDash = false;
        //healthreset= curHealth;
        isDashing = true;
        //rb.detectCollisions = false;
        //boxCollider.enabled = false;
        //rb.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * (dashingPower), Input.GetAxisRaw("Vertical") * (dashingPower), 0);
        //transform.position += playerinput.normalized * ( * dashingPower2);
        //tr.emitting = true;
        transform.position += playerinput.normalized * (dashingPower2);
        yield return new WaitForSeconds(dashingTime2);
        //rb.detectCollisions = true;
        //curHealth = healthreset;
        isDashing = false;
        //boxCollider.enabled = true;
        //tr.emitting = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

private IEnumerator ReloadDash()
{
        float speedX = Input.GetAxisRaw("Horizontal");
        float speedY = Input.GetAxisRaw("Vertical");
        Vector3 playerinput = new Vector3(speedX, speedY, 0);
        canDash = false;
        isDashing = true;
        //boxCollider.enabled = false;
        //rb.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * (dashingPower), Input.GetAxisRaw("Vertical") * (dashingPower), 0);
        //transform.position += playerinput.normalized * ( * dashingPower2);
        //tr.emitting = true;
        transform.position += playerinput.normalized * (dashingPower2);
        weaponinfo.currentAmmo = weaponinfo.getMaxAmmo();
        //currentAmmo = getMaxAmmo();
        yield return new WaitForSeconds(dashingTime2);
        isDashing = false;
        //boxCollider.enabled = true;
        //tr.emitting = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
