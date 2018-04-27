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

    public float ApproachDelay = 4.0f;
    public float ApproachStartVolume = 0.2f;
    public float ApproachEndVolume = 1.0f;
    public float[] ApproachSteps = { 8.0f, 3.0f };

    public float LeaveDelay = 5.0f;
    public float LeaveStartVolume = 0.4f;
    public float LeaveEndVolume = 0.1f;
    public float[] LeaveSteps = { 4.0f, 2.0f };

    public float IdleDelay = 2.0f;
    public float IdleStompVolume = 0.1f;
    public float[] IdleSteps = { 3.0f, 1.0f };

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
        activeState = State.SILENT;
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

    public void Approach(float probability)
    {
        if (activeState == State.APPROACHING) {
            return;
        }

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerManager>().Freeze();

        GameObject.FindGameObjectWithTag("AmbientSound").GetComponent<AmbientSoundController>().turnOffMusic();
        PlayAudio(violinAudio, 0.5f);

        if (Random.Range(0.0f, 1.0f) <= probability) {
            activeState = State.APPROACHING;
            GameObject.FindGameObjectWithTag("AmbientSound").GetComponent<AmbientSoundController>().setApproaching(3.0f);
            sc.StartStompSequence(ApproachDelay, TimeBetweenStomps, ApproachStartVolume, ApproachEndVolume, GetRandomInt(ApproachSteps));
        } else {
            GameObject.FindGameObjectWithTag("AmbientSound").GetComponent<AmbientSoundController>().setAmbient(3.0f);
        }
    }

    public void WalkAway()
    {
        activeState = State.LEAVING;
        GameObject.FindGameObjectWithTag("AmbientSound").GetComponent<AmbientSoundController>().setAmbient(LeaveDelay);
        sc.StartStompSequence(LeaveDelay, TimeBetweenStomps, LeaveStartVolume, LeaveEndVolume, GetRandomInt(LeaveSteps));
    }

    public void IdleStomping()
    {
        if (activeState == State.SILENT) {
            activeState = State.IDLE_STOMPING;
            sc.StartStompSequence(IdleDelay, TimeBetweenStomps, IdleStompVolume, IdleStompVolume, GetRandomInt(IdleSteps));
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

    private void PlayAudio(AudioClip clip, float delay) {
        audio.clip = clip;
        audio.PlayDelayed(delay);
        print("Playing");
    }

    private int GetRandomInt(float[] dist) {
        return Mathf.RoundToInt(Random.Range(dist[0] - dist[1], dist[0] + dist[1])); 
    }
}
