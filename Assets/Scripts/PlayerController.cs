using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour, IDamaging, IDamageable
{

	public static Transform PlayerTransform {
		get {
			return _instance.transform;
		}
	}

	public static bool PlayerDead {
		get {
			return _instance.Dead ();
		}
	}

	public Animator animator;

	public CharacterController characterController;

	public Collider playerCollider;

	public float movementSpeed;

	public MoveHitboxController hitboxController;

	private PlayerState state;

	private static PlayerController _instance;

	public float health = 20f;

	public AudioClip deathClip;

	public AudioClip punchClip;

	private AudioSource source;

	private float maxHealth;

	private bool gameEnd;

	void Awake ()
	{
		_instance = this;
		source = GetComponent<AudioSource> ();
		maxHealth = health;
		gameEnd = false;
		CountdownTimer.GameEnd += OnGameEnd;
	}

	void OnDisable ()
	{
		CountdownTimer.GameEnd -= OnGameEnd;
	}

	private void OnGameEnd ()
	{
		gameEnd = true;
	}

	void Start ()
	{
		state = new Running (this);	
	}

	void Update ()
	{
		if (!gameEnd)
			state.Update ();
	}

	public void SetState (PlayerState state)
	{
		this.enabled = false;
		this.state = state;
		this.enabled = true;
	}

	public void AnimationFrameWindowOpen ()
	{
		this.state.AnimationFrameWindowOpened ();
	}

	public void AnimationFrameWindowClosed ()
	{
		this.state.AnimationFrameWindowClosed ();
	}

	public void HitFrameOpened ()
	{
		this.state.HitFrameOpened ();
	}

	public void HitFrameClosed ()
	{
		this.state.HitFrameClosed ();
	}

	public void Damage (IDamageable other)
	{
		other.Damage (5f);
		source.PlayOneShot (punchClip);
	}

	public void Damage (float damage)
	{
		this.health = Mathf.Clamp (this.health - damage, 0f, maxHealth);
		this.state.Reset ();
		this.animator.SetTrigger ("GotHit");
		source.PlayOneShot (punchClip);
		if (this.health <= 0) {
			CountdownTimer.ShowDefeatText ();
			source.volume = 1f;
			source.PlayOneShot (deathClip);
			this.animator.SetTrigger ("Die");
		}
	}

	public bool Dead ()
	{
		if (this.health <= 0) {
			return true;
		}
		return false;
	}

	public void AddHealth (float healthUp)
	{
		this.health = Mathf.Clamp (this.health + healthUp, 0f, maxHealth);
	}

}
