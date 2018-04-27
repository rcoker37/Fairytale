using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlayerController : PlayerController {
    public KeyCode[] MoveRightKeySet = new KeyCode[] { KeyCode.RightArrow, KeyCode.D };
    public KeyCode[] MoveLeftKeySet = new KeyCode[] { KeyCode.LeftArrow, KeyCode.A };
    public float FallSpeedX = 2.0f;

    public float MaxFallSpeed = 10.0f;
    public float FallAcceleration = 0.6f;

    public float gaspFallTime = 0.75f;

    private float timeToUpdateFace = 0.1f;

    private float startTime;


	private void Start()
	{
        base.Start();
        startTime = Time.time;
	}

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

        if (timeToUpdateFace > 0.0f) {
            timeToUpdateFace -= Time.deltaTime;
            return;
        }

        if (rb.velocity.x != 0.0f)
        {
            anim.SetFloat("Facing", rb.velocity.x / Mathf.Abs(rb.velocity.x));
        } 
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

	private void OnDestroy()
	{
        if (Time.time - startTime > gaspFallTime) {
            audio.clip = GetComponent<PlayerSoundController>().GaspAudioClip;
            audio.Play();
            GameObject.FindGameObjectWithTag("Giant").GetComponent<GiantController>().Approach(0.25f);
        }
	}
}
