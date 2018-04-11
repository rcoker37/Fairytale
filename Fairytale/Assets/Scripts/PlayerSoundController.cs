using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour {

    public bool ShouldPlaySound;
    public AudioClip ActiveAudioClip;


    private AudioSource audio;


	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();	
	}
	
	// Update is called once per frame
	void Update () {
	    if (ShouldPlaySound)
        {
            audio.Stop();
            audio.clip = ActiveAudioClip;
            audio.Play();
        } 

	}
}
