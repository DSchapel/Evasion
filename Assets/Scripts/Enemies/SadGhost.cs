using UnityEngine;

public class SadGhost : MonoBehaviour {
    public Sprite[] frames;
    public GameObject eyes;

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

        Vector2 pos = transform.position;
        Vector2 playerPos = player.transform.position;

        Vector2 offsetToPlayer = playerPos - pos;
        Vector2 direction = offsetToPlayer.normalized;

        Vector2 offset;
        if (direction.magnitude > offsetToPlayer.magnitude) {
            offset = offsetToPlayer;
        } else {
            offset = direction;
        }
        
        eyes.transform.position = pos + offset * 0.06f;
    }
}
