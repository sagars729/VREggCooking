using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sizzle : MonoBehaviour
{
    public GameObject oil;

    // sizzling audio
	private AudioSource audio;

	// sizzling sequence
	private bool sizzling;
	private float oilRate;
	private float cookRate;
	private float startOilTime;
	private float startCookTime;

	// constants
	private Vector3 initScale;
	private float initVolume;
    // Start is called before the first frame update
    void Start()
    {
    	// get audio source attached to egg
    	audio = GetComponent<AudioSource>();
    	initScale = oil.transform.localScale;
    	initVolume = audio.volume;
    	sizzling = false;
    }

    // Update is called once per frame
    void Update()
    {
    	// Reset Oil
    	if (OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger) > 0){
    		startOilTime = Time.time;
    	}

    	// Update Sizzle
    	UpdateSizzle();
    }

    public void UpdateSizzle() {
    	if (sizzling) {
    		float deltaOilTime = Time.time - startOilTime;
    		float deltaCookTime = Time.time - startCookTime;


    		float oilRemaining = 1 - deltaOilTime*oilRate;
    		float eggRemaining = 1 - deltaCookTime*cookRate;
    		float remaining = eggRemaining * oilRemaining;

    		if (remaining <= 0) {
    			sizzling = false;
    		} else {
    			oil.transform.localScale = oilRemaining*initScale;
    			audio.volume = remaining*initVolume;
    		}
    	}
    }

    public void Sizzle(float newOilRate, float newCookRate)
    {
    	audio.Play();
    	audio.volume = 1.0f;

    	startOilTime = Time.time;
    	startCookTime = Time.time;

    	oilRate = newOilRate;
    	cookRate = newCookRate;

    	sizzling = true;
    }
}
