using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerController : MonoBehaviour {

    protected Rigidbody2D rb;
    protected Animator anim;
    protected PlayerCollisionManager colMan;

    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        colMan = GetComponent<PlayerCollisionManager>();
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

}
