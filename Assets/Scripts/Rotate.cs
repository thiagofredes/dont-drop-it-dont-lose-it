using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rotate : MonoBehaviour
{

	public float speed;


	void Update ()
	{
		this.transform.Rotate (0f, Time.deltaTime * speed, 0f);
	}
}
