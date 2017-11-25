using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
	public abstract void Reset ();

	public abstract void Update ();

	public abstract void AnimationFrameWindowOpened ();

	public abstract void AnimationFrameWindowClosed ();

	public abstract void HitFrameOpened ();

	public abstract void HitFrameClosed ();
}
