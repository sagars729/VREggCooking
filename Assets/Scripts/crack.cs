using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crack : MonoBehaviour
{
	public AudioClip crackEggAudio;
	public AudioClip breakEggAudio;
    public Transform rightController;
    public Transform leftController;
    // public GameObject omlette;
    // public GameObject oil;

	private AudioSource audio;
    private int cracks = 0;
    private bool breakable = false;
    private bool breaking = false;
    private GameObject pan;

    public ParticleSystem eggWhite;
    public ParticleSystem eggYolk;

    // Start is called before the first frame update
    void Start()
    {
    	// get audio source attached to egg
    	audio = GetComponent<AudioSource>();
    }

    void Update() {
        if (cracks >= 3) {
            Vector3 right = rightController.position;
            Vector3 left = leftController.position;
            float dist = Vector3.Distance(right, left);
            if (breakable && dist <= 0.2) {
                breakable = false;
                breaking = true;
            } else if (breaking && dist >= 0.25) {
                audio.clip = breakEggAudio;
                audio.Play();
                // eggWhite.transform.position = transform.position;
                // eggYolk.transform.position = transform.position;

                eggWhite.Play();
                eggYolk.Play();
                breaking = false;
            } else if (!breakable && !breaking){
                if(!audio.isPlaying) {
                    eggWhite.GetComponent<Heat>().StartCooking(false);
                    eggYolk.GetComponent<Heat>().StartCooking(true);
                    pan.GetComponent<sizzle>().Sizzle(0.1f, 0.05f);
                    gameObject.SetActive(false);
                }
            }
        }

    }

    private void crackEgg() {
        eggWhite.Clear();
        eggYolk.Clear();

        audio.clip = crackEggAudio;
        audio.Play();
        cracks += 1;

        if (cracks >= 3) {
            breakable = true;
            breaking = false;
        }
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Pan")) {
            pan = other.gameObject;
            crackEgg();
        }
    }

    void OnCollisionEnter(Collision other){
        if (other.gameObject.CompareTag("Pan")) {
            pan = other.gameObject;
            crackEgg();
        }
    }
}
