using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundController : MonoBehaviour {
    public AudioClip ambientMusic;
    public AudioClip approachMusic;
    public AudioClip caughtMusic;

    private AudioSource audio;


	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setAmbient(float timeToWait) {
        SwitchMusic(ambientMusic, timeToWait); 
    }

    public void setApproaching(float timeToWait)
    {
        SwitchMusic(approachMusic, timeToWait); 
    }

    public void setCaught()
    {
        SwitchMusic(caughtMusic, 0.0f); 
    }

    private void SwitchMusic(AudioClip clip, float timeToWait) {
        audio.clip = clip;
        audio.PlayDelayed(timeToWait);
        audio.loop = true;
    }
}
