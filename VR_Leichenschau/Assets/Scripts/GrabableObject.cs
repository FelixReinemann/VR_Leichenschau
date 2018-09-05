using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(FixedJoint))]
public class GrabableObject : MonoBehaviour {
	
	public FixedJoint spring;
	public Rigidbody connectedTo;
	Rigidbody myRigid;
	// Use this for initialization
	void Start () {
		myRigid = GetComponent<Rigidbody>();
		spring = GetComponent<FixedJoint>();
	}
	
	void Update(){
		if(connectedTo!=null){
			//spring.connectedAnchor = connectedTo.position;
		}
	}

	public void OnGrab(Rigidbody hand){
		Debug.Log("grab got called");
		connectedTo = hand;
		spring.connectedBody = hand;
		myRigid.useGravity = false;
		//myRigid.isKinematic = true;
		//spring.connectedAnchor = grabbedTo.position;
	}

	public void Release(){
		connectedTo = null;
		spring.connectedBody = null;
		myRigid.useGravity = true;
		//myRigid.isKinematic = false;
	}
}
