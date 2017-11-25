using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class CameraController : MonoBehaviour
{
	public float cameraFollowDamping = 0.8f;

	private PlayerController playerObject;

	private Vector3 playerOrbit;

	static public Vector3 CameraForward {
		get {
			return  cameraForward;
		}
	}

	static public Vector3 CameraRight {
		get {
			return  cameraRight;
		}
	}

	static private Vector3 cameraForward;

	static private Vector3 cameraRight;

	void Awake ()
	{
		playerObject = FindObjectOfType<PlayerController> ();
		cameraForward = Vector3.ProjectOnPlane (this.transform.forward, Vector3.up).normalized;
		cameraRight = Vector3.ProjectOnPlane (this.transform.right, Vector3.up).normalized;
		playerOrbit = this.transform.position - playerObject.transform.position;
	}

	IEnumerator Start ()
	{
		while (playerObject == null) {
			playerObject = FindObjectOfType<PlayerController> ();
			yield return new WaitForEndOfFrame ();
		}
	}

	void LateUpdate ()
	{		
		Vector3 currentVelocity = Vector3.zero;
		this.transform.position = Vector3.SmoothDamp (this.transform.position, playerObject.transform.position + playerOrbit, ref currentVelocity, cameraFollowDamping);
		cameraForward = Vector3.ProjectOnPlane (this.transform.forward, Vector3.up).normalized;
		cameraRight = Vector3.ProjectOnPlane (this.transform.right, Vector3.up).normalized;
		RemoveObjectsOnSight ();
	}

	private void RemoveObjectsOnSight ()
	{
		RaycastHit[] hits = Physics.SphereCastAll (this.transform.position, 10f, -playerOrbit, Mathf.Infinity, ~LayerMask.GetMask ("Player"));
		int hitsLength = hits.Length;
		if (hitsLength > 0) {
			for (int h = 0; h < hitsLength; h++) {
				Renderer r = hits [h].collider.gameObject.GetComponent<Renderer> ();
				if (r != null) {
					r.enabled = false;
				}
			}
		}

	}
}
