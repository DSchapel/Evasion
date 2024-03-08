using System;
using UnityEngine;

public class Switchable : MonoBehaviour {
    private static Color BG_COLOR = new(0.75f, 0.9f, 1.0f, 0.5f);

    public enum Side {
        Foreground,
        Background
    }

    public Side side;

    private SpriteRenderer sprite;

    private GameObject tracking;
    private Switchable trackingSwitchable;

    // Start is called before the first frame update
    void Start() {
        sprite = GetComponent<SpriteRenderer>();

        tracking = GameObject.FindGameObjectWithTag("Player");
        trackingSwitchable = tracking.GetComponent<Switchable>();

        if (side == trackingSwitchable.side) {
            sprite.color = Color.white;
            gameObject.layer = 0;
        } else {
            sprite.color = BG_COLOR;

            // TODO: i don't think this is actually working?
            // i'm not sure how else to get background objects
            // to render behind everything else
            gameObject.layer = 1;
        }
    }

    // Update is called once per frame
    void Update() {
        Color targetColor = Color.white;
        gameObject.layer = 0;
        if (side != trackingSwitchable.side) {
            targetColor = BG_COLOR;
            gameObject.layer = 1;
        }

        // TODO: this algorithm is framerate dependant, which
        // sucks really bad
        // (i think this is a pretty easy fix but i don't wanna deal
        //  with it right now)
        sprite.color = Color.Lerp(sprite.color, targetColor, 0.25f);
    }

    void OnTriggerEnter2D(Collider2D other) {
        GameObject gameObject = other.gameObject;

        if (gameObject.tag != "Portal") {
            return;
        }

        Switchable switchable = gameObject.GetComponent<Switchable>();

        if (switchable == null || side != switchable.side) {
            return;
        }

        switch (switchable.side) {
        case Side.Foreground:
            side = Side.Background;
            break;
        case Side.Background:
            side = Side.Foreground;
            break;
        }
    }
}
