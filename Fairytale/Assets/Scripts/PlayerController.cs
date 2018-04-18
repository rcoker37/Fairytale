using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerController : MonoBehaviour {

    protected Rigidbody2D rb;
    protected Animator anim;
    protected PlayerCollisionManager colMan;
    protected AudioSource audio;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        colMan = GetComponent<PlayerCollisionManager>();
        audio = GetComponent<AudioSource>();
    }

    public bool IsKeySetDown(KeyCode[] keySet)
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

	public void MoveHorizontal(float moveSpeed)
	{
		Vector2 velocity = rb.velocity;
		
		velocity.x = moveSpeed * Input.GetAxis("Horizontal");
		
		rb.velocity = velocity;
		rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
	}
}
