using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlayerController : PlayerController {
    public KeyCode[] MoveRightKeySet = new KeyCode[] { KeyCode.RightArrow, KeyCode.D };
    public KeyCode[] MoveLeftKeySet = new KeyCode[] { KeyCode.LeftArrow, KeyCode.A };
    public float FallSpeedX = 2.0f;

    public float MaxFallSpeed = 10.0f;
    public float FallAcceleration = 0.6f;

    private void Update()
    {
        /*bool moveRightDown = IsKeySetDown(MoveRightKeySet); 
        bool moveLeftDown = IsKeySetDown(MoveLeftKeySet); 

        if (moveRightDown && !moveLeftDown && !colMan.IsColliding(Vector2.right, false, false))
        {
            Vector2 vel = rb.velocity;
            vel.x = FallSpeedX;
            rb.velocity = vel;
        } else if (!moveRightDown && moveLeftDown && !colMan.IsColliding(Vector2.left, false, false))
        { 
            Vector2 vel = rb.velocity;
            vel.x = -FallSpeedX;
            rb.velocity = vel;
        } else
        {
            Vector2 vel = rb.velocity;
            vel.x = 0.0f;
            rb.velocity = vel;
        }*/
    }
    
    void FixedUpdate () {
        Vector2 vel = rb.velocity;
        if (vel.y > -MaxFallSpeed)
        {
            vel.y -= FallAcceleration;
			rb.velocity = vel;
		}

		MoveHorizontal(FallSpeedX);
	}
}
