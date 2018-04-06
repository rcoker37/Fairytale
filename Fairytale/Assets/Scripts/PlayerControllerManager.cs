using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerManager : MonoBehaviour {

    public KeyCode[] ClimbKeySet = new KeyCode[] { KeyCode.LeftShift, KeyCode.RightShift };
    public KeyCode[] PushKeySet = new KeyCode[] { KeyCode.LeftControl, KeyCode.RightControl };

    private static readonly int CONTACT_BUFFER_SIZE = 1024;
    private readonly ContactPoint2D[] contactBuffer = new ContactPoint2D[CONTACT_BUFFER_SIZE];

    private State activeState;
    private PlayerController activeController;
    private PlayerCollisionManager colMan;

    private Rigidbody2D rb;
    private Collider2D col;

    public enum State
    {
        GROUNDED,
        FALLING,
        PUSHING,
        CLIMBING,
    }

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        colMan = GetComponent<PlayerCollisionManager>();

        activeController = GetComponent<PlayerController>();
        activeState = State.GROUNDED;
	}
	
	// Update is called once per frame
	void Update () { 
		
	}

    private void FixedUpdate()
    {
        CheckForStateChange();
    }

    private void CheckForStateChange()
    {
        if (ShouldChangeToGrounded())
        {
            ChangeState(State.GROUNDED);
            GetComponent<SpriteRenderer>().color = Color.red;
        } else if (ShouldChangeToFalling())
        {
            ChangeState(State.FALLING);
            GetComponent<SpriteRenderer>().color = Color.black;
        }
        else if (ShouldChangeToClimbing())
        {
            ChangeState(State.CLIMBING);
            GetComponent<SpriteRenderer>().color = Color.blue;
        }
        else if (ShouldChangeToPushing())
        {
            ChangeState(State.PUSHING);
            GetComponent<SpriteRenderer>().color = Color.green;
        }
    }

    private void ChangeState(State newState)
    {
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
        }
    }

    private bool ShouldChangeToGrounded()
    {
        switch (activeState)
        {
            case State.GROUNDED:
                return false;
                break;
            case State.FALLING:
                return colMan.IsColliding(Vector2.down, false, false);
                break;
            case State.PUSHING:
                return (!colMan.IsColliding(Vector2.left, true, false) && !colMan.IsColliding(Vector2.right, true, false)) ||
                       (!activeController.IsKeySetDown(PushKeySet));
                break;
            case State.CLIMBING:
                return false;
                break;
            default:
                return false;
                break;
        }
    }

    private bool ShouldChangeToFalling()
    {
        switch (activeState) {
            case State.GROUNDED:
                return !colMan.IsColliding(Vector2.down, false, false);
                break;
            case State.FALLING:
                return false;
                break;
            case State.PUSHING:
                return !colMan.IsColliding(Vector2.down, false, false);
                break;
            case State.CLIMBING:
                return !activeController.IsKeySetDown(ClimbKeySet);
                break;
            default:
                return false;
                break;
        }
    }

    private bool ShouldChangeToPushing()
    {
        switch (activeState)
        {
            case State.GROUNDED:
                return (colMan.IsColliding(Vector2.left, true, false) || colMan.IsColliding(Vector2.right, true, false)) &&
                       (activeController.IsKeySetDown(PushKeySet));
                break;
            case State.FALLING:
                return false;
                break;
            case State.PUSHING:
                return false;
                break;
            case State.CLIMBING:
                return false;
                break;
            default:
                return false;
                break;
        }
    }

    private bool ShouldChangeToClimbing()
    {
        switch (activeState)
        {
            case State.GROUNDED:
                return activeController.IsKeySetDown(ClimbKeySet) &&
                       (colMan.IsColliding(Vector2.left, false, true) || colMan.IsColliding(Vector2.right, false, true));
                break;
            case State.FALLING:
                return activeController.IsKeySetDown(ClimbKeySet) &&
                       (colMan.IsColliding(Vector2.left, false, true) || colMan.IsColliding(Vector2.right, false, true));
                break;
            case State.PUSHING:
                return false;
                break;
            case State.CLIMBING:
                return false;
                break;
            default:
                return false;
                break;
        }
    }
}
