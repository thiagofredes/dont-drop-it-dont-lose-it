using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorStateManager : MonoBehaviour
{
	public bool captureCursor;
	public bool makeCursorVisible;

	void Awake ()
	{
		if (captureCursor)
			Cursor.lockState = CursorLockMode.Locked;
		else
			Cursor.lockState = CursorLockMode.None;

		Cursor.visible = makeCursorVisible;
	}
}
