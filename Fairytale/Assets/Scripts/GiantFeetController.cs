using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantFeetController : MonoBehaviour {

    public bool ShouldPlaySound;
    public bool ShouldKill;

	// Use this for initialization
	void Start () {
        GetComponent<Animator>().SetFloat("Facing", -1.0f);	
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

    public void SwitchSides() {
        GetComponent<Animator>().SetFloat("Facing", -1.0f * GetComponent<Animator>().GetFloat("Facing"));
    }
}
