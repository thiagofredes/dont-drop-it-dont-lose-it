using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
	public static event Action GameEnd;

	public float totalSeconds;

	public Text uiTimerSeconds;

	public Text uiTimerSecondsCents;

	public Text victoryText;

	public Text defeatText;

	private static CountdownTimer _instance;

	void Awake ()
	{
		_instance = this;
		victoryText.enabled = false;
		defeatText.enabled = false;
	}

	void Update ()
	{
		int integerPart;
		int decimalPart;
		totalSeconds = Mathf.Clamp (totalSeconds - Time.deltaTime, 0f, totalSeconds);
		if (totalSeconds == 0) {
			victoryText.enabled = true;
			StartCoroutine (LoadMenu ());
		} else {
			integerPart = (int)Math.Truncate (totalSeconds);
			decimalPart = System.Convert.ToInt16 ((totalSeconds - integerPart) * 100);
			uiTimerSeconds.text = integerPart.ToString ("###");
			uiTimerSecondsCents.text = decimalPart.ToString ("##");			
		}
	}

	private IEnumerator LoadMenu ()
	{
		yield return new WaitForSeconds (5f);
		SceneManager.LoadSceneAsync ("Menu");
	}

	public static void ShowDefeatText ()
	{
		_instance.defeatText.enabled = true;
		_instance.StartCoroutine (_instance.LoadMenu ());
	}
}
