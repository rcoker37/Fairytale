using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushingPlayerController : PlayerController {
    public KeyCode[] MoveRightKeySet = new KeyCode[] { KeyCode.RightArrow, KeyCode.D };
    public KeyCode[] MoveLeftKeySet = new KeyCode[] { KeyCode.LeftArrow, KeyCode.A };
    public float PushSpeed = 2.0f;

    private Collider2D otherCol;
    private float otherColRelX;

    void Start()
    {
        base.Start();
        if (colMan.IsColliding(Vector2.right, true, false))
        {
            otherCol = colMan.GetColliding(Vector2.right, true, false);
        } else if (colMan.IsColliding(Vector2.left, true, false))
        {
            otherCol = colMan.GetColliding(Vector2.left, true, false);
        }

        otherColRelX = otherCol.GetComponent<Rigidbody2D>().position.x - transform.position.x - 0.1f;
    }

	// Update is called once per frame
	void Update () {
	    if (IsKeySetDown(MoveRightKeySet) && !IsKeySetDown(MoveLeftKeySet))
        {
            rb.velocity = PushSpeed * Vector2.right;
        } else if (!IsKeySetDown(MoveRightKeySet) && IsKeySetDown(MoveLeftKeySet))
        {
            rb.velocity = PushSpeed * Vector2.left;
        } else
        {
            rb.velocity = Vector2.zero;
        }
	}

    private void FixedUpdate()
    {
        Vector2 otherPos = otherCol.GetComponent<Rigidbody2D>().position;
        otherPos.x = transform.position.x + otherColRelX;
		//otherCol.GetComponent<Rigidbody2D>().position = otherPos;
		otherCol.GetComponent<Rigidbody2D>().MovePosition(otherPos);
	}

}
