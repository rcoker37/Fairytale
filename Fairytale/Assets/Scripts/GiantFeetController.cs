using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantFeetController : MonoBehaviour {

    public bool ShouldPlaySound;
    public bool ShouldKill;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (ShouldPlaySound) {
            GetComponent<AudioSource>().Play();
            GetComponent<Animator>().SetBool("Play", false);
        }	

        if (ShouldKill) {
            GetComponentInParent<PlayerControllerManager>().Kill();
        }
	}

	public void Play() 
	{
        GetComponent<Animator>().SetBool("Play", true);
	}
}
