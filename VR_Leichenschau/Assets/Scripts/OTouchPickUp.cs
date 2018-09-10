using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OTouchPickUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
		mask = 1 << LayerMask.NameToLayer("Interactable");
		col = GetComponent<SphereCollider>();
		myRigid = GetComponent<Rigidbody>();

		if(hand==Hands.LeftHand){
			handTrigger = OVRInput.Axis1D.PrimaryHandTrigger;
		} else {
			handTrigger = OVRInput.Axis1D.SecondaryHandTrigger;
		}
	}
	
	public bool grabbing;
	public SphereCollider col;
	public Hands hand;
	OVRInput.Axis1D handTrigger;
	Rigidbody myRigid;
	GameObject grabbedObject;
	//public Transform lTouchController, rTouchController;

	// Update is called once per frame

	RaycastHit hit;
	int mask;
	void FixedUpdate () {
		OVRInput.Update();
		if(OVRInput.Get( handTrigger) > 0 ){
			grabbing = true;
			//Debug.Log(OVRInput.Get( OVRInput.Axis1D.PrimaryHandTrigger) +" ; "+OVRInput.Get( OVRInput.Axis1D.SecondaryHandTrigger));
			if(grabbedObject == null){
				Collider[] colObjects = Physics.OverlapSphere(transform.position,col.radius,mask);
                //Debug.Log("Amount: "+colObjects.Length);
                GrabableObject script;
                for (int i = 0; i < colObjects.Length; i++){
					script = colObjects[i].attachedRigidbody.GetComponent<GrabableObject>();
                    if (script.fixedJoint == null)
                    {
                        script.OnGrab(myRigid);
                        grabbedObject = colObjects[i].attachedRigidbody.gameObject;
                        break;
                    }
				}
			}
		} else {
			grabbing = false;
			if(grabbedObject != null){
				grabbedObject.GetComponent<GrabableObject>().Release();
				grabbedObject = null;
			}
		}

        if (hand == Hands.RightHand)
        {
            if (OVRInput.GetDown(OVRInput.RawButton.A))
            {
                Debug.Log("Button 1 pressed");
				CastRayForMarkable();
            }
            if (OVRInput.GetDown(OVRInput.RawButton.B))
            {
                Debug.Log("Button 2 pressed");
                CastRayForMarkable();
            }
        }
        else
        {
            if (OVRInput.GetDown(OVRInput.RawButton.X))
            {
                Debug.Log("Button 3 pressed");
                CastRayForMarkable();
            }
            if (OVRInput.GetDown(OVRInput.RawButton.Y))
            {
                Debug.Log("Button 4 pressed");
                CastRayForMarkable();
            }
        }
	}

	float castRadius = 0.2f;
    public void CastRayForMarkable()
    {
		if(Physics.SphereCast(transform.position,castRadius,transform.forward, out hit, 10, mask)){
			MarkableObject script;
			script = hit.collider.attachedRigidbody.GetComponent<MarkableObject>();
			if(script!=null){
				script.Mark();
			}
		}
    }
}



public enum Hands {LeftHand, RightHand}