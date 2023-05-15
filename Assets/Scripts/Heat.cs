using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heat : MonoBehaviour
{
	private float startTime;
	private float timeRn;

    private float alpha;
    private bool cooking;
    private bool yolk;
    // Start is called before the first frame update
    void Start()
    {
        cooking = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (cooking) {
        	CookEgg();
        }
    }

    void CookEgg() {
    	timeRn = Time.time - startTime;
        alpha = (timeRn*5 + 20)/255;

        float blue = 1;
        float yellow = 1;

        if(alpha < 0.75){
        	if (yolk) {
        		yellow = Mathf.Min((timeRn*1.5f + 190)/255, 1.0f);
        		blue = 0;
        	}

            this.GetComponent<Renderer>().material.color = new Color(
            	1, yellow, blue, alpha);
        }
    }

    public void StartCooking(bool isYolk) {
    	startTime = Time.time;
    	cooking = true;
    	yolk = isYolk;
    }
    // 
}
