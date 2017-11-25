using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHitboxController : MonoBehaviour
{

	[System.Serializable]
	public class Move
	{
		public string parentTrigger;
		public Collider hitbox;
	}

	public Move[] moves;

	private int numMoves;

	void Awake ()
	{
		numMoves = moves.Length;
		SetHitboxes (false);
	}

	private void SetHitboxes (bool status)
	{
		for (int m = 0; m < numMoves; m++) {			
			moves [m].hitbox.enabled = status;
		}
	}

	private void SetHitbox (string parentTrigger, bool status)
	{
		for (int m = 0; m < numMoves; m++) {
			if (moves [m].parentTrigger.Equals (parentTrigger)) {
				moves [m].hitbox.enabled = status;
				break;
			}
		}
	}

	public void EnableHitbox (string parentTrigger)
	{
		SetHitbox (parentTrigger, true);
	}

	public void DisableHitbox (string parentTrigger)
	{
		SetHitbox (parentTrigger, false);
	}
}
