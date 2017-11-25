using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFighting : EnemyState
{

	private EnemyController enemy;

	private string[] animationTriggers = { "Attack", "Attack2", "Attack3" };

	private int currentTrigger;

	private int numTriggers;

	private bool performingCombo = false;

	private int comboHits = -1;

	private bool canAttack;

	private float nextTimeToAttack;

	private bool setIdle;


	public EnemyFighting (EnemyController enemy)
	{
		this.enemy = enemy;
		currentTrigger = 0;
		performingCombo = false;
		comboHits = -1;
		canAttack = false;
		this.enemy.animator.SetBool ("Fighting", true);
		this.enemy.animator.SetTrigger ("FightingIdle");
		setIdle = true;
		nextTimeToAttack = Time.time + 0.5f * this.enemy.timeToAttack;
	}

	public override void Reset ()
	{
		currentTrigger = 0;
		performingCombo = false;
		comboHits = -1;
		canAttack = false;
		setIdle = true;
		nextTimeToAttack = Time.time + 0.5f * this.enemy.timeToAttack;
	}

	public override void Update ()
	{
		if (PlayerController.PlayerDead) {
			performingCombo = false;
			canAttack = false;
			comboHits = -1;
			currentTrigger = 0;
			setIdle = false;
			if (!setIdle) {
				enemy.animator.SetTrigger ("FightingIdle");
				setIdle = true;
			}
		} else {			
			if (Vector3.Distance (enemy.navMeshAgent.transform.position, PlayerController.PlayerTransform.position) > 3f) {			
				performingCombo = false;
				canAttack = false;
				comboHits = -1;
				currentTrigger = 0;
				this.enemy.animator.SetTrigger ("Chase");
				this.enemy.SetState (new Chasing (this.enemy));
			} else {
				enemy.LookAtPlayer ();
				if (!performingCombo) {
					if (Time.time >= nextTimeToAttack) {
						nextTimeToAttack = Time.time + enemy.timeToAttack;
						comboHits = Random.Range (1, 4);
						performingCombo = true;
						enemy.animator.SetTrigger (animationTriggers [currentTrigger]);
						setIdle = false;
						currentTrigger++;
						canAttack = false;
					} else {
						if (!setIdle) {
							enemy.animator.SetTrigger ("FightingIdle");
							setIdle = true;
						}
					}
				} else {
					if (currentTrigger < comboHits) {					
						if (canAttack) {
							enemy.animator.SetTrigger (animationTriggers [currentTrigger]);
							setIdle = false;
							currentTrigger++;
							canAttack = false;
						}
					} else {
						canAttack = false;
					}
				}
			}
		}
	}

	public override void HitFrameOpened ()
	{
		if (currentTrigger > 0) {
			enemy.hitboxController.EnableHitbox (animationTriggers [currentTrigger - 1]);
		}
	}

	public override void HitFrameClosed ()
	{
		if (currentTrigger > 0) {
			enemy.hitboxController.DisableHitbox (animationTriggers [currentTrigger - 1]);
		}
	}

	public override void AnimationFrameWindowClosed ()
	{
		if (currentTrigger > 0) {
			if (currentTrigger == comboHits) {
				comboHits = -1;
				currentTrigger = 0;
				performingCombo = false;
				canAttack = false;
				setIdle = false;
			} else {
				canAttack = true;
			}
		}
	}

	public override void AnimationFrameWindowOpened ()
	{
		return;
	}
}

