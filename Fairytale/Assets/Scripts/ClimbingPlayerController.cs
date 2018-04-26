using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingPlayerController : PlayerController {
    
    public float ClimbSpeed = 2.0f;
	
	private void FixedUpdate()
	{
		Vector2 velocity = rb.velocity;
		velocity.y = ClimbSpeed * Input.GetAxisRaw("Vertical");
		rb.velocity = velocity;

        if (velocity.y != 0) {
            anim.SetFloat("Moving", 1.0f);
        } else {
            anim.SetFloat("Moving", -1.0f);
        }

		rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
	}
}
