using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleController : MonoBehaviour {

    public float GrayScaleRadius = 0.15f;
    public float GrayScaleRange = 0.005f;
    public float FlickerRate = 0.01f;
    public bool ShouldFlicker = true;

    private Material material;
    private float targetRadius;
    private float currentRadius;

    // Use this for initialization 
    void Start () {
        material = GetComponent<Renderer>().material;
        targetRadius = currentRadius = GrayScaleRadius;
        Shader.SetGlobalFloat("_FadeMultiplier", 0.8f);
	}
	
	// Update is called once per frame
	void Update () {
        if (ShouldFlicker) {
            if (targetRadius > currentRadius)
            {
                currentRadius += FlickerRate * Time.deltaTime;
            }
            else
            {
                currentRadius -= FlickerRate * Time.deltaTime;
            }

            Shader.SetGlobalFloat("_GrayScaleDist", currentRadius);

            if (Mathf.Abs(targetRadius - currentRadius) < 2.0 * FlickerRate * Time.deltaTime)
            {
                targetRadius = Random.Range(GrayScaleRadius - GrayScaleRange, GrayScaleRadius + GrayScaleRange);
            }
        } else {
            Shader.SetGlobalFloat("_GrayScaleDist", 0.0f);
        }
	}
}
