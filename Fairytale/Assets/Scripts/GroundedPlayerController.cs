using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedPlayerController : PlayerController {
    public KeyCode[] MoveRightKeySet = new KeyCode[] { KeyCode.RightArrow, KeyCode.D };
    public KeyCode[] MoveLeftKeySet = new KeyCode[] { KeyCode.LeftArrow, KeyCode.A };
    public float WalkSpeed = 2.0f;

    private bool stoppedRight;
    private bool stoppedLeft;

    private float timeToUpdateFace = 0.1f;

	private void Start()
	{
		base.Start();
		gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
	}

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

        if (timeToUpdateFace > 0.0f)
        {
            timeToUpdateFace -= Time.deltaTime;
            return;
        }

        if (rb.velocity.x != 0)// && !GetComponent<PlayerCollisionManager>().IsColliding(rb.velocity, false, false))
		{
			//TODO: still do this if the collided object is a hiding trigger (or other trigger?)
			anim.SetFloat("Moving", 1.0f);
            anim.SetFloat("Facing", rb.velocity.x / Mathf.Abs(rb.velocity.x));
		}
		else
		{
			anim.SetFloat("Moving", -1.0f);
		}

	}

	private void FixedUpdate()
	{
		Vector2 vel = rb.velocity;
		vel.y = 0;
		rb.velocity = vel;

		MoveHorizontal(WalkSpeed);
	}

    private void OnDisable()
    {
        rb.velocity = Vector2.zero;
    }

}
