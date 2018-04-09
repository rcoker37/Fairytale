using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantController : MonoBehaviour {

    public AudioClip StompingSound;

    private AudioSource audio;

    private bool approaching;
    private float volumeDelta;
    private float stompSpeed;
    private float panVelocity;

	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (approaching)
        {
            audio.volume += volumeDelta;
        }	
	}

    void PlaySound(AudioClip audioClip)
    {
        audio.Stop();
        audio.clip = audioClip;
        audio.Play();
    }


}
