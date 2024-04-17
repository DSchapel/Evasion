using UnityEngine;

public class MedicalZombie : MonoBehaviour {
    public Sprite[] frames;

    private GameObject player;
    private SpriteRenderer render;
    private float timer;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        render = GetComponent<SpriteRenderer>();
    }

    void Update() {
        timer += Time.deltaTime;
        render.sprite = frames[(int) (timer * 4.0f) % frames.Length];

        render.flipX = player.transform.position.x < transform.position.x;
    }
}
