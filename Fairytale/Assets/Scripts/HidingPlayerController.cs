using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingPlayerController : PlayerController
{
	void FixedUpdate()
	{
		rb.velocity = Vector2.zero;
	}
}
