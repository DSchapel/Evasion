using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public EnemySpawnManager manager;
    public Enemy enemy;
    public float timeUntilSpawn;
    public Sprite[] frames;
    public GameObject[] particles;

    private struct ParticleInfo {
        public GameObject obj;
        public SpriteRenderer render;

        public ParticleInfo(GameObject obj) {
            this.obj = obj;

            render = obj.GetComponent<SpriteRenderer>();
        }
    }

    private enum State {
        Spawning,
        Spawned
    }

    private ParticleInfo[] infos;
    private State state;
    private float timer;

    void Start() {
        infos = particles.Select(r => new ParticleInfo(r)).ToArray();
        state = State.Spawning;
        timer = timeUntilSpawn;
    }

    void Update() {
        switch (state) {
            case State.Spawning:
                UpdateSpawning();
                break;
            case State.Spawned:
                UpdateSpawned();
                break;
        }
    }

    void UpdateSpawning() {
        timer -= Time.deltaTime;
        if (timer <= 0.0f) {
            var obj = Instantiate(enemy.prefab);
            obj.transform.position = transform.position;

            manager.AddEnemy(obj);
            state = State.Spawned;
            timer = 0.0f;
            return;
        }

        var sprite = frames[(int) (timer * frames.Length * 2.0f)];
        
        for (var i = 0; i < infos.Length; i++) {
            var info = infos[i];
            var angle = timer * Mathf.PI / 2.0f + i * Mathf.PI * 2.0f / frames.Length;

            var offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0.0f) * timer * 2.0f;
            info.obj.transform.position = transform.position + offset;

            info.render.sprite = sprite;
        }
    }

    void UpdateSpawned() {
        timer += Time.deltaTime;

        if (timer >= timeUntilSpawn) {
            Destroy(gameObject);
            return;
        }

        var sprite = frames[(int) (timer * frames.Length * 2.0f)];

        for (var i = 0; i < infos.Length; i++) {
            var info = infos[i];
            var angle = i * Mathf.PI * 2.0f / frames.Length;

            var offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0.0f) * Mathf.Sqrt(timer * 2.0f);
            info.obj.transform.position = transform.position + offset;

            info.render.sprite = sprite;
            info.render.color = new(1.0f, 1.0f, 1.0f, 0.5f - timer);
        }
    }
}
