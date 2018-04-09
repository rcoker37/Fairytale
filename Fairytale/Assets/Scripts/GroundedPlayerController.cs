using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedPlayerController : PlayerController {
    public KeyCode[] MoveRightKeySet = new KeyCode[] { KeyCode.RightArrow, KeyCode.D };
    public KeyCode[] MoveLeftKeySet = new KeyCode[] { KeyCode.LeftArrow, KeyCode.A };
    public float WalkSpeed = 3.0f;

    private bool stoppedRight;
    private bool stoppedLeft;

	
	// Update is called once per frame
	void Update () {
		/*bool moveRightDown = IsKeySetDown(MoveRightKeySet); 
        bool moveLeftDown = IsKeySetDown(MoveLeftKeySet); 

        if (moveRightDown && !moveLeftDown && !colMan.IsColliding(Vector2.right, false, false))
        {
            rb.velocity = WalkSpeed * Vector2.right;
            anim.SetBool("Moving", true);
            anim.SetBool("FacingRight", true);
        } else if (!moveRightDown && moveLeftDown && !colMan.IsColliding(Vector2.left, false, false))
        { 
            rb.velocity = WalkSpeed * Vector2.left;
            anim.SetBool("Moving", true);
            anim.SetBool("FacingRight", false);
        } else
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("Moving", false);
        }*/

		if (rb.velocity.x != 0)
		{
			anim.SetBool("Moving", true);
			anim.SetBool("FacingRight", rb.velocity.x > 0);
		}
		else
		{
			anim.SetBool("Moving", false);
		}
	}

	private void FixedUpdate()
	{
		Vector2 vel = rb.velocity;
		vel.y = 0;
		rb.velocity = vel;

		MoveHorizontal(WalkSpeed);
	}

}
