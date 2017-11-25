using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDead : EnemyState
{

	private EnemyController enemy;

	public EnemyDead (EnemyController enemy)
	{
		this.enemy = enemy;
		this.enemy.enemyCollider.enabled = false;
		this.enemy.navMeshAgent.enabled = false;
		this.enemy.characterController.enabled = false;
		this.enemy.animator.SetTrigger ("Die");
		GameObject.Destroy (this.enemy.uiController.gameObject);
		this.enemy.StartCoroutine (Blink ());
	}

	private IEnumerator Blink ()
	{
		float blinkTime = 0.5f;
		yield return new WaitForSeconds (5f);
		while (blinkTime > 0) {
			enemy.rendererManager.ToggleRenderer ();
			yield return new WaitForSeconds (blinkTime);
			blinkTime -= 3 * Time.deltaTime;
		}
		enemy.rendererManager.SetRenderer (false);
		if (Random.value > 0.5f)
			PowerUpManager.SpawnPowerUp (this.enemy.transform.position);
		GameObject.Destroy (this.enemy.gameObject.transform.parent.gameObject);
	}

	public override void AnimationFrameWindowClosed ()
	{
	}

	public override void AnimationFrameWindowOpened ()
	{
	}

	public override void HitFrameClosed ()
	{
	}

	public override void HitFrameOpened ()
	{
	}

	public override void Reset ()
	{
	}

	public override void Update ()
	{
		
	}
}
