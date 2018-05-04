using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour {

    public bool PlayerDisappear;
    public bool Restart;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (PlayerDisappear) {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerManager>().Disappear();   
        }	

        if (Restart) {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerManager>().Restart();   
        }
	}

    public void Appear(){
        GetComponent<Animator>().SetBool("Play", true);
    }
}
