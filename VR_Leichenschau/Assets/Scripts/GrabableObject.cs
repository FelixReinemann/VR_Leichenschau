using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GrabableObject : MonoBehaviour {
	
	public SpringJoint spring;
	public Rigidbody connectedTo;
	Rigidbody myRigid;

	float breakForce;
	// Use this for initialization
	void Start () {
		myRigid = GetComponent<Rigidbody>();
		spring = GetComponent<SpringJoint>();
		if(spring!=null){
			Destroy(spring);
		}
	}
	
	void Update(){
		if(connectedTo!=null){
			//spring.connectedAnchor = connectedTo.position;
		}
		if(spring!=null){
			Debug.Log("currentForce: "+spring.currentForce+"; currentTorque: "+spring.currentTorque);
			Debug.DrawLine(spring.anchor,spring.connectedAnchor,Color.blue);
			Debug.DrawLine(spring.anchor, spring.connectedBody.position);
		}
		/*if(spring!= null && spring.currentForce.magnitude > breakForce){
			Release();
		}*/
	}

	void FixedUpdate(){
		
	}

	public void OnGrab(Rigidbody hand){
		Debug.Log("grab got called");
		if(spring!=null){
			return;
		}
		connectedTo = hand;
		spring = gameObject.AddComponent<SpringJoint>();
		spring.connectedBody = hand;
		spring.enablePreprocessing = false;
		spring.maxDistance=0.01f;
		spring.minDistance=0f;
		spring.damper=1f;
		spring.connectedMassScale=50f;
		
		//spring.breakForce = 30;
		//breakForce = 30;
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
		//spring.connectedBody = null;
		Destroy(spring);
		myRigid.useGravity = true;
		//myRigid.isKinematic = false;
	}
}
