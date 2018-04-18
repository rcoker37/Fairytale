using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushingPlayerController : PlayerController {
    public float PushSpeed = 2.0f;

    private Collider2D otherCol;
    private float otherColRelX;

    protected override void Start()
    {
        base.Start();
        if (colMan.IsColliding(Vector2.right, true, false))
        {
            otherCol = colMan.GetColliding(Vector2.right, true, false);
        } else if (colMan.IsColliding(Vector2.left, true, false))
        {
            otherCol = colMan.GetColliding(Vector2.left, true, false);
        }

		float otherX = otherCol.GetComponent<Rigidbody2D>().position.x;
		
		otherColRelX = otherX - transform.position.x;
		otherColRelX *= 1.01f;
		print(otherColRelX);
    }
	
    private void FixedUpdate()
    {
        Vector2 otherPos = otherCol.GetComponent<Rigidbody2D>().position;
		float offset = PushSpeed * Input.GetAxis("Horizontal") * Time.fixedDeltaTime;
		otherPos.x = transform.position.x + otherColRelX + offset;
		otherCol.GetComponent<Rigidbody2D>().MovePosition(otherPos);

		MoveHorizontal(PushSpeed);
	}

}
