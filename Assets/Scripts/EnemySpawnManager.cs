using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawnManager : MonoBehaviour {
	[Header("Enemy Spawn Info")]
	public float minSpawnRange;
	public float maxSpawnRange;
	public float timeBetweenSpawns;
	public Enemy[] spawns;

	[Header("Wave Info")]
	public float timeBetweenWaves;
	public int wavesBetweenUpgrades;
	public float timeForUpgrade;

	private Queue<Enemy> pendingSpawns;
	private List<GameObject> aliveEnemies;

	private int wave;
	private float nextSpawnTimer;
	private float nextWaveTimer;
	private float totalPossibleWeight;

	void Start() {
		pendingSpawns = new();
		aliveEnemies = new();

		wave = 1;
		nextSpawnTimer = 0.0f;
		nextWaveTimer = timeBetweenWaves;

		totalPossibleWeight = 0.0f;
		foreach (var enemy in spawns) {
			totalPossibleWeight += enemy.weight;
		}	
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

		if (wave % wavesBetweenUpgrades == 0) {
			// TODO: i don't remember the consensus on this,
			// but we may want to pause wave spawning here
			// until the player picks an upgrade?
			//
			// either way we can't really do that until the
			// upgrade choosing system is done..
			TimeForUpgrade.Invoke();
			nextWaveTimer = timeForUpgrade;
		} else {
		}
	}

	private void SpawnEnemy(Enemy enemy) {
		var vec = (Vector3) UnityEngine.Random.insideUnitCircle;
		var unit = vec.normalized;

		var range = maxSpawnRange - minSpawnRange;
		var offset = unit * minSpawnRange + vec * range;
		
		var obj = Instantiate(enemy.spawner ?? enemy.prefab);
		obj.transform.position = transform.position + offset;
		
		if (enemy.spawner != null) {
			var component = obj.GetComponent<EnemySpawner>();
			component.enemy = enemy;
			component.manager = this;
		}

		aliveEnemies.Add(obj);
	}

	private void ChooseEnemies() {
		// TODO: this is probably a *horrible* power
		// function, we'll need to properly test this
		// and pick a nice one
		var power = (float) wave;

		var spawnsLeft = spawns.Select(e => e.limit).ToArray();
		var totalWeight = totalPossibleWeight;

		bool canSpawnEnemy(int index) {
			Enemy enemy = spawns[index];

			if (enemy.power > power) {
				return false;
			}

			if (spawnsLeft[index] <= 0) {
				return false;
			}

			return true;
		}

		int chooseEnemy() {
			var weight = UnityEngine.Random.Range(0.0f, totalWeight);

			for (var i = 0; i < spawns.Length; i++) {
				if (!canSpawnEnemy(i)) {
					continue;
				}

				weight -= spawns[i].weight;
				if (weight > 0) {
					continue;
				}

				return i;
			}

			return -1;
		}

		while (power > 0.0f) {
			var index = chooseEnemy();
			if (index < 0) {
				spawnsLeft = spawns.Select(e => e.limit).ToArray();
				totalWeight = totalPossibleWeight;

				index = chooseEnemy();
				if (index < 0) {
					return;
				}
			}

			var enemy = spawns[index];

			power -= enemy.power;
			spawnsLeft[index] -= 1;

			if (!canSpawnEnemy(index)) {
				totalWeight -= enemy.weight;
			}

			pendingSpawns.Enqueue(enemy);
		}
	}

	public int Wave { get { return wave; }}
	public UnityEvent TimeForUpgrade;

	public void AddEnemy(GameObject enemy) {
		aliveEnemies.Add(enemy);
	}
}