using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class MenuScreenController : MonoBehaviour
{
	public GameObject[] titleScreen;

	public GameObject[] howToPlayScreen;

	public GameObject[] controlsScreen;

	void Awake ()
	{
		SetObjects (titleScreen, true);
		SetObjects (howToPlayScreen, false);
		SetObjects (controlsScreen, false);
	}

	private void SetObjects (GameObject[] array, bool status)
	{
		for (int g = 0; g < array.Length; g++) {
			array [g].SetActive (status);
		}
	}

	public void StartGame ()
	{
		SceneManager.LoadSceneAsync ("Main");
	}

	public void Instructions ()
	{
		SetObjects (titleScreen, false);
		SetObjects (howToPlayScreen, true);
	}

	public void Back ()
	{
		SetObjects (titleScreen, true);
		SetObjects (howToPlayScreen, false);
		SetObjects (controlsScreen, false);
	}

	public void Controls ()
	{
		SetObjects (titleScreen, false);
		SetObjects (controlsScreen, true);
	}

	public void Exit ()
	{
		#if UNITY_EDITOR
		EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}
}
