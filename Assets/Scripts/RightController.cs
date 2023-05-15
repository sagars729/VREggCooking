using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RightController : MonoBehaviour
{
	// grabbed object if one exists
    private GameObject grabbedObject;

    void FixedUpdate()
    {
        //To grab GrabbableObjects in a radius
        GetTriggerPress();
    }

    void GetTriggerPress()
    {
        float pressedAmt = OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger);
        if(pressedAmt > 0.0){
            GrabObject();
        } else if (grabbedObject != null) {
            grabbedObject.GetComponent<GrabbableObject>().Grab(false);
            grabbedObject.GetComponent<GrabbableObject>().DetachFromParent();
            grabbedObject = null;
        }
    }

    void GrabObject()
    {
        if(grabbedObject == null)
        {
            Vector3 origin = transform.position;
            float radius = 0.25f; // arbitrarily adjust this radius, maybe scale?

            Collider[] hitColliders = Physics.OverlapSphere(origin, radius);
            hitColliders = hitColliders.OrderBy(x => Vector3.Distance(origin,x.transform.position)).ToArray();

            foreach(var hitCollider in hitColliders)
            {
                if(hitCollider.gameObject.CompareTag("Grab")) {
                    grabbedObject = hitCollider.gameObject;
                    grabbedObject.GetComponent<GrabbableObject>().Grab(true);
                    grabbedObject.GetComponent<GrabbableObject>().SetParent(transform.gameObject);
                    break;
                }
            }
        }

    }
    
}