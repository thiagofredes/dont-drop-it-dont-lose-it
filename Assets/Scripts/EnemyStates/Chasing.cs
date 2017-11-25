using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chasing : EnemyState
{

	private EnemyController enemy;

	private Coroutine followPlayerCoroutine;

	public Chasing (EnemyController enemy)
	{
		this.enemy = enemy;
		this.enemy.navMeshAgent.speed = this.enemy.chasingSpeed;
		this.enemy.navMeshAgent.acceleration = this.enemy.chasingAcceleration;
		this.enemy.animator.SetFloat ("MoveSpeed", 1f);
		this.enemy.animator.SetBool ("Fighting", false);
		followPlayerCoroutine = this.enemy.StartCoroutine (FollowPlayer ());
	}

	public override void Reset ()
	{
		this.enemy.navMeshAgent.speed = this.enemy.chasingSpeed;
		this.enemy.navMeshAgent.acceleration = this.enemy.chasingAcceleration;
		this.enemy.animator.SetFloat ("MoveSpeed", 1f);
		this.enemy.animator.SetBool ("Fighting", false);
		if (followPlayerCoroutine != null) {
			enemy.StopCoroutine (followPlayerCoroutine);
		}
		followPlayerCoroutine = this.enemy.StartCoroutine (FollowPlayer ());
	}

	private IEnumerator FollowPlayer ()
	{
		YieldInstruction endOfFrame = new WaitForEndOfFrame ();
		Vector3 lookVector;
		while (true) {
			enemy.navMeshAgent.SetDestination (PlayerController.PlayerTransform.position);
			enemy.navMeshAgent.updateRotation = false;
			while (enemy.navMeshAgent.pathPending) {
				yield return endOfFrame;
			}
			if (enemy.navMeshAgent.desiredVelocity != Vector3.zero) {
				lookVector = enemy.navMeshAgent.desiredVelocity;
			} else {
				lookVector = PlayerController.PlayerTransform.position - enemy.navMeshAgent.transform.position;
			}
			lookVector.y = 0f;
			enemy.transform.rotation = Quaternion.Slerp (enemy.transform.rotation, Quaternion.LookRotation (lookVector), Time.deltaTime * enemy.navMeshAgent.angularSpeed);
			yield return endOfFrame;
		}
	}

	public override void AnimationFrameWindowClosed ()
	{
		
	}

	public override void AnimationFrameWindowOpened ()
	{
		
	}

	public override void Update ()
	{
		if (Vector3.Distance (enemy.transform.position, PlayerController.PlayerTransform.position) < 2f) {
			enemy.StopCoroutine (followPlayerCoroutine);
			enemy.navMeshAgent.ResetPath ();
			enemy.navMeshAgent.velocity = Vector3.zero;
			enemy.SetState (new EnemyFighting (this.enemy));
		}
	}

	public override void HitFrameOpened ()
	{
		
	}

	public override void HitFrameClosed ()
	{
		
	}
}
