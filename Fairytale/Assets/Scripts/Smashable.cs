using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smashable : MonoBehaviour {

	public float SmashThreshold;

	private Rigidbody2D rb;
    private AudioSource audio;

    private bool deathQueued;
    private bool smashThresholdReached;

	private void Start()
	{
		rb = gameObject.GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
	}

    private void Update()
    {
        if (deathQueued && !audio.isPlaying)
        {
            Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {
		if (Mathf.Abs(rb.velocity.y) >= SmashThreshold)
        {
            smashThresholdReached = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
	{
        if (smashThresholdReached)
		{
			Smash();
		}
	}

	private void Smash()
	{
		print("SMASH");
        audio.Play();
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        GameObject.FindGameObjectWithTag("Giant").GetComponent<GiantController>().Smash();
        deathQueued = true;
	}
	
}
