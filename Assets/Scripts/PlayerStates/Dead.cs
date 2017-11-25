using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : PlayerState
{

	private PlayerController player;

	public Dead (PlayerController enemy)
	{
		this.player = enemy;
		this.player.characterController.enabled = false;
		this.player.playerCollider.enabled = false;
		this.player.animator.SetTrigger ("Die");
	}

	public override void AnimationFrameWindowClosed ()
	{
		throw new System.NotImplementedException ();
	}

	public override void AnimationFrameWindowOpened ()
	{
		throw new System.NotImplementedException ();
	}

	public override void HitFrameClosed ()
	{
		throw new System.NotImplementedException ();
	}

	public override void HitFrameOpened ()
	{
		throw new System.NotImplementedException ();
	}

	public override void Reset ()
	{
		throw new System.NotImplementedException ();
	}

	public override void Update ()
	{

	}
}
