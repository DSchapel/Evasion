using UnityEngine;

public class WitchBullet : MonoBehaviour {
    public Vector2 velocity;
    private float timer = 10.0f;

    private SpriteRenderer render;

    void Start() {
        render = GetComponent<SpriteRenderer>();
    }

    void Update() {
        transform.position += (Vector3) velocity;

        if (timer >= 10.0f) {
            Destroy(gameObject);
        } else if (timer > 9.0f) {
            float transparency = timer - 9.0f;
            render.color = new(1.0f, 1.0f, 1.0f, 1.0f - transparency);
        }
    }
}
