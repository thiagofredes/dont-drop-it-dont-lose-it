using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUILifeController : MonoBehaviour
{

	public Image healthGauge;

	private EnemyController enemy;

	private float maxHealth;

	void Awake ()
	{
		enemy = GetComponentInParent<EnemyController> ();
		maxHealth = enemy.health;
	}

	void LateUpdate ()
	{
		healthGauge.fillAmount = enemy.health / maxHealth;
	}
}
