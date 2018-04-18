using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompController : MonoBehaviour {

    public AudioClip stompClip;
    public float stompVolumeScale;

    private float startVolume;
    private float endVolume;
    private float delay;
    private float timeBetweenStomps;
    private int totalStomps;

    private AudioSource audio;
    private GiantController gc;

    public bool stomping;

    private float timeToNextStomp;
    private int stepsRemaining;

	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
        gc = GetComponent<GiantController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (stomping)
        {
            timeToNextStomp -= Time.deltaTime;
            if (timeToNextStomp < 0.0f && stepsRemaining > 0)
            {
                timeToNextStomp = timeBetweenStomps;
                stepsRemaining -= 1;

                audio.clip = stompClip;
                audio.volume = (endVolume - audio.volume) / stepsRemaining + audio.volume;
                audio.Play();
            }

            if (stepsRemaining == 0)
            {
                stomping = false;
                gc.OnStompSequenceDone();
            }
        }	
	}

    public void StartStompSequence(float delay, float timeBetweenStomps, float startVolume, float endVolume,  int numberOfSteps)
    {
        this.timeBetweenStomps = timeBetweenStomps;
        this.startVolume = startVolume * stompVolumeScale;
        this.endVolume = endVolume * stompVolumeScale;
        stepsRemaining = numberOfSteps;


        stomping = true;
        timeToNextStomp = delay;
        audio.volume = startVolume;
    }

    void StopStomp()
    {
        stomping = false;
    }

}
