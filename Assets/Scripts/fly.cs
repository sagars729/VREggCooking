using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fly : MonoBehaviour
{
    public float speed;
    public GameObject rig;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 velocity = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick);
        float radians = transform.rotation.eulerAngles.y/180 * Mathf.PI;
        
        // Account for rotation
		float realY = (-1 * Mathf.Sin(radians) * velocity.x
						+ Mathf.Cos(radians) * velocity.y);
		float realX = (Mathf.Cos(radians) * velocity.x
						+ Mathf.Sin(radians) * velocity.y);

        // Move
        Vector3 offset = new Vector3(realX, 0.0f, realY);
        rig.transform.position = rig.transform.position + speed * offset;
    }
}
