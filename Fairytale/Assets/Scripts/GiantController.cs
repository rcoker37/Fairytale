using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantController : MonoBehaviour {

    private enum State
    {
        APPROACHING,
        LEAVING,
        IDLE_STOMPING,
        SILENT
    }

    private AudioSource audio;

    private State activeState;

    private float volumeDelta;
    private float stompSpeed;
    private float panVelocity;

    private StompController sc;

	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
        sc = GetComponent<StompController>();
	}

    private void Update()
    {
    }

    public void Approach()
    {
        activeState = State.APPROACHING;
        sc.StartStompSequence(2.0f, 4.0f, 0.2f, 1.0f, 5);
    }

    public void WalkAway()
    {
        activeState = State.LEAVING;
        sc.StartStompSequence(2.0f, 4.0f, 1.0f, 0.2f, 5);
    }

    public void IdleStomping()
    {

    }

    public void OnStompSequenceDone()
    {
        switch (activeState)
        {
            case State.APPROACHING:
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerManager>().OnGiantApproached();
                break;
            case State.LEAVING:
                break;
            case State.IDLE_STOMPING:
                break;
            case State.SILENT:
                break;
        } 
    }
}
