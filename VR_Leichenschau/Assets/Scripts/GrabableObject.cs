using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GrabableObject : MonoBehaviour {
	
	public FixedJoint fixedJoint;
	public Rigidbody connectedTo;
	Rigidbody myRigid;
	// Use this for initialization
	void Start () {
		myRigid = GetComponent<Rigidbody>();
		fixedJoint = GetComponent<FixedJoint>();
		if(fixedJoint!=null){
			Destroy(fixedJoint);
		}
	}
	
	void Update(){
		if(connectedTo!=null){
			//spring.connectedAnchor = connectedTo.position;
		}
	}

	public void OnGrab(Rigidbody hand){
		Debug.Log("grab got called");
		connectedTo = hand;
		fixedJoint = gameObject.AddComponent<FixedJoint>();
		fixedJoint.connectedBody = hand;
		myRigid.useGravity = false;
		//myRigid.isKinematic = true;
		//spring.connectedAnchor = grabbedTo.position;
	}

	public void Release(){
		connectedTo = null;
		fixedJoint.connectedBody = null;
		Destroy(fixedJoint);
		myRigid.useGravity = true;
		//myRigid.isKinematic = false;
	}
}
