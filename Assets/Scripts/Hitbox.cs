using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
	private Collider thisCollider;

	private IDamaging parentDamageable;

	void Awake ()
	{
		thisCollider = GetComponent<Collider> ();
		parentDamageable = GetComponentInParent<IDamaging> ();
	}

	void OnTriggerEnter (Collider other)
	{
		thisCollider.enabled = false;
		IDamageable damageable = other.GetComponent<IDamageable> ();
		if (damageable != null) {
			parentDamageable.Damage (damageable);
		}
	}
}
