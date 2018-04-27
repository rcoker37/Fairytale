using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartledPlayerController : PlayerController {

    public int frameCountTotal = 2;
    public int frameCount = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        frameCount++;	
	}
}
