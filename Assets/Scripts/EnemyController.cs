using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyController : MonoBehaviour, IDamageable, IDamaging
{
	public static event Action EnemyDead;

	public NavMeshAgent navMeshAgent;

	public Collider enemyCollider;

	public Animator animator;

	public CharacterController characterController;

	[HideInInspector]
	public Transform[] patrolPoints;

	[HideInInspector]
	public float patrollingFOV = 20f;

	[HideInInspector]
	public float patrollingFOVDistance = 12f;

	[HideInInspector]
	public float patrollingFOVDistanceToFighting = 5f;

	[HideInInspector]
	public float patrollingSpeed = 1.7f;

	[HideInInspector]
	public float patrollingAcceleration = 5f;

	public float chasingSpeed = 8f;

	public float chasingAcceleration = 10f;

	public MoveHitboxController hitboxController;

	public float timeToAttack = 5f;

	public float damage;

	public float health = 20f;

	public EnemyRendererManager rendererManager;

	public EnemyUILifeController uiController;

	private PlayerController player;

	private EnemyState currentState;

	private AudioSource source;

	private bool gameEnd;

	void Awake ()
	{
		player = FindObjectOfType<PlayerController> ();
		source = GetComponent<AudioSource> ();
		CountdownTimer.GameEnd += OnGameEnd;
	}

	void OnDisable ()
	{
		CountdownTimer.GameEnd -= OnGameEnd;
	}

	void Start ()
	{
		currentState = new Chasing (this);
	}

	private void OnGameEnd ()
	{
		gameEnd = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!gameEnd)
			currentState.Update ();
	}

	public void SetState (EnemyState state)
	{
		this.enabled = false;
		this.currentState = state;
		this.enabled = true;
	}

	public void Damage (float damage)
	{		
		this.health -= damage;
		this.currentState.Reset ();
		this.animator.SetTrigger ("GotHit");
		if (this.health <= 0) {
			this.currentState = new EnemyDead (this);
			this.source.Play ();
			EnemyController.EnemyDead ();
		}
	}

	public void Damage (IDamageable other)
	{
		other.Damage (this.damage);
	}

	public void LookAtPlayer ()
	{
		Vector3 lookVector = player.transform.position - this.transform.position;
		lookVector.y = 0;
		lookVector.Normalize ();
		this.transform.rotation = Quaternion.LookRotation (lookVector);
	}

	public void HitFrameOpened ()
	{
		this.currentState.HitFrameOpened ();
	}

	public void HitFrameClosed ()
	{
		this.currentState.HitFrameClosed ();
	}

	public void AnimationFrameWindowOpened ()
	{
		this.currentState.AnimationFrameWindowOpened ();
	}

	public void AnimationFrameWindowClosed ()
	{
		this.currentState.AnimationFrameWindowClosed ();
	}
}
