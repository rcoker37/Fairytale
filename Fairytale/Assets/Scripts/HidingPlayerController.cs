using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingPlayerController : PlayerController
{
	private void Start()
	{
		base.Start();
		gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Hiding";
	}

	void FixedUpdate()
	{
		rb.velocity = Vector2.zero;
	}
}
