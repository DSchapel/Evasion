using UnityEngine;

public class Switchable : MonoBehaviour {
    private static Color BG_COLOR = new(0.75f, 0.9f, 1.0f, 0.5f);

    public enum Side {
        Foreground,
        Background
    }

    public Side side;

    public bool castsShadow;

    // TODO: is there a way to have all instances
    // of this script just *know* the shadow material?
    public Material shadowMaterial;

    private SpriteRenderer sprite;

    private GameObject shadow;

    // TODO: i think this is genuinely the *worst* possible
    // implementation of this, but idk how else to do it
    public GameObject tracking;
    private Switchable trackingSwitchable;

    private SpriteRenderer shadowSprite;

    // Start is called before the first frame update
    void Start() {
        sprite = GetComponent<SpriteRenderer>();
        trackingSwitchable = tracking.GetComponent<Switchable>();

        if (side == trackingSwitchable.side) {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                0.0f
            );

            sprite.color = Color.white;
        } else {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                1.0f
            );
        }

        shadow = new GameObject("Shadow");
        
        shadow.transform.rotation = transform.rotation;
        shadow.transform.localScale = transform.localScale;

        shadow.transform.SetParent(transform);

        shadowSprite = shadow.AddComponent<SpriteRenderer>();
        shadowSprite.material = shadowMaterial;
    }

    // Update is called once per frame
    void Update() {
        float targetZ = 0.0f;
        Color targetColor = Color.white;
        if (side != trackingSwitchable.side) {
            targetZ += 1.0f;
            targetColor = BG_COLOR;
        }

        // TODO: this algorithm is framerate dependant, which
        // sucks really bad
        // (i think this is a pretty easy fix but i don't wanna deal
        //  with it right now)

        // BONUS TODO: the player controller is using a rigidbody,
        // so it completely ignores this offset :(
        // (i do not know how to fix this lol)
        transform.position += new Vector3(0, 0, (targetZ - transform.position.z) / 4);
        sprite.color = Color.Lerp(sprite.color, targetColor, 0.25f);

        if (castsShadow) {
            shadow.transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                1.0f
            );

            shadowSprite.sprite = sprite.sprite;
            shadowSprite.enabled = true;
        } else {
            shadowSprite.enabled = false;
        }
    }
}
