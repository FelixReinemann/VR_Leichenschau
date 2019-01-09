using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GrabableObject : MonoBehaviour {
	
	public FixedJoint grabJoint;
	public Rigidbody connectedTo;
	Rigidbody myRigid;

	public bool isBodyPart=false;

	float breakForce;
	// Use this for initialization
	void Start () {
		myRigid = GetComponent<Rigidbody>();
		grabJoint = GetComponent<FixedJoint>();
		if(grabJoint!=null){
			Destroy(grabJoint);
		}
	}
	
	void Update(){
		if(connectedTo!=null){
			//spring.connectedAnchor = connectedTo.position;
		}
		if(grabJoint!=null){
			//Debug.Log("currentForce: "+spring.currentForce+"; currentTorque: "+spring.currentTorque);
			//Debug.DrawLine(spring.anchor,spring.connectedAnchor,Color.blue);
			//Debug.DrawLine(spring.anchor, spring.connectedBody.position);
		}
		/*if(spring!= null && spring.currentForce.magnitude > breakForce){
			Release();
		}*/
	}

	void FixedUpdate(){
		
	}

	public void OnGrab(Rigidbody hand){
		//Debug.Log("grab got called");
		if(grabJoint!=null){
			return;
		}
		connectedTo = hand;
		grabJoint = gameObject.AddComponent<FixedJoint>();
		grabJoint.connectedBody = hand;
		grabJoint.enablePreprocessing = false;
		//grabJoint.maxDistance=0.01f;
		//grabJoint.minDistance=0f;
		//grabJoint.damper=1f;
		//grabJoint.connectedMassScale=50f;
		
		//spring.breakForce = 30;
		//breakForce = 30;
		myRigid.useGravity = false;
		//myRigid.isKinematic = true;
		//spring.connectedAnchor = grabbedTo.position;
		ChangeAllChildComponents someScript = GetComponentInParent<Animator>()?.GetComponent<ChangeAllChildComponents>();
		if(someScript!=null){
			someScript.drag=10f;
		}
		if(isBodyPart)
			GetComponentInParent<Animator>().GetComponent<ChangeAllChildComponents>().drag = 10f;
	}

	void OnJointBreak(float breakForce){
		connectedTo = null;
		myRigid.useGravity = true;
        if (isBodyPart)
            GetComponentInParent<Animator>().GetComponent<ChangeAllChildComponents>().drag = 4f;
	}

	public void Release(){
		connectedTo = null;
		//spring.connectedBody = null;
		Destroy(grabJoint);
		myRigid.useGravity = true;
        if (isBodyPart)
            GetComponentInParent<Animator>().GetComponent<ChangeAllChildComponents>().drag = 4f;
		//myRigid.isKinematic = false;
	}
}
