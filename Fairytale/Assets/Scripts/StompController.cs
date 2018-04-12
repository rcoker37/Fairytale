using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompController : MonoBehaviour {

    public AudioClip StompSound;
    public float MinVolume;
    public float MaxVolume;
    public float TimeBeforeStartMean;
    public float TimeBeforeStartRange;
    public float TimeBetweenStompsMean;
    public float TimeBetweenStompsRange;
    public int TotalStepsMean;
    public int TotalStepsRange;

    private AudioSource audio;

    public bool stomping;
    private float timeBetweenStomps;
    private float timeToNextStomp;
    private int stepsRemaining;

	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
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

            if (stepsRemaining == 0)
            {
                stomping = false;
            }
        }	
	}

    public void StartStomp()
    {
        timeBetweenStomps = Random.Range(TimeBetweenStompsMean - TimeBetweenStompsRange, 
                                         TimeBetweenStompsMean + TimeBetweenStompsRange);
        stepsRemaining = Mathf.RoundToInt(Random.Range(TotalStepsMean - TotalStepsRange, 
                                                       TotalStepsMean + TotalStepsRange));

        stomping = true;
        timeToNextStomp = Random.Range(TimeBeforeStartMean - TimeBeforeStartRange, TimeBeforeStartMean + TimeBeforeStartRange);
        audio.volume = MinVolume;
    }

    void StopStomp()
    {
        stomping = false;
    }

}
