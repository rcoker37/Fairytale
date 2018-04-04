using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedPlayerController : PlayerController {
    public KeyCode[] MoveRightKeySet = new KeyCode[] { KeyCode.RightArrow, KeyCode.D };
    public KeyCode[] MoveLeftKeySet = new KeyCode[] { KeyCode.LeftArrow, KeyCode.A };
    public float WalkSpeed = 10.0f;

    private Rigidbody2D rb;
    private Animator anim;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        bool moveRightDown = IsKeySetDown(MoveRightKeySet); 
        bool moveLeftDown = IsKeySetDown(MoveLeftKeySet); 

        if (moveRightDown && !moveLeftDown)
        {
            rb.velocity = WalkSpeed * Vector2.right;
            anim.SetBool("Moving", true);
            anim.SetBool("FacingRight", true);
        } else if (!moveRightDown && moveLeftDown)
        { 
            rb.velocity = WalkSpeed * Vector2.left;
            anim.SetBool("Moving", true);
            anim.SetBool("FacingRight", false);
        } else
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("Moving", false);
        }
	}

    private bool IsKeySetDown(KeyCode[] keySet)
    {
        foreach (KeyCode keyCode in keySet)
        {
            if (Input.GetKey(keyCode))
            {
                return true; 
            }
        }

        return false;
    }

}
