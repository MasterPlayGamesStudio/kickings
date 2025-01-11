using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disableit : MonoBehaviour {

	// Use this for initialization
	public float Timedelay=0.15f;
	void OnEnable()
	{
		Invoke ("Disableobjnow", Timedelay);
	}

	void Disableobjnow()
	{
		gameObject.SetActive (false);
	}
}
