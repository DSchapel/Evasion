using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawnManager : MonoBehaviour {
	[Header("Enemy Spawn Info")]
	public float safeRange;
	public float timeBetweenSpawns;
	public Collider2D spawnRegion;
	public Enemy[] spawns;

	[Header("Wave Info")]
	public float timeBetweenWaves;
	public int wavesBetweenUpgrades;
	public float timeForUpgrade;

	[Header("Events")]
	public UnityEvent TimeForUpgrade;

	private Queue<Enemy> pendingSpawns;
	private List<GameObject> aliveEnemies;

	private int wave;
	private float nextSpawnTimer;
	private float nextWaveTimer;

	void Start() {
		TimeForUpgrade = new();
		pendingSpawns = new();
		aliveEnemies = new();

		wave = 1;
		nextSpawnTimer = 0.0f;
		nextWaveTimer = timeBetweenWaves;

		ChooseEnemies();
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

	private float PowerFunction(float x) {
		return (x + Mathf.Pow(x, 1.25f) + Mathf.Pow(1.1f, x)) / 3.0f;
	}

	private void SpawnEnemy(Enemy enemy) {
		var spawnPos = Vector3.zero;
		while (true) {
			spawnPos = new(
				Random.Range(spawnRegion.bounds.min.x, spawnRegion.bounds.max.x),
				Random.Range(spawnRegion.bounds.max.y, spawnRegion.bounds.max.y),
				0
			);
			
			if ((spawnPos - transform.position).magnitude > safeRange) {
				break;
			}
		}
		
		var obj = Instantiate(enemy.spawner ?? enemy.prefab);
		obj.transform.position = spawnPos;
		
		if (enemy.spawner != null) {
			var component = obj.GetComponent<EnemySpawner>();
			component.enemy = enemy;
			component.manager = this;
		}

		aliveEnemies.Add(obj);
	}

	private void ChooseEnemies() {
		var power = PowerFunction((float) wave);

		var spawnsLeft = spawns.Select(e => e.limit).ToArray();

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

		var totalSpawnableEnemies = new List<int>();
		var totalSpawnableWeight = 0.0f;
		for (int i = 0; i < spawns.Length; i++) {
			if (canSpawnEnemy(i)) {
				totalSpawnableEnemies.Add(i);
				totalSpawnableWeight += spawns[i].weight;
			}
		}
		var spawnableEnemies = new List<int>(totalSpawnableEnemies);

		int chooseEnemy() {
			var weight = Random.Range(0.0f, totalSpawnableWeight);

			foreach (var i in spawnableEnemies) {
				weight -= spawns[i].weight;
				if (weight > 0) {
					continue;
				}

				return i;
			}

			if (spawnableEnemies.Count != 0) {
				Debug.Log("Did not pick enemy even though one could spawn!");
			}
			return -1;
		}

		while (power > 0.0f) {
			var index = chooseEnemy();
			if (index < 0) {
				spawnsLeft = spawns.Select(e => e.limit).ToArray();
				spawnableEnemies = new List<int>(totalSpawnableEnemies);

				index = chooseEnemy();
				if (index < 0) {
					return;
				}
			}

			var enemy = spawns[index];

			power -= enemy.power;
			spawnsLeft[index] -= 1;

			if (!canSpawnEnemy(index)) {
				spawnableEnemies.Remove(index);
				totalSpawnableWeight -= enemy.weight;
			}

			pendingSpawns.Enqueue(enemy);
		}
	}

	public int Wave { get { return wave; }}

	public void AddEnemy(GameObject enemy) {
		aliveEnemies.Add(enemy);
	}
}