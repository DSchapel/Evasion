using UnityEngine;

public class GooBullet : MonoBehaviour {
    private enum State {
        Spawning,
        Spawned
    }

    public Sprite spawnFrame;
    public Sprite[] frames;

    private SpriteRenderer render;

    private State state;
    private float timer;

    void Start() {
        render = GetComponent<SpriteRenderer>();
        render.sprite = spawnFrame;

        state = State.Spawning;
        timer = 0.0f;
    }

    void Update() {
        timer += Time.deltaTime;

        if (state == State.Spawning && timer >= 0.15f) {
            timer -= 0.15f;
            state = State.Spawned;
        }

        switch (state) {
            case State.Spawning:
                render.sprite = spawnFrame;
                break;
            case State.Spawned:
                render.sprite = frames[(int) (3.0f * timer) % frames.Length];
                break;
        }
    }
}
