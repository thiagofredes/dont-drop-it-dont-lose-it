using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawnManager : MonoBehaviour
{

	public Transform[] spawnPoints;

	public GameObject[] enemies;

	public int minEnemiesToSpawn = 2;

	public int maxEnemiesToSpawn = 4;

	private PlayerController player;

	private int numSpawnPoints;

	private int numEnemyTypes;

	private int enemiesSpawned;

	private int[] currentSpawnPoints;

	private int[] usableSpawnPoints;

	void Awake ()
	{
		EnemyController.EnemyDead += OnEnemyDead;
		player = FindObjectOfType<PlayerController> ();
		numSpawnPoints = spawnPoints.Length;
		numEnemyTypes = enemies.Length;
		usableSpawnPoints = new int[numSpawnPoints];
	}

	void OnDisable ()
	{
		EnemyController.EnemyDead -= OnEnemyDead;
	}

	void Start ()
	{
		Spawn ();
	}

	private void OnEnemyDead ()
	{
		enemiesSpawned--;
		if (enemiesSpawned == 0) {
			Spawn ();
		}
	}

	public void Spawn (int enemyType = -1)
	{
		enemiesSpawned = Random.Range (minEnemiesToSpawn, maxEnemiesToSpawn);
		currentSpawnPoints = new int[enemiesSpawned];
		ArrayExtensions.Range (usableSpawnPoints, 0, numSpawnPoints, 1);
		for (int e = 0; e < enemiesSpawned; e++) {
			int point = FarthestPoint ();
			currentSpawnPoints [e] = point;
			usableSpawnPoints [point] = -1;
			if (enemyType != -1) {
				GameObject.Instantiate (enemies [enemyType], spawnPoints [point].position, Quaternion.identity);
			} else {
				GameObject.Instantiate (enemies [Random.Range (0, numEnemyTypes)], spawnPoints [point].position, Quaternion.identity);
			}
		}
	}

	private int FarthestPoint ()
	{
		float maxAngle = 0f;
		int farthest = 0;
		for (int p = 0; p < numSpawnPoints; p++) {
			if (usableSpawnPoints [p] != -1) {
				float currAngle = Vector3.Angle (spawnPoints [p].position - player.transform.position, player.transform.forward);
				if (currAngle > maxAngle) {
					maxAngle = currAngle;
					farthest = p;
				}
			}
		}
		return farthest;
	}
}
