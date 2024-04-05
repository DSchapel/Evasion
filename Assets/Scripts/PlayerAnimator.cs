using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {
    private enum State {
        Idle,
        Run,
        Hit
    }

    public Sprite[] idleFrames;
    public Sprite[] runFrames;
    public Sprite hitFrame;

    private SpriteRenderer render;
    private Camera cam;
    private Vector3 lastPos;
    private State state;
    private float timer;

    void Start() {
        render = GetComponent<SpriteRenderer>();
        cam = Camera.main;
        state = State.Idle;
        timer = 0.0f;
    }

    void Update() {
        State newState = state;

        var distance = (transform.position - lastPos) / Time.deltaTime;
        lastPos = transform.position;

        if (distance.magnitude < 0.1f) {
            newState = State.Idle;
        }

        var moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (moveDirection.magnitude >= 0.9f) {
            newState = State.Run;
        }

        if (newState != state) {
            state = newState;
            timer = 0.0f;
        }

        var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        
        if (transform.position.x != mousePos.x) {
            render.flipX = transform.position.x > mousePos.x;
        }

        switch (state) {
        case State.Idle:
            UpdateIdle();
            break;
        case State.Run:
            UpdateRun(moveDirection, distance);
            break;
        case State.Hit:
            // TODO: not implementing this one yet
            break;
        }
    }

    void UpdateIdle() {
        timer += Time.deltaTime * 2.0f;
        var frame = (int) (timer % idleFrames.Length);
        
        render.sprite = idleFrames[frame];
    }

    void UpdateRun(Vector2 moveDirection, Vector2 distance) {
        var timerOffset = Time.deltaTime * distance.magnitude * 1.75f;

        if (moveDirection.x >= 0.0f) {
            timer += timerOffset;
        } else {
            timer -= timerOffset;
        }
    
        var frame = (int) negativeMod(timer, runFrames.Length);
        render.sprite = runFrames[frame];
    }

    // modulo function that doesn't explode the
    // second it sees a negative number
    float negativeMod(float a, float b) {
        return a - b * Mathf.Floor(a / b);
    }
}
