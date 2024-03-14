using UnityEngine;

public class AngyGhost : MonoBehaviour {
    public Sprite[] frames;
    public Sprite[] chargeFrames;

    private SpriteRenderer render;
    private chargingGhostChase ai;
    private float timer;

    private GameObject player;

    void Start() {
        render = GetComponent<SpriteRenderer>();
        ai = GetComponent<chargingGhostChase>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() {
        if (ai.IsCharging()) {
            timer += Time.deltaTime * 3.0f;

            int frame = (int) (timer * chargeFrames.Length);
            frame %= chargeFrames.Length;

            render.sprite = chargeFrames[frame];
        } else {
            timer += Time.deltaTime * 1.5f;

            if (ai.IsPausing()) {
                float anger = 1.0f - (ai.PauseTimeLeft() / ai.pauseTime);

                timer += anger * Time.deltaTime * 1.5f;
            }

            int frame = (int) (timer * frames.Length);
            frame %= frames.Length;

            render.sprite = frames[frame];
            render.flipX = transform.position.x > player.transform.position.x;
        }
    }
}
