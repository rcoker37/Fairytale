using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompController : MonoBehaviour {

	private const float SCREEN_SHAKE_TIME = 0.8f;
	private const float SCREEN_SHAKE_AMOUNT = 0.35f;

	public AudioClip stompClip;
    public float stompVolumeScale;

    private float startVolume;
    private float endVolume;
    private float delay;
    private float timeBetweenStomps;
    private int totalStomps;

    private AudioSource audio;
    private GiantController gc;

    public bool stomping;

    private float timeToNextStomp;
    private int stepsRemaining;

	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
        gc = GetComponent<GiantController>();
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

                audio.clip = stompClip;
                audio.volume = (endVolume - audio.volume) / stepsRemaining + audio.volume;
                audio.Play();
                print(stepsRemaining);

				StartCoroutine(ScreenShake());
            }

            if (stepsRemaining == 0)
            {
                stomping = false;
                gc.OnStompSequenceDone();
            }
        }	
	}

    public void StartStompSequence(float delay, float timeBetweenStomps, float startVolume, float endVolume,  int numberOfSteps)
    {
        this.timeBetweenStomps = timeBetweenStomps;
        this.startVolume = startVolume * stompVolumeScale;
        this.endVolume = endVolume * stompVolumeScale;
        stepsRemaining = numberOfSteps;


        stomping = true;
        timeToNextStomp = delay;
        audio.volume = startVolume;
    }

    void StopStomp()
    {
        stomping = false;
    }

	private IEnumerator ScreenShake()
	{
		Vector3 basePos = Camera.main.transform.localPosition;
		float shakeTime = 0;
		while (shakeTime < SCREEN_SHAKE_TIME)
		{
			Vector2 pos2d = (Vector2)basePos + Random.insideUnitCircle * SCREEN_SHAKE_AMOUNT;
			Camera.main.transform.localPosition = new Vector3(pos2d.x, pos2d.y, basePos.z);

			shakeTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}

		Camera.main.transform.localPosition = basePos;
	}

}
