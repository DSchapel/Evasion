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
    public float dashingPower2 = 2f;
    public float dashingTime2 = 0.3f;
    public float dashingCooldown = 1f;
    public int dashtype = 3;
    public Health healthval;
    public Weapon weaponinfo;
    //public int currentAmmo;
    //public int currentAmmo;
    //public int healthreset;
    //[SerializeField] WeaponData data;
    //public int curHealth;
    //[SerializeField] private TrailRenderer tr;
    Rigidbody2D rb;



    // Start is called before the first frame update
    void Start()
    {
        //weaponinfo= GetComponent<Weapon.cs>();
        //healthval = GetComponent<Health.cs>();
        weaponinfo= GameObject.FindObjectOfType(typeof(Weapon)) as Weapon;
        healthval = GameObject.FindObjectOfType(typeof(Health)) as Health;
        rb = GetComponent<Rigidbody2D>();
        //curHealth = GameObject.FindGameObjectWithTag("curHealth").GetComponent<int>();
        //data= GameObject.FindGameObjectWithTag("data").GetComponent<WeaponData>();
        //currentAmmo = GameObject.FindGameObjectWithTag("currentAmmo").GetComponent<int>();
        //private GameObject player;
        //player = GameObject.Find("Player");
        //BoxCollider boxCollider = player.GetComponent<BoxCollider>();

        canDash = true;
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
            StartCoroutine(TeleportDash());
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


    }
    private IEnumerator TeleportDash()
    {
        float speedX = Input.GetAxisRaw("Horizontal");
        float speedY = Input.GetAxisRaw("Vertical");
        Vector3 playerinput = new Vector3(speedX, speedY, 0);
        canDash = false;
        //healthreset = curHealth;
        isDashing = true;
        healthval.Healthholder();
        //boxCollider.enabled = false;
        //rb.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * (dashingPower), Input.GetAxisRaw("Vertical") * (dashingPower), 0);
        transform.position += playerinput.normalized * (dashingPower);
        //tr.emitting = true;
        //rb.velocity = playerinput.normalized * (dashingPower);
        yield return new WaitForSeconds(dashingTime);
        //curHealth = healthreset;
        healthval.Healthreset();
        isDashing = false;
        //boxCollider.enabled = true;
        //tr.emitting = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

private IEnumerator DefaultDash()
{
    speedX = Input.GetAxisRaw("Horizontal");
    speedY = Input.GetAxisRaw("Vertical");
    Vector3 playerinput = new Vector3(speedX, speedY, 0);
    canDash = false;
    isDashing = true;
    //boxCollider.enabled = false;
    rb.velocity = playerinput*dashingPower2;
    //rb.velocity = rb.velocity * ;
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
        healthval.Healthholder();
        //rb.detectCollisions = false;
        //boxCollider.enabled = false;
        //rb.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * (dashingPower), Input.GetAxisRaw("Vertical") * (dashingPower), 0);
        //transform.position += playerinput.normalized * ( * dashingPower2);
        //tr.emitting = true;
        transform.position += playerinput.normalized * (dashingPower2);
        yield return new WaitForSeconds(dashingTime2);
        //rb.detectCollisions = true;
        //curHealth = healthreset;
        healthval.Healthreset();
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
        weaponinfo.instantReload();
        //currentAmmo = getMaxAmmo();
        yield return new WaitForSeconds(dashingTime2);
        isDashing = false;
        //boxCollider.enabled = true;
        //tr.emitting = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
