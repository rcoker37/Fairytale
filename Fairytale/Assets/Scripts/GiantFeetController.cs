using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantFeetController : MonoBehaviour {
    private const float SCREEN_SHAKE_TIME = 0.8f;
    private const float SCREEN_SHAKE_AMOUNT = 0.35f;

    public float Y = 0.0f;
    public bool ShouldPlaySound;
    public bool ShouldKill;
    public bool Playing;

	// Use this for initialization
	void Start () {
        GetComponent<Animator>().SetInteger("Facing", -1);	
	}
	
	// Update is called once per frame
	void Update () {
        if (ShouldPlaySound) {
            GetComponent<AudioSource>().Play();
            StartCoroutine(ScreenShake(GetComponent<AudioSource>().volume));
        }	

        if (ShouldKill) {
            GetComponentInParent<PlayerControllerManager>().Kill();
        }

	}

	private void LateUpdate()
	{
        Vector3 pos = transform.position;
        pos.y = Y - transform.parent.position.y;
        transform.position = pos;
	}

	public void Play(bool shouldPlay) 
	{
        GetComponent<Animator>().SetBool("Play", shouldPlay);
	}

    public void Caught(bool caught) {
        GetComponent<Animator>().SetBool("Caught", caught);
    }

    public void SwitchSides() {
        GetComponent<Animator>().SetInteger("Facing", -1 * GetComponent<Animator>().GetInteger("Facing"));
    }

    private IEnumerator ScreenShake(float intensity)
    {
        Vector3 basePos = Camera.main.transform.localPosition;
        float shakeTime = 0;
        while (shakeTime < SCREEN_SHAKE_TIME)
        {
            Vector2 pos2d = (Vector2)basePos + Random.insideUnitCircle * SCREEN_SHAKE_AMOUNT * intensity;
            Camera.main.transform.localPosition = new Vector3(pos2d.x, pos2d.y, basePos.z);

            shakeTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        Camera.main.transform.localPosition = basePos;
    }

}
