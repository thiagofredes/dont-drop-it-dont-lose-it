using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUILifeController : MonoBehaviour
{

	public Text healthNumber;

	public Image healthGauge;

	private PlayerController player;

	private float maxHealth;

	void Awake ()
	{
		player = FindObjectOfType<PlayerController> ();
		maxHealth = player.health;
	}

	void LateUpdate ()
	{
		healthNumber.text = player.health.ToString ("##");
		healthGauge.fillAmount = player.health / maxHealth;
	}
}
