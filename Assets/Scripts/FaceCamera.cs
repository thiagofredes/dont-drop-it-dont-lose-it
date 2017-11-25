using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{

	public bool allignView;

	public bool faceAway;

	void LateUpdate ()
	{
		Vector3 lookVector = Camera.main.transform.position - this.transform.position;
		if (!allignView) {
			lookVector.y = 0f;
		}
		if (!faceAway)
			this.transform.rotation = Quaternion.LookRotation (lookVector);
		else
			this.transform.rotation = Quaternion.LookRotation (-lookVector);
	}
}
