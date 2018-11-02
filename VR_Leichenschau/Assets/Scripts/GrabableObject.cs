using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GrabableObject : MonoBehaviour {
	
	public FixedJoint fixedJoint;
	public Rigidbody connectedTo;
	Rigidbody myRigid;

	float breakForce;
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
		if(fixedJoint!=null){
			Debug.Log("currentForce: "+fixedJoint.currentForce+"; currentTorque: "+fixedJoint.currentTorque);
		}
		/*if(fixedJoint!= null && fixedJoint.currentForce.magnitude > breakForce){
			Release();
		}*/
	}

	void FixedUpdate(){
		
	}

	public void OnGrab(Rigidbody hand){
		Debug.Log("grab got called");
		if(fixedJoint!=null){
			return;
		}
		connectedTo = hand;
		fixedJoint = gameObject.AddComponent<FixedJoint>();
		fixedJoint.connectedBody = hand;
		fixedJoint.enablePreprocessing = false;
		//fixedJoint.breakForce = 30;
		breakForce = 30;
		myRigid.useGravity = false;
		//myRigid.isKinematic = true;
		//spring.connectedAnchor = grabbedTo.position;
	}

	void OnJointBreak(float breakForce){
		connectedTo = null;
		myRigid.useGravity = true;
	}

	public void Release(){
		connectedTo = null;
		//fixedJoint.connectedBody = null;
		Destroy(fixedJoint);
		myRigid.useGravity = true;
		//myRigid.isKinematic = false;
	}
}
