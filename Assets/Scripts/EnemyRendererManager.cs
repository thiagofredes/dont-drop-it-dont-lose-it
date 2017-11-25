using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRendererManager : MonoBehaviour
{

	public Renderer[] renderers;

	int numRenderers;

	void Awake ()
	{
		numRenderers = renderers.Length;
	}

	public void SetRenderer (bool status)
	{
		Set (status);
	}

	public void ToggleRenderer ()
	{
		Toggle ();
	}

	private void Set (bool status)
	{
		for (int r = 0; r < numRenderers; r++) {
			renderers [r].enabled = status;
		}	
	}

	private void Toggle ()
	{
		for (int r = 0; r < numRenderers; r++) {
			renderers [r].enabled = !renderers [r].enabled;
		}	
	}
}
