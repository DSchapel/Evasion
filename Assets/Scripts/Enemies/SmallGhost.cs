using UnityEngine;

public class SmallGhost : MonoBehaviour {
    public Sprite[] spawnFrames;
    public Sprite[] frames;
    public float spawnTime;

    private SpriteRenderer render;
    private float timer;

    void Start() {
        render = GetComponent<SpriteRenderer>();
        render.sprite = spawnFrames[0];

        timer = 0.0f;

    }

    void Update() {
        timer += Time.deltaTime;

        if (timer < spawnTime) {
            int frame = (int) (timer * spawnFrames.Length / spawnTime);
            Debug.Assert(frame < spawnFrames.Length);

            render.sprite = spawnFrames[frame];
        } else {
            int frame = (int) ((timer - spawnTime) * frames.Length);
            frame %= frames.Length;

            render.sprite = frames[frame];
        }
    }
}
