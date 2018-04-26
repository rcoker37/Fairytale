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

    public void setAmbient() {
        SwitchMusic(ambientMusic); 
    }

    public void setApproaching()
    {
        SwitchMusic(approachMusic); 
    }

    public void setCaught()
    {
        SwitchMusic(caughtMusic); 
    }

    private void SwitchMusic(AudioClip clip) {
        audio.clip = clip;
        audio.Play();
        audio.loop = true;
    }
}
