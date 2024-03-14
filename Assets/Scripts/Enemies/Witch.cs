using UnityEngine;

public class Witch : MonoBehaviour {
    public Sprite[] frames;
    public GameObject hand;

    private SpriteRenderer render;
    private float timer;

    private GameObject player;

    void Start() {
        render = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() {
        timer += Time.deltaTime;
        
        int frame = (int) (timer * frames.Length);
        frame %= frames.Length;

        Sprite sprite = frames[frame];

        render.sprite = sprite;
        render.flipX = transform.position.x > player.transform.position.x;

        Vector2 pos = transform.position;
        pos += Vector2.down * 0.1f;

        if (render.flipX) {
            pos += Vector2.left * 0.1f;
        } else {
            pos += Vector2.right * 0.1f;
        }

        Vector2 playerPos = player.transform.position;

        Vector2 offsetToPlayer = playerPos - pos;
        Vector2 direction = offsetToPlayer.normalized;

        Vector2 offset;
        if (direction.magnitude > offsetToPlayer.magnitude) {
            offset = offsetToPlayer;
        } else {
            offset = direction;
        }

        hand.transform.position = pos + offset * 0.1f;
    }
}
