using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blender : MonoBehaviour {

	bool active = false;

	public void Activate()
	{
		if (!active)
		{
			active = true;
			gameObject.GetComponent<AudioSource>().Play();
			GameObject.FindGameObjectWithTag("Giant").GetComponent<GiantController>().Approach(1.0f);
		}
	}
}
