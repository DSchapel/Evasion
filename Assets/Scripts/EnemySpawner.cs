using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	[Header("Enemy Spawn Info")]
	public float minSpawnRange;
	public float maxSpawnRange;
	public float timeBetweenSpawns;
	public Enemy[] spawns;

	[Header("Wave Info")]
	public float timeBetweenWaves;

	private Queue<Enemy> pendingSpawns;
	private List<GameObject> aliveEnemies;

	private int wave;
	private float nextSpawnTimer;
	private float nextWaveTimer;

	void Start() {
		pendingSpawns = new();
		aliveEnemies = new();

		wave = 1;
		nextSpawnTimer = 0.0f;
		nextWaveTimer = timeBetweenWaves;
	}

	void Update() {
		aliveEnemies.RemoveAll(o => !o);

		if (pendingSpawns.Count > 0) {
			nextSpawnTimer -= Time.deltaTime;
			if (nextSpawnTimer > 0.0f) {
				return;
			}

			SpawnEnemy(pendingSpawns.Dequeue());

			if (pendingSpawns.Count > 0) {
				nextSpawnTimer = timeBetweenSpawns;
			} else {
				nextSpawnTimer = 0.0f;
			}
			return;
		}

		if (aliveEnemies.Count > 0) {
			return;
		}

		nextWaveTimer -= Time.deltaTime;
		if (nextWaveTimer > 0.0f) {
			return;
		}

		ChooseEnemies();

		wave += 1;
		nextWaveTimer = timeBetweenWaves;
	}

	private void SpawnEnemy(Enemy enemy) {
		var vec = (Vector3) Random.insideUnitCircle;
		var unit = vec.normalized;

		var range = maxSpawnRange - minSpawnRange;
		var offset = unit * minSpawnRange + vec * range;
		
		var obj = Instantiate(enemy.prefab);
		obj.transform.position = transform.position + offset;

		aliveEnemies.Add(obj);
	}

	private void ChooseEnemies() {
		// TODO: this is probably a *horrible* power
		// function, we'll need to properly test this
		// and pick a nice one
		var power = (float) wave;
		Debug.Log(power);

		var options = spawns.ToList();
		while (power > 0.0f) {
			var totalWeight = 0.0f;
			options.RemoveAll(e => {
				if (e.power > power) {
					return true;
				}

				totalWeight += e.weight;
				return false;
			});

			var weight = Random.Range(0.0f, totalWeight);
			var enemy = options.Find(enemy => {
				weight -= enemy.weight;
				if (weight > 0.0f) {
					return false;
				}

				return true;
			});

			if (!enemy) {
				return;
			}

			power -= enemy.power;
			pendingSpawns.Enqueue(enemy);
		}
	}

	public int Wave { get { return wave; }}
}