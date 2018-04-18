using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingPlayerController : PlayerController {
    
    public float ClimbSpeed = 3.0f;
	
	private void FixedUpdate()
	{
		Vector2 velocity = rb.velocity;
		velocity.y = ClimbSpeed * Input.GetAxisRaw("Vertical");
		rb.velocity = velocity;

		rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
	}
}
