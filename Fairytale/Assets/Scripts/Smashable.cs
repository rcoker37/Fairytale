using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smashable : MonoBehaviour {

	public float SmashThreshold;

	private Rigidbody2D rb;

	private void Start()
	{
		rb = gameObject.GetComponent<Rigidbody2D>();
	}

	private void OnCollisionEnter2D()
	{
		print(rb.velocity.y);
		if (Mathf.Abs(rb.velocity.y) >= SmashThreshold)
		{
			Smash();
		}
	}

	private void Smash()
	{
		print("SMASH");
		//TODO: play sound
		//TODO: leave broken bits
		Destroy(gameObject);
	}
	
}
