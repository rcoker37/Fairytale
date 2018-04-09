using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepSoundController : MonoBehaviour {
    public bool ShouldPlaySound;

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
            audio.Play();
            ShouldPlaySound = false;
        }	
	}
}
