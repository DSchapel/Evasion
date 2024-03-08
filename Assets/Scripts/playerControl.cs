using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{

    public float movSpeed;
    float speedX, speedY;
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
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
        float speedX = Input.GetAxisRaw("Horizontal") * movSpeed;
        float speedY = Input.GetAxisRaw("Vertical") * movSpeed;
        Vector3 vector = new Vector3(speedX, speedY, 0);

        transform.position += Vector3.Normalize(vector) * Time.deltaTime * 6;
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
