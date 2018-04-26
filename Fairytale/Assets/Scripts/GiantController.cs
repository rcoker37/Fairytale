using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantController : MonoBehaviour {

    public AudioClip violinAudio;

    private enum State
    {
        APPROACHING,
        LEAVING,
        IDLE_STOMPING,
        SILENT
    }

    public float TimeBetweenIdleStomps = 15.0f;

    public float TimeBetweenStomps = 2.0f;

    public float ApproachDelay = 2.0f;
    public float ApproachStartVolume = 0.2f;
    public float ApproachEndVolume = 1.0f;
    public int ApproachSteps = 10;

    public float LeaveDelay = 5.0f;
    public float LeaveStartVolume = 0.6f;
    public float LeaveEndVolume = 0.2f;
    public int LeaveSteps = 6;

    public float IdleDelay = 2.0f;
    public float IdleStompVolume = 0.1f;
    public int IdleSteps = 4;

    private AudioSource audio;

    private State activeState;

    private float volumeDelta;
    private float stompSpeed;
    private float panVelocity;

    private float timeUntilNextIdleStomp;

    private StompController sc;

	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
        sc = GetComponent<StompController>();

        timeUntilNextIdleStomp = TimeBetweenIdleStomps;
	}

    private void Update()
    {
        timeUntilNextIdleStomp -= Time.deltaTime;
        if (timeUntilNextIdleStomp < 0.0f)
        {
            IdleStomping();
            timeUntilNextIdleStomp = TimeBetweenIdleStomps;
        }
    }

    public void Approach()
    {
        activeState = State.APPROACHING;
        PlayAudio(violinAudio);
        GameObject.FindGameObjectWithTag("AmbientSound").GetComponent<AmbientSoundController>().setApproaching();
        sc.StartStompSequence(ApproachDelay, TimeBetweenStomps, ApproachStartVolume, ApproachEndVolume, ApproachSteps);
    }

    public void WalkAway()
    {
        activeState = State.LEAVING;
        GameObject.FindGameObjectWithTag("AmbientSound").GetComponent<AmbientSoundController>().setAmbient();
        sc.StartStompSequence(LeaveDelay, TimeBetweenStomps, LeaveStartVolume, LeaveEndVolume, LeaveSteps);
    }

    public void IdleStomping()
    {
        if (activeState == State.SILENT) {
            activeState = State.IDLE_STOMPING;
            sc.StartStompSequence(IdleDelay, TimeBetweenStomps, IdleStompVolume, IdleStompVolume, IdleSteps);
        }
    }

    public void OnStompSequenceDone()
    {
        State oldState = activeState;
        activeState = State.SILENT;

        switch (oldState)
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

    private void PlayAudio(AudioClip clip) {
        audio.clip = clip;
        audio.Play();
    }
}
