using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingPlayerController : PlayerController {
    public KeyCode[] ClimbUpKeySet = new KeyCode[] { KeyCode.UpArrow, KeyCode.W };
    public KeyCode[] ClimbDownKeySet = new KeyCode[] { KeyCode.DownArrow, KeyCode.A };

    public float ClimbSpeed = 3.0f;

	// Update is called once per frame
	void Update () {
        if (IsKeySetDown(ClimbUpKeySet) && !IsKeySetDown(ClimbDownKeySet))
        {
            rb.velocity = ClimbSpeed * Vector2.up;
        } else if (!IsKeySetDown(ClimbUpKeySet) && IsKeySetDown(ClimbDownKeySet))
        {
            rb.velocity = ClimbSpeed * Vector2.down;
        } else
        {
            rb.velocity = Vector2.zero;
        }
	}
}
