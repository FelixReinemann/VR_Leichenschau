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
	// Update is called once per frame

	RaycastHit hit;
	int mask;
	void Update () {
		OVRInput.Update();
		if(OVRInput.Get( handTrigger) > 0 ){
			grabbing = true;
			//Debug.Log(OVRInput.Get( OVRInput.Axis1D.PrimaryHandTrigger) +" ; "+OVRInput.Get( OVRInput.Axis1D.SecondaryHandTrigger));
			if(grabbedObject == null){
				Collider[] colObjects = Physics.OverlapSphere(transform.position,col.radius,mask);
				Debug.Log("Amount: "+colObjects.Length);
				for (int i = 0; i < colObjects.Length; i++){
					colObjects[i].attachedRigidbody.GetComponent<GrabableObject>().OnGrab(myRigid);
					grabbedObject = colObjects[i].attachedRigidbody.gameObject;
					break;
				}
			}
		} else {
			grabbing = false;
			if(grabbedObject != null){
				grabbedObject.GetComponent<GrabableObject>().Release();
				grabbedObject = null;
			}
		}
	}
}

public enum Hands {LeftHand, RightHand}