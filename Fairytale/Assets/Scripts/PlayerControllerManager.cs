﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerManager : MonoBehaviour {

    public KeyCode[] ClimbKeySet = new KeyCode[] { KeyCode.LeftShift, KeyCode.RightShift };
    public KeyCode[] PushKeySet = new KeyCode[] { KeyCode.Z, KeyCode.LeftControl, KeyCode.RightControl };
	public KeyCode[] HideKeySet = new KeyCode[] { KeyCode.Space };

    public bool PlayerControllerDisabled;

    public float ClimbDeltaX = 1.0f;
    public float ClimbDeltaY = 1.0f;

	private static readonly int CONTACT_BUFFER_SIZE = 1024;
    private readonly ContactPoint2D[] contactBuffer = new ContactPoint2D[CONTACT_BUFFER_SIZE];

    private State activeState;
    private PlayerController activeController;
    private PlayerCollisionManager colMan;

    private Rigidbody2D rb;
    private Collider2D col;
    private Animator anim;

    private Vector3 startPosition;

    private bool hasPassedHidingSpot;

    public enum State
    {
        GROUNDED,
        FALLING,
        CLIMBING,
        PUSHING,
		HIDING,
    }

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        colMan = GetComponent<PlayerCollisionManager>();
        anim = GetComponent<Animator>();

        activeController = GetComponent<PlayerController>();
        activeState = State.GROUNDED;
        startPosition = transform.position;
	}

    public void OnGiantApproached()
    {
        if (activeState != State.HIDING)
        {
            SceneManager.LoadScene("LivingRoom");
        } else
        {
            GameObject.FindGameObjectWithTag("Giant").GetComponent<GiantController>().WalkAway();
        }
    }
	
	// Update is called once per frame
	void Update () {
        activeController.enabled = !PlayerControllerDisabled;	

        if (!hasPassedHidingSpot && colMan.CanHide())
        {
            hasPassedHidingSpot = true;
            GameObject.FindGameObjectWithTag("Giant").GetComponent<GiantController>().IdleStomping();
        }
	}

    private void FixedUpdate()
    {
        CheckForStateChange();
    }

    private void CheckForStateChange()
    {
        if (!activeController.enabled) {
            return;
        }

        if (ShouldChangeToGrounded())
        {
            ChangeState(State.GROUNDED);
        } else if (ShouldChangeToFalling())
        {
            ChangeState(State.FALLING);
        }
        else if (ShouldChangeToClimbing())
        {
            ChangeState(State.CLIMBING);
        }
        else if (ShouldChangeToPushing())
        {
            ChangeState(State.PUSHING);
        }
		else if (ShouldChangeToHiding())
		{
			ChangeState(State.HIDING);
		}
	}

    private void ChangeState(State newState)
    {
        anim.SetInteger("PreviousState", (int)activeState);
        anim.SetInteger("NextState", (int)newState);
        anim.SetBool("DirtyBit", true);

        print(activeState.ToString() + " -> " + newState.ToString());

        Destroy(activeController);
        activeState = newState;
        switch (newState)
        {
            case State.GROUNDED:
                activeController = gameObject.AddComponent<GroundedPlayerController>();
                break;
            case State.FALLING:
                activeController = gameObject.AddComponent<FallingPlayerController>();
                break;
            case State.PUSHING:
                activeController = gameObject.AddComponent<PushingPlayerController>();
                break;
            case State.CLIMBING:
                activeController = gameObject.AddComponent<ClimbingPlayerController>();
                break;
			case State.HIDING:
				activeController = gameObject.AddComponent<HidingPlayerController>();
				break;
		}
    }

    private bool ShouldChangeToGrounded()
    {
        switch (activeState)
        {
            case State.GROUNDED:
                return false;
            case State.FALLING:
				return colMan.IsGrounded();
            case State.PUSHING:
                return (!colMan.IsColliding(Vector2.left, true, false) && !colMan.IsColliding(Vector2.right, true, false)) ||
                       (!activeController.IsKeySetDown(PushKeySet));
            case State.CLIMBING:
                return false;
			case State.HIDING:
				return !activeController.IsKeySetDown(HideKeySet);
			default:
                return false;
        }
    }

    private bool ShouldChangeToFalling()
    {
        switch (activeState) {
            case State.GROUNDED:
                return !colMan.IsGrounded();
            case State.FALLING:
                return false;
            case State.PUSHING:
                return !colMan.IsGrounded();
            case State.CLIMBING:
                if (colMan.IsColliding(Vector2.left, false, true) && colMan.GetColliding(Vector2.left, false, true).bounds.max.y < col.bounds.center.y)
                {

                    rb.position = new Vector2(colMan.GetColliding(Vector2.left, false, true).bounds.max.x - col.bounds.extents.x,
                                              colMan.GetColliding(Vector2.left, false, true).bounds.max.y + col.bounds.extents.y);
                    col.enabled = false;
                    col.enabled = true;
                    return true;
                } else if (colMan.IsColliding(Vector2.right, false, true) && colMan.GetColliding(Vector2.right, false, true).bounds.max.y < col.bounds.center.y)
                {
                    rb.position = new Vector2(colMan.GetColliding(Vector2.right, false, true).bounds.min.x + col.bounds.extents.x,
                                              colMan.GetColliding(Vector2.right, false, true).bounds.max.y + col.bounds.extents.y);
                    col.enabled = false;
                    col.enabled = true;
                    return true;
                } else
                {
                    return !activeController.IsKeySetDown(ClimbKeySet);
                }
			case State.HIDING:
				return false;
			default:
                return false;
        }
    }

    private bool ShouldChangeToPushing()
    {
        switch (activeState)
        {
            case State.GROUNDED:
                return (colMan.IsColliding(Vector2.left, true, false) || colMan.IsColliding(Vector2.right, true, false)) &&
                       (activeController.IsKeySetDown(PushKeySet));
            case State.FALLING:
                return false;
            case State.PUSHING:
                return false;
            case State.CLIMBING:
                return false;
			case State.HIDING:
				return false;
			default:
                return false;
        }
    }

    private bool ShouldChangeToClimbing()
    {
        switch (activeState)
        {
            case State.GROUNDED:
                if (activeController.IsKeySetDown(ClimbKeySet)) {
                    if (colMan.IsColliding(Vector2.left, false, true)) {
                        anim.SetFloat("Facing", -1.0f);
                        return true;
                    } else if (colMan.IsColliding(Vector2.right, false, true)) {
                        anim.SetFloat("Facing", 1.0f);
                        return true;
                    }
                }

                return false;
            case State.FALLING:
                if (activeController.IsKeySetDown(ClimbKeySet))
                {
                    if (colMan.IsColliding(Vector2.left, false, true))
                    {
                        anim.SetFloat("Facing", -1.0f);
                        return true;
                    }
                    else if (colMan.IsColliding(Vector2.right, false, true))
                    {
                        anim.SetFloat("Facing", 1.0f);
                        return true;
                    }
                }

                return false;
            case State.PUSHING:
                return false;
            case State.CLIMBING:
                return false;
			case State.HIDING:
				return false;
			default:
                return false;
        }
    }

	private bool ShouldChangeToHiding()
	{
		switch (activeState)
		{
			case State.GROUNDED:
				return colMan.CanHide() && activeController.IsKeySetDown(HideKeySet);
			case State.FALLING:
				return false;
			case State.PUSHING:
				return false;
			case State.CLIMBING:
				return false;
			case State.HIDING:
				return false;
			default:
				return false;
		}
	}
}
