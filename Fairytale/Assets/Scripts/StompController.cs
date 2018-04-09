using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompController : MonoBehaviour {

    public AudioClip StompSound;
    public float MinVolume;
    public float MaxVolume;
    public float TimeBetweenStompsMean;
    public float TimeBetweenStompsRange;
    public int TotalStepsMean;
    public int TotalStepsRange;

    private AudioSource audio;

    private bool stomping;
    private float timeBetweenStomps;
    private float timeToNextStomp;
    private int stepsRemaining;

	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!stomping && Input.GetKeyDown(KeyCode.R))
        {
            StartStomp(); 
        }

        if (stomping && Input.GetKeyDown(KeyCode.T))
        {
            StopStomp();
        }

        if (stomping)
        {
            timeToNextStomp -= Time.deltaTime;
            if (timeToNextStomp < 0.0f && stepsRemaining > 0)
            {
                timeToNextStomp = timeBetweenStomps;
                stepsRemaining -= 1;

                audio.volume = (MaxVolume - audio.volume) / stepsRemaining + audio.volume;
                audio.Play();
            }
        }	
	}

    void StartStomp()
    {
        timeBetweenStomps = Random.Range(TimeBetweenStompsMean - TimeBetweenStompsRange / 2.0f, 
                                         TimeBetweenStompsMean + TimeBetweenStompsRange / 2.0f);
        stepsRemaining = Mathf.RoundToInt(Random.Range(TotalStepsMean - TotalStepsRange / 2.0f, 
                                                       TotalStepsMean + TotalStepsRange / 2.0f));

        stomping = true;
        timeToNextStomp = 0.0f;
        audio.volume = MinVolume;
    }

    void StopStomp()
    {
        stomping = false;
    }

}
