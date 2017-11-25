using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Patrolling : EnemyState
{
	private EnemyController enemy;

	private int currentPatrolPoint = 0;

	private int numPatrolPoints;

	private Coroutine patrolCoroutine;

	public Patrolling (EnemyController enemy)
	{
		Debug.Log ("Patrolling");
		this.enemy = enemy;
		numPatrolPoints = this.enemy.patrolPoints.Length;
		this.enemy.navMeshAgent.speed = this.enemy.patrollingSpeed;
		this.enemy.navMeshAgent.acceleration = this.enemy.patrollingAcceleration;
		this.enemy.navMeshAgent.enabled = true;
		patrolCoroutine = this.enemy.StartCoroutine (Patrol ());
		this.enemy.animator.SetFloat ("MoveSpeed", 0.5f);
		this.enemy.animator.SetBool ("Fighting", false);
	}

	public override void Reset ()
	{
		this.enemy.navMeshAgent.enabled = true;
		//this.enemy.characterController.enabled = false;
		if (patrolCoroutine != null)
			this.enemy.StopCoroutine (patrolCoroutine);
		patrolCoroutine = this.enemy.StartCoroutine (Patrol ());
		this.enemy.animator.SetFloat ("MoveSpeed", 0.5f);
		this.enemy.animator.SetBool ("Fighting", false);
	}

	public override void AnimationFrameWindowClosed ()
	{
	}

	public override void AnimationFrameWindowOpened ()
	{
	}

	public override void Update ()
	{
		if (!PlayerController.PlayerDead) {
			Vector3 playerPositionVector = PlayerController.PlayerTransform.position - enemy.navMeshAgent.transform.position;
			float angle = Vector3.Angle (enemy.navMeshAgent.transform.forward, playerPositionVector);
			if (Mathf.Abs (angle) < enemy.patrollingFOV) {
				if (playerPositionVector.magnitude < enemy.patrollingFOVDistanceToFighting) {
					enemy.SetState (new EnemyFighting (this.enemy));
				} else if (playerPositionVector.magnitude < enemy.patrollingFOVDistance) {
					enemy.SetState (new Chasing (this.enemy));
				}
			}
		}
	}

	private IEnumerator Patrol ()
	{		
		YieldInstruction endOfFrame = new WaitForEndOfFrame ();
		while (true) {
			enemy.navMeshAgent.SetDestination (enemy.patrolPoints [currentPatrolPoint].position);
			while (enemy.navMeshAgent.pathPending) {
				yield return endOfFrame;
			}
			while (Vector3.Distance (enemy.navMeshAgent.transform.position, enemy.patrolPoints [currentPatrolPoint].position) > 0.5f) {
				yield return endOfFrame;
			}
			currentPatrolPoint = (currentPatrolPoint + 1) % numPatrolPoints;
		}
	}

	public override void HitFrameOpened ()
	{
		
	}

	public override void HitFrameClosed ()
	{
		
	}
}
