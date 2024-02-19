using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switchable : MonoBehaviour {
    public enum Side {
        Foreground,
        Background
    }
    public Side side;

    private SpriteRenderer sprite;

    private GameObject shadow;
    private SpriteRenderer shadowSprite;

    // Start is called before the first frame update
    void Start() {
        switch (side) {
        case Side.Foreground:
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                0.0f
            );
            break;
        case Side.Background:
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                1.0f
            );
            break;
        }

        sprite = GetComponent<SpriteRenderer>();

        shadow = new GameObject("Shadow");
        
        shadow.transform.rotation = transform.rotation;
        shadow.transform.localScale = transform.localScale;

        shadow.transform.SetParent(transform);

        shadowSprite = shadow.AddComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update() {

        float targetZ = 0.0f;
        if (side == Side.Background) {
            targetZ += 1.0f;
        }
        
        // TODO: this algorithm is framerate dependant, which
        // sucks really bad
        // (i think this is a pretty easy fix but i don't wanna deal
        //  with it right now)
        transform.position += new Vector3(0, 0, (targetZ - transform.position.z) / 4);

        shadow.transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            1.0f
        );

        shadowSprite.sprite = sprite.sprite;
        shadowSprite.color = sprite.color * new Color(0.8f, 1.0f, 0.9f, 0.1f);
    }
}
