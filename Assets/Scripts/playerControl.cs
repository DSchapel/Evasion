using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    public float movSpeed;

    void Start() {}

    void Update() {
        float speedX = Input.GetAxisRaw("Horizontal")*movSpeed;
        float speedY = Input.GetAxisRaw("Vertical")*movSpeed;
        Vector3 vector = new Vector3(speedX, speedY, 0);
        
        transform.position += Vector3.Normalize(vector) * Time.deltaTime * 6;
    }
}
