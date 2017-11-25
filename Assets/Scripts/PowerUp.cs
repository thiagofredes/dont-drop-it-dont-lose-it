using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PowerUp : MonoBehaviour
{

	public float healthUp = 2f;

	public float maxTimeOn = 10f;

	public AudioSource source;

	public Collider collider;

	public Renderer renderer;

	public Canvas canvas;

	private Coroutine blinkCoroutine;

	void Start ()
	{
		blinkCoroutine = StartCoroutine (Blink ());
	}

	// Update is called once per frame
	void Update ()
	{
		Vector3 newPosition = this.transform.position;
		newPosition.y += (float)Math.Sin (Time.time) * Time.deltaTime * 0.1f;
		this.transform.position = newPosition;
		this.transform.Rotate (0f, 2f, 0f);
	}

	private IEnumerator Blink ()
	{
		float blinkTime = 0.5f;
		yield return new WaitForSeconds (maxTimeOn * 0.5f);
		while (blinkTime > 0) {
			renderer.enabled = !renderer.enabled;
			canvas.enabled = !canvas.enabled;
			yield return new WaitForSeconds (blinkTime);
			blinkTime -= 3 * Time.deltaTime;
		}
		renderer.enabled = false;
		canvas.enabled = false;
		collider.enabled = false;
		GameObject.Destroy (this.gameObject);
	}

	void OnTriggerEnter (Collider other)
	{
		PlayerController player = other.GetComponent<PlayerController> ();
		if (player != null) {
			StopCoroutine (blinkCoroutine);
			player.AddHealth (healthUp);
			collider.enabled = false;
			renderer.enabled = false;
			this.enabled = false;
			canvas.enabled = false;
			source.Play ();
			StartCoroutine (WaitAudioEnd ());
		}
	}

	private IEnumerator WaitAudioEnd ()
	{
		while (source.isPlaying) {
			yield return new WaitForEndOfFrame ();
		}
		Destroy (this.gameObject);
	}
}
