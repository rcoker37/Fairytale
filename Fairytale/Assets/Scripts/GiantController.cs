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

    private StompController sc;

	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
        sc = GetComponent<StompController>();
	}


    public void Smash()
    {
        sc.StartStomp();
    }


}
