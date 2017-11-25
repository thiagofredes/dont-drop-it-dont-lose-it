using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighting : PlayerState
{
	private PlayerController player;

	private string[] animationTriggers = { "Attack", "Attack2", "Attack3" };

	private int currentTrigger;

	private bool canChangeLookDirection;

	private int numTriggers;

	public Fighting (PlayerController player)
	{
		this.player = player;
		this.currentTrigger = 0;
		this.numTriggers = animationTriggers.Length;
		this.player.animator.SetTrigger (animationTriggers [currentTrigger]);
		currentTrigger++;
		this.canChangeLookDirection = true;
	}

	public override void Reset ()
	{
		this.currentTrigger = 0;
//		this.player.animator.SetTrigger (animationTriggers [currentTrigger]);
		this.currentTrigger++;
		this.canChangeLookDirection = true;
		this.player.SetState (new Running (this.player));
	}

	public override void Update ()
	{
		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");
		Vector3 lookDirection = ThirdPersonCameraController.CameraForwardProjectionOnGround * vertical + ThirdPersonCameraController.CameraRightProjectionOnGround * horizontal;

		if (canChangeLookDirection) {
			if (lookDirection.magnitude == 0.0f) {
				player.transform.rotation = Quaternion.LookRotation (ThirdPersonCameraController.CameraForwardProjectionOnGround);
			} else {
				player.transform.rotation = Quaternion.LookRotation (lookDirection);
			}

			if (Input.GetKeyDown (KeyCode.Space)) {
				if (currentTrigger < numTriggers) {
					canChangeLookDirection = false;
					player.animator.SetTrigger (animationTriggers [currentTrigger]);
					currentTrigger++;
				}
			}
		}
	}

	public override void AnimationFrameWindowOpened ()
	{
		canChangeLookDirection = true;
	}

	public override void AnimationFrameWindowClosed ()
	{
		canChangeLookDirection = false;
		player.animator.SetTrigger ("EndFight");
		player.SetState (new Running (this.player));
	}

	public override void HitFrameOpened ()
	{
		if (currentTrigger > 0) {
			player.hitboxController.EnableHitbox (animationTriggers [currentTrigger - 1]);
		}
	}

	public override void HitFrameClosed ()
	{
		if (currentTrigger > 0) {
			player.hitboxController.DisableHitbox (animationTriggers [currentTrigger - 1]);
		}
	}
}
