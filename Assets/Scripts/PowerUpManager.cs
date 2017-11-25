using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{

	public GameObject[] powerups;

	private int numPowerUps;

	private PlayerController player;

	private static PowerUpManager _instance;

	private int nextPowerUp;

	void Awake ()
	{
		_instance = this;
		numPowerUps = powerups.Length;
		player = FindObjectOfType<PlayerController> ();
		nextPowerUp = Random.Range (0, numPowerUps);
	}

	public static void SpawnPowerUp (Vector3 position)
	{		
		Instantiate<GameObject> (_instance.powerups [_instance.nextPowerUp], position, Quaternion.identity);
		_instance.nextPowerUp = Random.Range (0, _instance.numPowerUps);
	}

}
